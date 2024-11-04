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

        public static Rectangle[,] boardCells = new Rectangle[4, 8];
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
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    boardCells[i, j] = new Rectangle(i * cellWidth + 240, j * cellHeight, cellWidth, cellHeight);
                }
            }
        }

        public void DrawBoard(Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    currentColor = (i + j) % 2 == 0 ? Color.AntiqueWhite : Color.DarkKhaki;
                    g.FillRectangle(new SolidBrush(currentColor), boardCells[i, j]);
                }
            }
        }

        private void InitializePieces()
        {
            pieces.Add(new ChessPiece("♔", Color.Black, new Point(3, 0)));
            pieces.Add(new ChessPiece("♖", Color.Black, new Point(0, 0)));
            pieces.Add(new ChessPiece("♘", Color.Black, new Point(1, 0)));
            pieces.Add(new ChessPiece("♗", Color.Black, new Point(2, 0)));

            for (int i = 0; i < 4; i++)
            {
                pieces.Add(new ChessPiece("♙", Color.Black, new Point(i, 1)));
            }

            pieces.Add(new ChessPiece("♚", Color.Black, new Point(3, 7)));
            pieces.Add(new ChessPiece("♜", Color.Black, new Point(0, 7)));
            pieces.Add(new ChessPiece("♞", Color.Black, new Point(1, 7)));
            pieces.Add(new ChessPiece("♝", Color.Black, new Point(2, 7)));
            
            for (int i = 0; i < 4; i++)
            {
                pieces.Add(new ChessPiece("♟", Color.Black, new Point(i, 6)));
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
        }
    }
}
