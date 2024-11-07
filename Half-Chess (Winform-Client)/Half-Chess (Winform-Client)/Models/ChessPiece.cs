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
                int direction = Color == Color.White ? -1 : 1;
                Point forwardMove = new Point(Position.X, Position.Y + direction);
                if (IsValidMove(forwardMove, board, out bool canCapture) && !canCapture)
                    validMoves.Add(forwardMove);

                Point leftMove = new Point(Position.X - 1, Position.Y);
                Point rightMove = new Point(Position.X + 1, Position.Y);
                if (IsValidMove(leftMove, board, out canCapture) && !canCapture)
                    validMoves.Add(leftMove);
                if (IsValidMove(rightMove, board, out canCapture) && !canCapture)
                    validMoves.Add(rightMove);

                // Diagonal captures
                Point leftCapture = new Point(Position.X - 1, Position.Y + direction);
                Point rightCapture = new Point(Position.X + 1, Position.Y + direction);

                if (IsValidMove(leftCapture, board, out canCapture) && canCapture)
                    validMoves.Add(leftCapture);
                if (IsValidMove(rightCapture, board, out canCapture) && canCapture)
                    validMoves.Add(rightCapture);
            }
            else if (Type == "♖" || Type == "♜") 
            {
                AddLinearMoves(validMoves, board, new Point(0, 1));  
                AddLinearMoves(validMoves, board, new Point(0, -1)); 
                AddLinearMoves(validMoves, board, new Point(1, 0));  
                AddLinearMoves(validMoves, board, new Point(-1, 0)); 
            }
            else if (Type == "♘" || Type == "♞") 
            {
                Point[] knightMoves = {
                                        new Point(2, 1), new Point(2, -1), new Point(-2, 1), new Point(-2, -1),
                                        new Point(1, 2), new Point(1, -2), new Point(-1, 2), new Point(-1, -2)
                                      };

                foreach (var move in knightMoves)
                {
                    Point knightMove = new Point(Position.X + move.X, Position.Y + move.Y);
                    if (IsValidMove(knightMove, board, out bool canCapture))
                        validMoves.Add(knightMove);
                }
            }
            else if (Type == "♗" || Type == "♝") 
            {
                AddLinearMoves(validMoves, board, new Point(1, 1));  
                AddLinearMoves(validMoves, board, new Point(1, -1)); 
                AddLinearMoves(validMoves, board, new Point(-1, 1)); 
                AddLinearMoves(validMoves, board, new Point(-1, -1));
            }
            else if (Type == "♔" || Type == "♚")
            {
                Point[] kingMoves = {
                                    new Point(1, 0), new Point(-1, 0), new Point(0, 1), new Point(0, -1),
                                    new Point(1, 1), new Point(1, -1), new Point(-1, 1), new Point(-1, -1)
                                    };

                foreach (var move in kingMoves)
                {
                    Point kingMove = new Point(Position.X + move.X, Position.Y + move.Y);
                    if (IsValidMove(kingMove, board, out bool canCapture))
                        validMoves.Add(kingMove);
                }
            }

            return validMoves;
        }

        private void AddLinearMoves(List<Point> validMoves, ChessPiece[,] board, Point direction)
        {
            Point move = new Point(Position.X + direction.X, Position.Y + direction.Y);
            while (true)
            {
                if (!IsValidMove(move, board, out bool canCapture))
                    break;

                validMoves.Add(move);

                if (canCapture) // Stop moving further if a piece is captured
                    break;

                move = new Point(move.X + direction.X, move.Y + direction.Y);
            }
        }

        private bool IsValidMove(Point move, ChessPiece[,] board, out bool canCapture)
        {
            canCapture = false;

            // Check bounds
            if (move.X < 0 || move.X >= 4 || move.Y < 0 || move.Y >= 8)
                return false;

            ChessPiece targetPiece = board[move.Y, move.X];

            if (targetPiece == null)
                return true;

            // Capture
            if (targetPiece.Color != this.Color)
            {
                canCapture = true;
                return true;
            }

            return false;
        }

    }
}
