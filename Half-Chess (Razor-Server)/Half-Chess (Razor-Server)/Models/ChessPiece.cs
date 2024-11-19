using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Half_Chess__Razor_Server_.Models
{
    public class ChessPiece
    {
        // ♔ ♖ ♘ ♗ ♙ - white pieces
        // ♚ ♜ ♞ ♝ ♟ - black pieces

        public enum ChessColor
        {
            White,
            Black
        }

        public string Type { get; set; }
        public string TypeName { get; set; }
        public ChessColor PieceColor { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public ChessPiece() { }

        public List<Point> CalculateValidMoves(ChessPiece[,] board, bool isServer)
        {
            List<Point> validMoves = new List<Point>();

            if (TypeName == "Pawn")
            {
                int direction = isServer ? 1 : -1;
                bool canCapture;

                Point forwardMove = new Point(X, Y + direction);
                if (IsValidMove(forwardMove, board, out canCapture) && !canCapture)
                {
                    validMoves.Add(forwardMove);

                    if (isServer && this.Y == 1)
                    {
                        Point doubleForwardMove = new Point(X, Y + 2 * direction);
                        if (IsValidMove(doubleForwardMove, board, out canCapture) && !canCapture)
                            validMoves.Add(doubleForwardMove);
                    }
                    else if (!isServer && this.Y == 6)
                    {
                        Point doubleForwardMove = new Point(X, Y + 2 * direction);
                        if (IsValidMove(doubleForwardMove, board, out canCapture) && !canCapture)
                            validMoves.Add(doubleForwardMove);
                    }
                }
                    
                Point leftMove = new Point(X - 1, Y);
                Point rightMove = new Point(X + 1, Y);
                if (IsValidMove(leftMove, board, out canCapture) && !canCapture)
                    validMoves.Add(leftMove);
                if (IsValidMove(rightMove, board, out canCapture) && !canCapture)
                    validMoves.Add(rightMove);

                // Diagonal captures
                Point leftCapture = new Point(X - 1, Y + direction);
                Point rightCapture = new Point(X + 1, Y + direction);

                if (IsValidMove(leftCapture, board, out canCapture) && canCapture)
                    validMoves.Add(leftCapture);
                if (IsValidMove(rightCapture, board, out canCapture) && canCapture)
                    validMoves.Add(rightCapture);
            }
            else if (TypeName == "Rook")
            {
                AddLinearMoves(validMoves, board, new Point(0, 1));
                AddLinearMoves(validMoves, board, new Point(0, -1));
                AddLinearMoves(validMoves, board, new Point(1, 0));
                AddLinearMoves(validMoves, board, new Point(-1, 0));
            }
            else if (TypeName == "Knight")
            {
                Point[] knightMoves = {
                                        new Point(2, 1), new Point(2, -1), new Point(-2, 1), new Point(-2, -1),
                                        new Point(1, 2), new Point(1, -2), new Point(-1, 2), new Point(-1, -2)
                                      };

                foreach (var move in knightMoves)
                {
                    Point knightMove = new Point(X + move.X, Y + move.Y);
                    if (IsValidMove(knightMove, board, out bool canCapture))
                        validMoves.Add(knightMove);
                }
            }
            else if (TypeName == "Bishop")
            {
                AddLinearMoves(validMoves, board, new Point(1, 1));
                AddLinearMoves(validMoves, board, new Point(1, -1));
                AddLinearMoves(validMoves, board, new Point(-1, 1));
                AddLinearMoves(validMoves, board, new Point(-1, -1));
            }
            else if (TypeName == "Queen")
            {
                AddLinearMoves(validMoves, board, new Point(0, 1));  // Vertical up
                AddLinearMoves(validMoves, board, new Point(0, -1)); // Vertical down
                AddLinearMoves(validMoves, board, new Point(1, 0));  // Horizontal right
                AddLinearMoves(validMoves, board, new Point(-1, 0)); // Horizontal left
                AddLinearMoves(validMoves, board, new Point(1, 1));  // Diagonal top-right
                AddLinearMoves(validMoves, board, new Point(1, -1)); // Diagonal bottom-right
                AddLinearMoves(validMoves, board, new Point(-1, 1)); // Diagonal top-left
                AddLinearMoves(validMoves, board, new Point(-1, -1)); // Diagonal bottom-left
            }
            else if (TypeName == "King")
            {
                Point[] kingMoves = {
                                    new Point(1, 0), new Point(-1, 0), new Point(0, 1), new Point(0, -1),
                                    new Point(1, 1), new Point(1, -1), new Point(-1, 1), new Point(-1, -1)
                                    };

                foreach (var move in kingMoves)
                {
                    Point kingMove = new Point(X + move.X, Y + move.Y);
                    if (IsValidMove(kingMove, board, out bool canCapture))
                        validMoves.Add(kingMove);
                }
            }
            return validMoves;
        }

        private void AddLinearMoves(List<Point> validMoves, ChessPiece[,] board, Point direction)
        {
            Point move = new Point(X + direction.X, Y + direction.Y);
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
            if (targetPiece.PieceColor != this.PieceColor)
            {
                canCapture = true;
                return true;
            }

            return false;
        }
    }
}
