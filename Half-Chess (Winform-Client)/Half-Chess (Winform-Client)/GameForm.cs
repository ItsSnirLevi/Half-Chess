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

        private Timer animationTimer = new Timer();
        private Color currentColor;
        private Bitmap canvas;

        private ChessPiece selectedPiece = null;
        private Point initialMousePosition;

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

            form = f;
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
                        g.FillRectangle(new SolidBrush(Color.LightGreen), cell);
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

        private List<Point> validMoves = new List<Point>();

        protected override void OnMouseClick(MouseEventArgs e)
        {
            Point clickedCell = new Point((e.X - 240) / cellWidth, e.Y / cellHeight);

            if (selectedPiece == null)
            {
                // Select a piece if it’s in the clicked cell
                selectedPiece = pieces.FirstOrDefault(p => p.Position == clickedCell);

                if (selectedPiece != null)
                {
                    // Calculate valid moves and highlight them
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
                    boardPieces[selectedPiece.Position.Y, selectedPiece.Position.X] = null;
                    selectedPiece.Position = clickedCell;
                    boardPieces[selectedPiece.Position.Y, selectedPiece.Position.X] = selectedPiece;
                    selectedPiece = null;
                    validMoves.Clear();
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Draw board 
            DrawBoard(g);
            // Draw pieces
            foreach (ChessPiece piece in pieces)
                piece.DrawPiece(g);

            if (selectedPiece != null)
            {
                Rectangle selectedCell = boardCells[selectedPiece.Position.Y, selectedPiece.Position.X];
                g.DrawRectangle(new Pen(Color.Blue, 3), selectedCell);
            }
        }


    }
}
