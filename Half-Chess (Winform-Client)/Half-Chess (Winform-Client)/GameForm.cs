using Half_Chess__Winform_Client_.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Half_Chess__Winform_Client_
{
    public partial class GameForm : Form
    {
        private LoginForm form { get; set; }

        private int cellWidth = 80;
        private int cellHeight = 80;

        public static Rectangle[,] boardCells = new Rectangle[8, 4];
        private ChessPiece[,] boardPieces = new ChessPiece[8, 4];
        private List<ChessPiece> pieces = new List<ChessPiece>();
        private List<Point> validMoves = new List<Point>();

        private bool isMyKingInCheck = false;
        private Point myKingBlinkPosition = Point.Empty;
        private Point myKingPosition = Point.Empty;

        private bool isOppKingInCheck = false;
        private Point oppKingPosition = Point.Empty;

        private Timer checkBlinkTimer = new Timer();
        private bool isBlinking = false;

        private Timer turnTimer = new Timer();
        private int remainingTime;

        private Color tileCurrentColor;

        private ChessPiece selectedPiece = null;

        private bool isDrawing = false;
        private Bitmap canvas;
        private Point lastPoint = Point.Empty;

        private ChessPiece.ChessColor myColor;
        private ChessPiece.ChessColor oppColor;
        private bool myTurn;

        private bool gameOver = false;

        private static readonly HttpClient client = new HttpClient();

        private const string PATH = "https://localhost:44382/";

        public GameDBEntities db = new GameDBEntities();

        private string gameMoves;
        private DateTime startTime;

        private Point lastOrigin = Point.Empty;
        private Point lastTarget = Point.Empty;

        private Timer pieceAnimationTimer;
        private int animationOffset = 0;
        private int animationDirection = 1;

        public enum Winner
        {
            Client,
            Server,
            Stalemate
        }
        
        public GameForm(LoginForm f)
        {
            InitializeComponent();

            this.Size = new Size(800, 675);
            this.MinimumSize = new Size(800, 675);
            this.MaximumSize = new Size(800, 675);
            this.StartPosition = FormStartPosition.CenterParent;

            form = f;

            IDLabel.Text = form.user.Id.ToString();
            NameLabel.Text = form.user.FirstName;
            CountryLabel.Text = form.user.Country;

            remainingTime = form.turnTime;
            TimerLabel.Text = "Timer";

            turnTimer.Interval = 1000;
            turnTimer.Tick += TurnTimer_Tick;
            turnTimer.Start();


            if (form.isWhite)
            {
                myColor = ChessPiece.ChessColor.White;
                oppColor = ChessPiece.ChessColor.Black;
                myTurn = true;
            }
            else
            {
                myColor = ChessPiece.ChessColor.Black;
                oppColor = ChessPiece.ChessColor.White;
                myTurn = false;
            }

            InitializeBoard();
            InitializePieces();

            checkBlinkTimer.Interval = 500;
            checkBlinkTimer.Tick += (s, e) =>
            {
                isBlinking = !isBlinking;
                Invalidate(); 
            };
            checkBlinkTimer.Start();

            canvas = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            drawBtn.Click += (s, e) =>
            {
                isDrawing = true;
                clearBtn.Enabled = true;
                drawBtn.Enabled = false;
            };
            clearBtn.Click += (s, e) => ClearDrawing();
            clearBtn.Enabled = false;

            if (!form.isWhite)
                ServerPlay();

            this.FormClosing += GameForm_FormClosing;

            gameMoves = "";
            startTime = DateTime.Now;

            pieceAnimationTimer = new Timer();
            pieceAnimationTimer.Interval = 100; 
            pieceAnimationTimer.Tick += PieceAnimationTimer_Tick;
            pieceAnimationTimer.Start();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 8; i++) 
            {
                for (int j = 0; j < 4; j++) 
                {
                    boardCells[i, j] = new Rectangle(j * cellWidth + 240, i * cellHeight, cellWidth, cellHeight);
                }
            }
        }

        public void DrawBoard(Graphics g)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++) 
                {
                    Rectangle cell = boardCells[i, j];
                    if ((isMyKingInCheck && myKingBlinkPosition == new Point(j, i)) || (isOppKingInCheck && oppKingPosition == new Point(j, i)))
                    {
                        if (isBlinking)
                            g.FillRectangle(new SolidBrush(Color.Red), cell);
                        else
                            g.FillRectangle(new SolidBrush(Color.PaleVioletRed), cell);
                    }
                    else if (validMoves.Contains(new Point(j, i)))
                    {
                        g.FillRectangle(new SolidBrush(Color.LightGreen), cell);
                        g.DrawRectangle(new Pen(Color.Black, 1), cell);
                    }
                    else if (lastOrigin != Point.Empty && (lastOrigin == new Point(j, i) || lastTarget == new Point(j, i)))
                    {
                        g.FillRectangle(new SolidBrush(Color.Gold), cell);
                        g.DrawRectangle(new Pen(Color.Black, 1), cell);
                    }
                    else
                    {
                        tileCurrentColor = (i + j) % 2 == 0 ? Color.AntiqueWhite : Color.DarkKhaki;
                        g.FillRectangle(new SolidBrush(tileCurrentColor), boardCells[i, j]);
                    }
                }
            }
        }

        private void InitializePieces()
        {
            if (myColor == ChessPiece.ChessColor.White)
            {
                pieces.Add(new ChessPiece("♔", ChessPiece.ChessColor.White, 0, 7, "King"));
                pieces.Add(new ChessPiece("♖", ChessPiece.ChessColor.White, 3, 7, "Rook"));
                pieces.Add(new ChessPiece("♘", ChessPiece.ChessColor.White, 2, 7, "Knight"));
                pieces.Add(new ChessPiece("♗", ChessPiece.ChessColor.White, 1, 7, "Bishop"));

                for (int i = 0; i < 4; i++)
                {
                    pieces.Add(new ChessPiece("♙", ChessPiece.ChessColor.White, i, 6, "Pawn"));
                }

                pieces.Add(new ChessPiece("♚", ChessPiece.ChessColor.Black, 0, 0, "King"));
                pieces.Add(new ChessPiece("♜", ChessPiece.ChessColor.Black, 3, 0, "Rook"));
                pieces.Add(new ChessPiece("♞", ChessPiece.ChessColor.Black, 2, 0, "Knight"));
                pieces.Add(new ChessPiece("♝", ChessPiece.ChessColor.Black, 1, 0, "Bishop"));

                for (int i = 0; i < 4; i++)
                {
                    pieces.Add(new ChessPiece("♟", ChessPiece.ChessColor.Black, i, 1, "Pawn"));
                }

                foreach (ChessPiece piece in pieces)
                    boardPieces[piece.Y, piece.X] = piece;

                ChessPiece myKing = pieces.FirstOrDefault(p => p.TypeName == "King" && p.PieceColor == myColor);
                ChessPiece oppKing = pieces.FirstOrDefault(p => p.TypeName == "King" && p.PieceColor == oppColor);

                if (myKing != null)
                {
                    myKingPosition = new Point(myKing.X, myKing.Y);
                    myKingBlinkPosition = myKingPosition;
                }
                if (oppKing != null) oppKingPosition = new Point(oppKing.X, oppKing.Y);
            } else
            {
                pieces.Add(new ChessPiece("♔", ChessPiece.ChessColor.White, 3, 0, "King"));
                pieces.Add(new ChessPiece("♖", ChessPiece.ChessColor.White, 0, 0, "Rook"));
                pieces.Add(new ChessPiece("♘", ChessPiece.ChessColor.White, 1, 0, "Knight"));
                pieces.Add(new ChessPiece("♗", ChessPiece.ChessColor.White, 2, 0, "Bishop"));

                for (int i = 0; i < 4; i++)
                {
                    pieces.Add(new ChessPiece("♙", ChessPiece.ChessColor.White, i, 1, "Pawn"));
                }

                pieces.Add(new ChessPiece("♚", ChessPiece.ChessColor.Black, 3, 7, "King"));
                pieces.Add(new ChessPiece("♜", ChessPiece.ChessColor.Black, 0, 7, "Rook"));
                pieces.Add(new ChessPiece("♞", ChessPiece.ChessColor.Black, 1, 7, "Knight"));
                pieces.Add(new ChessPiece("♝", ChessPiece.ChessColor.Black, 2, 7, "Bishop"));

                for (int i = 0; i < 4; i++)
                {
                    pieces.Add(new ChessPiece("♟", ChessPiece.ChessColor.Black, i, 6, "Pawn"));
                }

                foreach (ChessPiece piece in pieces)
                    boardPieces[piece.Y, piece.X] = piece;

                ChessPiece myKing = pieces.FirstOrDefault(p => p.TypeName == "King" && p.PieceColor == myColor);
                ChessPiece oppKing = pieces.FirstOrDefault(p => p.TypeName == "King" && p.PieceColor == oppColor);

                if (myKing != null)
                {
                    myKingPosition = new Point(myKing.X, myKing.Y);
                    myKingBlinkPosition = myKingPosition;
                }
                if (oppKing != null) oppKingPosition = new Point(oppKing.X, oppKing.Y);
            }
        }
        
        protected async override void OnMouseClick(MouseEventArgs e)
        {
            if (isDrawing || !myTurn || gameOver) return;

            Point clickedCell = new Point((e.X - 240) / cellWidth, e.Y / cellHeight);
            if (clickedCell.X < 0 || clickedCell.X >= 4 || clickedCell.Y < 0 || clickedCell.Y >= 8)
                return;

            ChessPiece piece = boardPieces[clickedCell.Y, clickedCell.X];

            if (selectedPiece == null || (piece != null && selectedPiece.PieceColor == piece.PieceColor))
            {
                if (piece != null && piece.PieceColor != myColor)
                    return;

                selectedPiece = piece;
                if (selectedPiece != null)
                {
                    validMoves = selectedPiece.CalculateValidMoves(boardPieces, true);
                    if (selectedPiece.TypeName != "King")
                        validMoves = FilterMovesThatResolveCheck(validMoves, selectedPiece);
                    Invalidate();
                }
            }
            else
            {
                // If a piece is already selected, check if the click is on a valid move
                if (validMoves.Contains(clickedCell))
                {
                    MoveTo(clickedCell);
                    isMyKingInCheck = IsKingInCheck(myColor);
                    isOppKingInCheck = IsKingInCheck(oppColor);

                    if (isOppKingInCheck && IsCheckmate(oppColor))
                    { 
                        EndGame((int)Winner.Client);
                    }
                    else if (!isOppKingInCheck && IsStalemate(oppColor))
                    {
                        EndGame((int)Winner.Stalemate);
                    }

                    Invalidate();
                    
                    ChangeTurn();
                    if (!gameOver)
                        await ServerPlay();

                    if (isMyKingInCheck && IsCheckmate(myColor))
                    {
                        EndGame((int)Winner.Server);
                    }
                    else if (!isMyKingInCheck && IsStalemate(myColor))
                    {
                        EndGame((int)Winner.Stalemate);
                    }
                }
                else
                {
                    // Deselect if the click is not on a valid move
                    selectedPiece = null;
                    validMoves.Clear();
                    Invalidate();
                }
            }
        }

        private List<Point> FilterMovesThatResolveCheck(List<Point> potentialMoves, ChessPiece piece)
        {
            List<Point> validMoves = new List<Point>();

            foreach (Point move in potentialMoves)
            {
                ChessPiece originalPieceAtTarget = boardPieces[move.Y, move.X];
                Point originalPosition = new Point(piece.X, piece.Y);

                // Move piece temporarily
                boardPieces[piece.Y, piece.X] = null;
                piece.X = move.X;
                piece.Y = move.Y;
                boardPieces[move.Y, move.X] = piece;

                if (piece.TypeName == "King")
                {
                    if (piece.PieceColor == myColor)
                        myKingPosition = new Point(piece.X, piece.Y);
                    else
                        oppKingPosition = new Point(piece.X, piece.Y);
                }

                bool isKingInCheckAfterMove = IsKingInCheck(piece.PieceColor);

                // Revert the move
                boardPieces[move.Y, move.X] = originalPieceAtTarget;
                boardPieces[originalPosition.Y, originalPosition.X] = piece;
                piece.X = originalPosition.X;
                piece.Y = originalPosition.Y;

                if (piece.TypeName == "King")
                {
                    if (piece.PieceColor == myColor)
                        myKingPosition = new Point(piece.X, piece.Y);
                    else
                        oppKingPosition = new Point(piece.X, piece.Y);
                }

                if (!isKingInCheckAfterMove)
                {
                    validMoves.Add(move);
                }
            }

            return validMoves;
        }

        private void MoveTo(Point target)
        {
            gameMoves += $"{selectedPiece.X}{selectedPiece.Y}{target.X}{target.Y} ";

            lastOrigin = new Point(selectedPiece.X, selectedPiece.Y);
            lastTarget = target;

            CapturePiece(target.X, target.Y);
            boardPieces[selectedPiece.Y, selectedPiece.X] = null;
            selectedPiece.X = target.X;
            selectedPiece.Y = target.Y;
            boardPieces[selectedPiece.Y, selectedPiece.X] = selectedPiece;

            if (selectedPiece.CanPromote(true))
            {
                PromotePawn(selectedPiece);
            }

            if (selectedPiece.TypeName == "King")
            {
                myKingPosition = new Point(selectedPiece.X, selectedPiece.Y);
                myKingBlinkPosition = myKingPosition;
            }

            selectedPiece = null;
            validMoves.Clear();
        }

        private void ServerMoveTo(CustomPoint origin, CustomPoint target)
        {
            selectedPiece = boardPieces[origin.Y, origin.X];
            gameMoves += $"{selectedPiece.X}{selectedPiece.Y}{target.X}{target.Y} ";

            CapturePiece(target.X, target.Y);
            boardPieces[origin.Y, origin.X] = null;
            selectedPiece.X = target.X;
            selectedPiece.Y = target.Y;
            boardPieces[target.Y, target.X] = selectedPiece;

            if (selectedPiece.CanPromote(false))
            {
                PromotePawn(selectedPiece);
            }

            if (selectedPiece.TypeName == "King")
            {
                oppKingPosition = new Point(selectedPiece.X, selectedPiece.Y);
            }
            lastOrigin = new Point(origin.X, origin.Y);
            lastTarget = new Point(target.X, target.Y);

            selectedPiece = null;
        }

        private void PromotePawn(ChessPiece pawn)
        {
            // Remove the pawn from the pieces list
            // pieces.Remove(pawn);

            // Create a new queen
            ChessPiece promotedQueen = new ChessPiece(
                pawn.PieceColor == ChessPiece.ChessColor.White ? "♕" : "♛",
                pawn.PieceColor,
                pawn.X,
                pawn.Y,
                "Queen"
            );

            // Add the new queen to the pieces list
            pieces.Add(promotedQueen);

            // Update the board pieces
            boardPieces[pawn.Y, pawn.X] = promotedQueen;

            selectedPiece = promotedQueen;
        }

        private void ChangeTurn()
        {
            myTurn = !myTurn;
            remainingTime = form.turnTime;
        }

        private async void EndGame(int winner)
        {
            gameOver = true;
            turnTimer.Stop();
            remainingTime = form.turnTime;

            DateTime endTime = DateTime.Now;
            TimeSpan gameDuration = endTime - startTime;

            string msg;
            string winner_txt;

            if (winner == 0)
            {
                msg = "You Win!";
                winner_txt = form.user.FirstName; 
            } else if (winner == 1)
            {
                msg = "You Lose!";
                winner_txt = "Server";
            } else
            {
                msg = "Stalemate!";
                winner_txt = "Stalemate";
            }

            var game = new TblGames
            {
                Id = 0,
                PlayerID = form.user.Id,
                PlayerName = form.user.FirstName,
                StartGameTime = startTime,
                GameDuration = (float)gameDuration.TotalSeconds,
                GameMoves = gameMoves,
                IsWhite = myColor == ChessPiece.ChessColor.White ? true : false, 
                Winner = winner_txt
            };

            //db.TblGames.Add(game);
            //await db.SaveChangesAsync();

            await PushClientDbAsync(game);

            using (EndGameDialog endGameDialog = new EndGameDialog(msg))
            {
                DialogResult result = endGameDialog.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    StartNewGame();  // Start a new game
                }
                else if (result == DialogResult.No)
                {
                    this.Close(); 
                }
            }
        }

        private async Task PushClientDbAsync(TblGames latestGame)
        {
            try
            {
                if (latestGame == null)
                {
                    MessageBox.Show("No game data to push.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Convert the data to JSON
                var jsonContent = JsonConvert.SerializeObject(latestGame);

                // Prepare the HTTP request
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(PATH + "api/TblUsers/PushGame", content);

                // Handle the response
                if (response.IsSuccessStatusCode)
                {
                    // MessageBox.Show("Game data successfully pushed to the server.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to push game data to the server: {errorMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while pushing game data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartNewGame()
        {
            this.Dispose();
            GameForm newGameForm = new GameForm(form);
            newGameForm.Show();
        }

        private async Task ServerPlay()
        {
            List<CustomPoint> move = await GetMovesAsync(PATH + "api/ChessGame");
            ServerMoveTo(move[0], move[1]);
            isMyKingInCheck = IsKingInCheck(myColor);
            isOppKingInCheck = IsKingInCheck(oppColor);
            ChangeTurn();
        }

        private async Task<List<CustomPoint>> GetMovesAsync(string url)
        {
            List<CustomPoint> move = null;

            var boardList = new List<ChessPiece>();

            // Convert the 2D board into a List<ChessPiece> (flatten the 2D array)
            for (int y = 0; y < boardPieces.GetLength(0); y++) // 8 rows
            {
                for (int x = 0; x < boardPieces.GetLength(1); x++) // 4 columns
                {
                    var chessPiece = boardPieces[y, x];
                    if (chessPiece != null)
                        boardList.Add(chessPiece);
                }
            }

            // Prepare the request object
            var request = new BoardRequest
            {
                PieceColor = (myColor == ChessPiece.ChessColor.White) ? BoardRequest.ChessColor.Black : BoardRequest.ChessColor.White,
                Board = boardList,
                KingPositionX = oppKingPosition.X,
                KingPositionY = oppKingPosition.Y
            };

            // Send the request to the server
            HttpResponseMessage response = await client.PostAsJsonAsync(url, request);
            if (response.IsSuccessStatusCode)
            {
                move = await response.Content.ReadAsAsync<List<CustomPoint>>(); // Read the response content as List<Point>
            }

            return move;
        }

        private bool IsKingInCheck(ChessPiece.ChessColor kingColor)
        {
            if (kingColor == myColor)
            {
                // Check if any opponent's piece can move to the king's position
                foreach (ChessPiece piece in boardPieces)
                {
                    if (piece != null && piece.PieceColor != kingColor && piece.TypeName != "King")
                    {
                        List<Point> moves = piece.CalculateValidMoves(boardPieces, false);
                        if (moves.Contains(myKingPosition))
                            return true;
                    }
                }
                return false;
            } else
            {
                // Check if any opponent's piece can move to the king's position
                foreach (ChessPiece piece in boardPieces)
                {
                    if (piece != null && piece.PieceColor != kingColor && piece.TypeName != "King")
                    {
                        List<Point> moves = piece.CalculateValidMoves(boardPieces, true);
                        if (moves.Contains(oppKingPosition))
                            return true;
                    }
                }
                return false;
            }
            
        }

        private bool IsStalemate(ChessPiece.ChessColor playerColor)
        {
            // Count pieces
            int totalPieces = 0, bishops = 0, knights = 0;

            foreach (ChessPiece piece in boardPieces)
            {
                if (piece != null)
                {
                    totalPieces++;
                    switch (piece.TypeName)
                    {
                        case "Bishop": bishops++; break;
                        case "Knight": knights++; break;
                    }
                }
            }

            // Insufficient mating material scenarios:
            // 1. King vs King
            // 2. King + Bishop vs King
            // 3. King + Knight vs King
            if (totalPieces == 2 || // King vs King
                (totalPieces == 3 && (bishops == 1 || knights == 1))) // King + Bishop/Knight vs King)
            {
                return true;
            }

            bool isClient = playerColor == myColor;

            foreach (ChessPiece piece in boardPieces)
            {
                if (piece != null && piece.PieceColor == playerColor)
                {
                    List<Point> potentialMoves = piece.CalculateValidMoves(boardPieces, isClient);

                    // If the piece has at least one valid move, it's not stalemate
                    if (potentialMoves.Count > 0)
                        return false;
                }
            }

            return true;
        }

        private bool IsCheckmate(ChessPiece.ChessColor kingColor)
        {
            bool isPlayer = kingColor == myColor;

            foreach (ChessPiece piece in boardPieces)
            {
                if (piece != null && piece.PieceColor == kingColor)
                {
                    List<Point> validMoves = piece.CalculateValidMoves(boardPieces, isPlayer);
                    if (piece.TypeName != "King")
                        validMoves = FilterMovesThatResolveCheck(validMoves, piece);

                    // If there's at least one valid move, it's not checkmate
                    if (validMoves.Count > 0)
                        return false;
                }
            }

            return true;
        }

        private void CapturePiece(int X, int Y)
        {
            if (boardPieces[Y, X] != null)
            {
                boardPieces[Y, X] = null;
                // PrintBoardPieces();
            }
        }

        private void TurnTimer_Tick(object sender, EventArgs e)
        {
            if (remainingTime > 0)
            {
                TimerLabel.Text = $"{remainingTime}";
                remainingTime--;
            }
            else
            {
                TimerLabel.Text = "Time's Up!";

                int winner = myTurn ? (int)Winner.Server : (int)Winner.Client;
                EndGame(winner);
            }
        }

        private void PrintBoardPieces()
        {
            string s = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (boardPieces[i, j] != null)
                        s += boardPieces[i, j].Type;
                    else
                        s += "♕";
                }
                s += "\n";
            }
            MessageBox.Show(s);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (isDrawing && e.Button == MouseButtons.Left)
            {
                lastPoint = e.Location;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (isDrawing && e.Button == MouseButtons.Left && lastPoint != Point.Empty)
            {
                using (Graphics g = Graphics.FromImage(canvas))
                {
                    using (Pen pen = new Pen(Color.Black, 5))
                    {
                        g.DrawLine(pen, lastPoint, e.Location);
                    }
                    // g.DrawLine(Pens.Black, lastPoint, e.Location);
                }
                lastPoint = e.Location;
                Invalidate(); // Request form repaint
            }
        }

        private void PieceAnimationTimer_Tick(object sender, EventArgs e)
        {
            if (selectedPiece != null)
            {
                // Change animation offset
                animationOffset += 2 * animationDirection;

                // Reverse direction when it reaches certain limits
                if (Math.Abs(animationOffset) > 5)
                {
                    animationDirection *= -1;
                }

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (isDrawing && e.Button == MouseButtons.Left)
            {
                lastPoint = Point.Empty; // Stop drawing
            }
        }

        private void ClearDrawing()
        {
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.Transparent); // Clear the bitmap
            }
            isDrawing = false;
            clearBtn.Enabled = false;
            drawBtn.Enabled = true;
            Invalidate(); // Request form repaint
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Draw board 
            DrawBoard(g);
            // Draw pieces
            foreach (ChessPiece piece in boardPieces)
            {
                if (piece != null)
                {
                    if (piece == selectedPiece)
                    {
                        // Slightly offset the selected piece
                        Rectangle offsetCell = boardCells[piece.Y, piece.X];
                        offsetCell.Offset(0, animationOffset);
                        piece.DrawPiece(g, boardCells, offsetCell);
                    }
                    else
                    {
                        piece.DrawPiece(g, boardCells);
                    }
                }
            }

            g.DrawImage(canvas, Point.Empty);

            if (selectedPiece != null)
            {
                Rectangle selectedCell = boardCells[selectedPiece.Y, selectedPiece.X];
                g.DrawRectangle(new Pen(Color.Blue, 3), selectedCell);
            }
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            turnTimer.Stop();
            checkBlinkTimer.Stop();
            pieceAnimationTimer.Stop();
        }

        private void ForfeitBtn_Click(object sender, EventArgs e)
        {
            EndGame((int)Winner.Server);
        }
    }
}
