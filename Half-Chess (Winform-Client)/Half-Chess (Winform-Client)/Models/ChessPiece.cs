using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Half_Chess__Winform_Client_.Models
{
    internal class ChessPiece
    {
        // ♔ ♖ ♘ ♗ ♙ - white pieces
        // ♚ ♜ ♞ ♝ ♟ - black pieces

        public string Type { get; set; } 
        public Color Color { get; set; } 
        public Point Position { get; set; }

        public ChessPiece(string type, Color color, Point position)
        {
            Type = type;
            Color = color;
            Position = position;
        }

        public void DrawPiece(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color.Black))
            {
                Rectangle cell = GameForm.boardCells[Position.Y, Position.X];
                cell.Y += 10;
                g.DrawString(Type.Substring(0, 1), new Font("Arial", 40),
                             brush, cell.Location);
            }
        }

        public List<Point> CalculateValidMoves(ChessPiece[,] board)
        {
            List<Point> validMoves = new List<Point>();

            if (Type == "♙" || Type == "♟")
            {
                // Regular forward movement
                int direction = Color == Color.White ? -1 : 1;
                Point forwardMove = new Point(Position.X, Position.Y + direction);
                if (IsValidMove(forwardMove, board))
                    validMoves.Add(forwardMove);

                // Horizontal moves (strategic improvement rule)
                Point leftMove = new Point(Position.X - 1, Position.Y);
                Point rightMove = new Point(Position.X + 1, Position.Y);
                if (IsValidMove(leftMove, board)) validMoves.Add(leftMove);
                if (IsValidMove(rightMove, board)) validMoves.Add(rightMove);
            }

            // Add logic for other pieces
            return validMoves;
        }

        private bool IsValidMove(Point move, ChessPiece[,] board)
        {
            return move.X >= 0 && move.X < 4 && move.Y >= 0 && move.Y < 8 && board[move.Y, move.X] == null;
        }

    }
}
