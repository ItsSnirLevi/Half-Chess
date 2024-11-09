using Half_Chess__Winform_Client_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private ChessPiece.ChessColor myColor = ChessPiece.ChessColor.White;
        private ChessPiece.ChessColor oppColor = ChessPiece.ChessColor.Black;
        private bool myTurn = true;

        private static readonly HttpClient client = new HttpClient();

        public GameForm(LoginForm f)
        {
            InitializeComponent();

            this.Size = new Size(800, 675);
            this.MinimumSize = new Size(800, 675);
            this.MaximumSize = new Size(800, 675);
            canvas = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

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
            };
            clearBtn.Click += (s, e) => ClearDrawing();
            clearBtn.Enabled = false;

            form = f;

            IDLabel.Text = form.user.Id.ToString();
            NameLabel.Text = form.user.FirstName;
            CountryLabel.Text = form.user.Country;

            remainingTime = form.turnTime;
            TimerLabel.Text = "Timer";

            turnTimer.Interval = 1000; 
            turnTimer.Tick += TurnTimer_Tick;
            turnTimer.Start();
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
                    } else
                    {
                        tileCurrentColor = (i + j) % 2 == 0 ? Color.AntiqueWhite : Color.DarkKhaki;
                        g.FillRectangle(new SolidBrush(tileCurrentColor), boardCells[i, j]);
                    }
                }
            }
        }

        private void InitializePieces()
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

            ChessPiece myKing = pieces.FirstOrDefault(p => p.Type == "♔" && p.PieceColor == myColor);
            ChessPiece oppKing = pieces.FirstOrDefault(p => p.Type == "♚" && p.PieceColor == oppColor);

            if (myKing != null) {
                myKingPosition = new Point(myKing.X, myKing.Y);
                myKingBlinkPosition = myKingPosition;
            }
            if (oppKing != null) oppKingPosition = new Point(oppKing.X, oppKing.Y);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (isDrawing || !myTurn) return;

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
                    validMoves = CalculateValidMovesForCheck();
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
                    Invalidate();

                    ChangeTurn();
                    ServerPlay();
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

        private List<Point> CalculateValidMovesForCheck()
        {
            List<Point> tmp = selectedPiece.CalculateValidMoves(boardPieces);

            tmp = FilterMovesThatResolveCheck(tmp, selectedPiece);

            return tmp;
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

                if (selectedPiece.TypeName == "King")
                {
                    myKingPosition = new Point(piece.X, piece.Y);
                }

                bool isKingInCheckAfterMove = IsKingInCheck(piece.PieceColor);

                // Revert the move
                boardPieces[move.Y, move.X] = originalPieceAtTarget;
                boardPieces[originalPosition.Y, originalPosition.X] = piece;
                piece.X = originalPosition.X;
                piece.Y = originalPosition.Y;

                if (selectedPiece.TypeName == "King")
                {
                    myKingPosition = new Point(piece.X, piece.Y);
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
            CapturePiece(target.X, target.Y);
            boardPieces[selectedPiece.Y, selectedPiece.X] = null;
            selectedPiece.X = target.X;
            selectedPiece.Y = target.Y;
            boardPieces[selectedPiece.Y, selectedPiece.X] = selectedPiece;

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
            CapturePiece(target.X, target.Y);
            boardPieces[origin.Y, origin.X] = null;
            selectedPiece.X = target.X;
            selectedPiece.Y = target.Y;
            boardPieces[target.Y, target.X] = selectedPiece;

            if (selectedPiece.TypeName == "King")
            {
                oppKingPosition = new Point(selectedPiece.X, selectedPiece.Y);
            }

            selectedPiece = null;
        }

        private void ChangeTurn()
        {
            myTurn = !myTurn;
            remainingTime = form.turnTime;
        }

        private async void ServerPlay()
        {
            List<CustomPoint> move = await GetMovesAsync("");
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
                Board = boardList
            };

            // Send the request to the server
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44382/api/ChessGame", request);
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
                    if (piece != null && piece.PieceColor != kingColor)
                    {
                        List<Point> moves = piece.CalculateValidMoves(boardPieces);
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
                    if (piece != null && piece.PieceColor != kingColor)
                    {
                        List<Point> moves = piece.CalculateValidMoves(boardPieces);
                        if (moves.Contains(oppKingPosition))
                            return true;
                    }
                }
                return false;
            }
            
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
                turnTimer.Stop();
                TimerLabel.Text = "Time's up!";
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
                    g.DrawLine(Pens.Black, lastPoint, e.Location);
                }
                lastPoint = e.Location;
                Invalidate(); // Request form repaint
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
                if (piece != null)
                    piece.DrawPiece(g);

            g.DrawImage(canvas, Point.Empty);

            if (selectedPiece != null)
            {
                Rectangle selectedCell = boardCells[selectedPiece.Y, selectedPiece.X];
                g.DrawRectangle(new Pen(Color.Blue, 3), selectedCell);
            }
        }
    }
}
