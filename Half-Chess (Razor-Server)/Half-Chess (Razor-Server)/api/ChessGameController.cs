using Half_Chess__Razor_Server_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Half_Chess__Razor_Server_.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChessGameController : ControllerBase
    {
        // POST: api/ChessGame
        [HttpPost]
        public IActionResult GetValidMove([FromBody] BoardRequest request)
        {
            ChessPiece[,] board = new ChessPiece[8, 4];

            foreach (var piece in request.Board)
            {
                if (piece.Y >= 0 && piece.Y < 8 && piece.X >= 0 && piece.X < 4)
                {
                    board[piece.Y, piece.X] = piece;
                }
            }

            List<CustomPoint> validMoves = GetRandomMove(board, request.PieceColor, request.KingPositionX, request.KingPositionY);
            return Ok(validMoves);
        }

        public List<CustomPoint> GetRandomMove(ChessPiece[,] board, BoardRequest.ChessColor pieceColor, int kingX, int kingY)
        {
            CustomPoint kingPos = new CustomPoint { X = kingX, Y = kingY };

            ChessPiece.ChessColor color = (pieceColor == BoardRequest.ChessColor.White) ? ChessPiece.ChessColor.White : ChessPiece.ChessColor.Black;
            Random random = new Random();
            List<ChessPiece> piecesOfColor = new List<ChessPiece>();

            // Add a random delay between 1 and 3 seconds
            //int delay = random.Next(1, 4);
            //Thread.Sleep(delay * 1000);

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    ChessPiece piece = board[y, x];
                    if (piece != null && piece.PieceColor == color)
                    {
                        piecesOfColor.Add(piece);
                    }
                }
            }

            if (piecesOfColor.Count == 0) return null;

            piecesOfColor = piecesOfColor.OrderBy(_ => random.Next()).ToList();

            // Loop through shuffled pieces until we find one with valid moves
            foreach (var selectedPiece in piecesOfColor)
            {
                List<Point> validMoves = selectedPiece.CalculateValidMoves(board); // selectedPiece.CalculateValidMoves(board); 
                validMoves = FilterMovesThatResolveCheck(validMoves, selectedPiece, board, pieceColor, kingPos);

                if (validMoves.Count > 0)
                {
                    Point chosenMove = validMoves[random.Next(validMoves.Count)];

                    return new List<CustomPoint>
                            {
                                new CustomPoint { X = selectedPiece.X, Y = selectedPiece.Y },  // Origin
                                new CustomPoint { X = chosenMove.X, Y = chosenMove.Y }          // Target
                            };
                }
            }

            // If no valid moves are found for any piece, return null
            return null;
        }

        private List<Point> FilterMovesThatResolveCheck(List<Point> potentialMoves, ChessPiece piece, ChessPiece[,] board, BoardRequest.ChessColor pieceColor, CustomPoint kingPos)
        {
            List<Point> validMoves = new List<Point>();

            foreach (Point move in potentialMoves)
            {
                ChessPiece originalPieceAtTarget = board[move.Y, move.X];
                Point originalPosition = new Point(piece.X, piece.Y);

                // Move piece temporarily
                board[piece.Y, piece.X] = null;
                piece.X = move.X;
                piece.Y = move.Y;
                board[move.Y, move.X] = piece;

                if (piece.TypeName == "King")
                {
                    kingPos = new CustomPoint { X = piece.X, Y = piece.Y };
                }

                bool isKingInCheckAfterMove = IsKingInCheck(board, piece.PieceColor, kingPos);

                // Revert the move
                board[move.Y, move.X] = originalPieceAtTarget;
                board[originalPosition.Y, originalPosition.X] = piece;
                piece.X = originalPosition.X;
                piece.Y = originalPosition.Y;

                if (piece.TypeName == "King")
                {
                    kingPos = new CustomPoint { X = piece.X, Y = piece.Y };
                }

                if (!isKingInCheckAfterMove)
                {
                    validMoves.Add(move);
                }
            }

            return validMoves;
        }

        private bool IsKingInCheck(ChessPiece[,] board, ChessPiece.ChessColor kingColor, CustomPoint kingPos)
        {
            Point kingPosition = new Point(kingPos.X, kingPos.Y);
            // Check if any opponent's piece can move to the king's position
            foreach (ChessPiece piece in board)
            {
                if (piece != null && piece.PieceColor != kingColor)
                {
                    List<Point> moves = piece.CalculateValidMoves(board);
                    if (moves.Contains(kingPosition))
                        return true;
                }
            }
            return false;
        }

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            return Ok("Connection successful!");
        }


    }
}
