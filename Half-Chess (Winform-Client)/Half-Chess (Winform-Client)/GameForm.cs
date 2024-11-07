using Half_Chess__Winform_Client_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Half_Chess__Winform_Client_
{
    public partial class GameForm : Form
    {
        private LoginForm form { get; set; }

        public static Rectangle[,] boardCells = new Rectangle[8, 4];
        private ChessPiece[,] boardPieces = new ChessPiece[8, 4];
        private List<ChessPiece> pieces = new List<ChessPiece>();
        private List<Point> validMoves = new List<Point>();

        private Timer checkBlinkTimer = new Timer();
        private bool isBlinking = false;

        private Timer turnTimer = new Timer();
        private int remainingTime;

        private Color currentColor;
        private Bitmap canvas;

        private ChessPiece selectedPiece = null;

        private int cellWidth = 80;
        private int cellHeight = 80;

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
           
            form = f;

            IDLabel.Text = form.user.Id.ToString();
            NameLabel.Text = form.user.FirstName;
            CountryLabel.Text = form.user.Country;

            remainingTime = form.turnTime;
            TimerLabel.Text = $"{remainingTime}";

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

                    // Highlight valid moves
                    if (validMoves.Contains(new Point(j, i)))
                    {
                        if (boardPieces[i, j] != null && (boardPieces[i, j].Type == "♔" || boardPieces[i, j].Type == "♚"))
                        {
                            if (isBlinking)
                                g.FillRectangle(new SolidBrush(Color.Red), cell);   
                            else
                                g.FillRectangle(new SolidBrush(Color.PaleVioletRed), cell);
                        } else
                        {
                            g.FillRectangle(new SolidBrush(Color.LightGreen), cell);
                        }
                        g.DrawRectangle(new Pen(Color.Black, 1), cell);
                    } else
                    {
                        currentColor = (i + j) % 2 == 0 ? Color.AntiqueWhite : Color.DarkKhaki;
                        g.FillRectangle(new SolidBrush(currentColor), boardCells[i, j]);
                    }
                }
            }
        }

        private void InitializePieces()
        {
            pieces.Add(new ChessPiece("♔", Color.White, new Point(0, 7)));
            pieces.Add(new ChessPiece("♖", Color.White, new Point(3, 7)));
            pieces.Add(new ChessPiece("♘", Color.White, new Point(2, 7)));
            pieces.Add(new ChessPiece("♗", Color.White, new Point(1, 7)));

            for (int i = 0; i < 4; i++)
            {
                pieces.Add(new ChessPiece("♙", Color.White, new Point(i, 6)));
            }

            pieces.Add(new ChessPiece("♚", Color.Black, new Point(0, 0)));
            pieces.Add(new ChessPiece("♜", Color.Black, new Point(3, 0)));
            pieces.Add(new ChessPiece("♞", Color.Black, new Point(2, 0)));
            pieces.Add(new ChessPiece("♝", Color.Black, new Point(1, 0)));
            
            for (int i = 0; i < 4; i++)
            {
                pieces.Add(new ChessPiece("♟", Color.Black, new Point(i, 1)));
            }

            foreach (ChessPiece piece in pieces)
                boardPieces[piece.Position.Y, piece.Position.X] = piece;  
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            Point clickedCell = new Point((e.X - 240) / cellWidth, e.Y / cellHeight);
            ChessPiece piece = pieces.FirstOrDefault(p => p.Position == clickedCell);
            
            if (selectedPiece == null || (piece != null && selectedPiece.Color == piece.Color))
            {
                selectedPiece = piece;
                if (selectedPiece != null)
                {
                    validMoves = selectedPiece.CalculateValidMoves(boardPieces);
                    Invalidate();
                }
            }
            else
            {
                // If a piece is already selected, check if the click is on a valid move
                if (validMoves.Contains(clickedCell))
                {
                    // Move the piece to the new location
                    CapturePiece(clickedCell);
                    boardPieces[selectedPiece.Position.Y, selectedPiece.Position.X] = null;
                    selectedPiece.Position = clickedCell;
                    boardPieces[selectedPiece.Position.Y, selectedPiece.Position.X] = selectedPiece;
                    selectedPiece = null;
                    validMoves.Clear();
                    // PrintBoardPieces();
                    Invalidate();
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

        private void CapturePiece(Point clickedCell)
        {
            if (boardPieces[clickedCell.Y, clickedCell.X] != null)
            {
                boardPieces[clickedCell.Y, clickedCell.X] = null;
                // PrintBoardPieces();
            }
        }

        private void TurnTimer_Tick(object sender, EventArgs e)
        {
            if (remainingTime > 0)
            {
                remainingTime--;
                TimerLabel.Text = $"{remainingTime}";
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

            if (selectedPiece != null)
            {
                Rectangle selectedCell = boardCells[selectedPiece.Position.Y, selectedPiece.Position.X];
                g.DrawRectangle(new Pen(Color.Blue, 3), selectedCell);
            }
        }
    }
}
