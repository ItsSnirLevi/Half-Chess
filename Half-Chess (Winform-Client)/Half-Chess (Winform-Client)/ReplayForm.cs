using Half_Chess__Winform_Client_.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Half_Chess__Winform_Client_
{
    public partial class ReplayForm : Form
    {
        private LoginForm form { get; set; }

        public static Rectangle[,] boardCells = new Rectangle[8, 4];
        private int cellWidth = 80;
        private int cellHeight = 80;

        private ChessPiece[,] boardPieces = new ChessPiece[8, 4];
        private List<ChessPiece> pieces = new List<ChessPiece>();

        private ChessPiece.ChessColor myColor;
        private ChessPiece.ChessColor oppColor;

        private bool isMyKingInCheck = false;
        private Point myKingBlinkPosition = Point.Empty;
        private Point myKingPosition = Point.Empty;

        private bool isOppKingInCheck = false;
        private Point oppKingPosition = Point.Empty;

        private ChessPiece selectedPiece = null;

        private Timer checkBlinkTimer = new Timer();
        private bool isBlinking = false;

        private Color tileCurrentColor;

        private Timer replayTimer;
        private int currentMoveIndex = 0;
        private string[] moveArray;
        private bool turn;

        private string moves;
        private bool isWhite;

        TblGames details;

        public ReplayForm(TblGames details)
        {
            InitializeComponent();

            this.Size = new Size(800, 675);
            this.MinimumSize = new Size(800, 675);
            this.MaximumSize = new Size(800, 675);
            this.StartPosition = FormStartPosition.CenterParent;

            this.details = details;
            this.moves = details.GameMoves;
            this.isWhite = details.IsWhite;

            IDLabel.Text = details.Id.ToString();
            NameLabel.Text = details.PlayerName;
            TimeLabel.Text = details.StartGameTime.ToString();
            DurLabel.Text = Convert.ToInt32(details.GameDuration).ToString() + " Seconds";
            WinnerLabel.Text = "Winner: " + details.Winner;

            replayTimer = new Timer();
            replayTimer.Interval = 1000; 
            replayTimer.Tick += ReplayTimer_Tick;

            if (isWhite)
            {
                myColor = ChessPiece.ChessColor.White;
                oppColor = ChessPiece.ChessColor.Black;
            } else
            {
                myColor = ChessPiece.ChessColor.Black;
                oppColor = ChessPiece.ChessColor.White;
            }

            checkBlinkTimer.Interval = 500;
            checkBlinkTimer.Tick += (s, e) =>
            {
                isBlinking = !isBlinking;
                Invalidate();
            };
            checkBlinkTimer.Start();

            InitializeBoard();
            InitializePieces();
        }

        private void ReplayForm_Load(object sender, EventArgs e)
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
            }
            else
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

        private void ReplayTimer_Tick(object sender, EventArgs e)
        {
            if (currentMoveIndex >= moveArray.Length)
            {
                replayTimer.Stop(); // Stop the timer when all moves are done
                /*StartButton.Text = "Replay";
                StartButton.Show();*/
                EndLabel.Text = "Game Over";
                return;
            }

            string move = moveArray[currentMoveIndex];
            CustomPoint origin = new CustomPoint
            {
                X = int.Parse(move[0].ToString()),
                Y = int.Parse(move[1].ToString())
            };
            CustomPoint target = new CustomPoint
            {
                X = int.Parse(move[2].ToString()),
                Y = int.Parse(move[3].ToString())
            };

            if (turn)
            {
                MoveTo(origin, target);
            }
            else
            {
                ServerMoveTo(origin, target);
            }
            isMyKingInCheck = IsKingInCheck(myColor);
            isOppKingInCheck = IsKingInCheck(oppColor);

            Invalidate(); 
            turn = !turn;
            currentMoveIndex++;
        }

        private void ReplayGame()
        {
            moveArray = moves.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            currentMoveIndex = 0;
            turn = isWhite;
            replayTimer.Start(); // Start the timer
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
            }
            else
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

        private void MoveTo(CustomPoint origin, CustomPoint target)
        {
            selectedPiece = boardPieces[origin.Y, origin.X];

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

        private void CapturePiece(int X, int Y)
        {
            if (boardPieces[Y, X] != null)
            {
                boardPieces[Y, X] = null;
                // PrintBoardPieces();
            }
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
                    piece.DrawPiece(g, boardCells);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            ReplayGame();
            StartButton.Hide();
        }
    }
}
