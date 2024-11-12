using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Half_Chess__Winform_Client_.Models
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


        public ChessPiece(string type, ChessColor color, int x, int y, string typeName)
        {
            Type = type;
            PieceColor = color;
            X = x;
            Y = y;
            TypeName = typeName;    
        }

        public void DrawPiece(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color.Black))
            {
                Rectangle cell = GameForm.boardCells[Y, X];
                cell.Y += 10;
                g.DrawString(Type.Substring(0, 1), new Font("Arial", 40),
                             brush, cell.Location);
            }
        }

        public List<Point> CalculateValidMoves(ChessPiece[,] board, bool isClient)
        {
            List<Point> validMoves = new List<Point>();

            if (TypeName == "Pawn")
            {
                int direction = isClient ? -1 : 1;
                bool canCapture;
                if (isClient && this.Y == 6)
                {
                    Point doubleForwardMove = new Point(X, Y + 2*direction);
                    if (IsValidMove(doubleForwardMove, board, out canCapture) && !canCapture)
                        validMoves.Add(doubleForwardMove);
                } else if (!isClient && this.Y == 1)
                {
                    Point doubleForwardMove = new Point(X, Y + 2 * direction);
                    if (IsValidMove(doubleForwardMove, board, out canCapture) && !canCapture)
                        validMoves.Add(doubleForwardMove);
                }

                Point forwardMove = new Point(X, Y + direction);
                if (IsValidMove(forwardMove, board, out canCapture) && !canCapture)
                    validMoves.Add(forwardMove);

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
            else if (TypeName == "King")
            {
                validMoves = GetKingMoves(board);
                validMoves = FilterMovesThatPreventCheck(board, validMoves, isClient);
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

        public List<Point> FilterMovesThatPreventCheck(ChessPiece[,] board, List<Point> potentialMoves, bool isClient)
        {
            List<Point> safeMoves = new List<Point>();
            Point kingPosition = new Point(X, Y);

            // Iterate over all potential moves and keep only those that do not result in the king being in check
            foreach (Point move in potentialMoves)
            {
                // Save the current state
                ChessPiece originalPieceAtTarget = board[move.Y, move.X];
                Point originalPosition = new Point(this.X, this.Y);

                // Move the king temporarily
                board[this.Y, this.X] = null;
                this.X = move.X;
                this.Y = move.Y;
                board[move.Y, move.X] = this;

                // Check if the move would result in the king being in check
                bool isKingInCheckAfterMove = IsKingInCheck(board, isClient);

                // Revert the move
                board[move.Y, move.X] = originalPieceAtTarget;
                board[originalPosition.Y, originalPosition.X] = this;
                this.X = originalPosition.X;
                this.Y = originalPosition.Y;

                // If the king is not in check, add the move to the list of safe moves
                if (!isKingInCheckAfterMove)
                {
                    safeMoves.Add(move);
                }
            }

            return safeMoves;
        }

        private List<Point> GetKingMoves(ChessPiece[,] board)
        {
            Point[] kingMoves = {
                                    new Point(1, 0), new Point(-1, 0), new Point(0, 1), new Point(0, -1),
                                    new Point(1, 1), new Point(1, -1), new Point(-1, 1), new Point(-1, -1)
                                    };
            List<Point> potentialMoves = new List<Point>();

            foreach (var move in kingMoves)
            {
                Point kingMove = new Point(X + move.X, Y + move.Y);
                if (IsValidMove(kingMove, board, out bool canCapture))
                    potentialMoves.Add(kingMove);
            }
            return potentialMoves;
        }

        private bool IsKingInCheck(ChessPiece[,] board, bool isClient)
        {
            Point kingPosition = new Point(X, Y);
            // Check if any opponent's piece can move to the king's position
            foreach (ChessPiece piece in board)
            {
                if (piece != null)
                {
                    if (piece.PieceColor != PieceColor && piece.TypeName == "King")
                    {
                        List<Point> moves = piece.GetKingMoves(board);
                        if (moves.Contains(kingPosition))
                            return true;
                    } else if (piece.PieceColor != PieceColor)
                    {
                        List<Point> moves = piece.CalculateValidMoves(board, !isClient);
                        if (moves.Contains(kingPosition))
                            return true;
                    }
                }
            }
            return false;
        }

    }
}
