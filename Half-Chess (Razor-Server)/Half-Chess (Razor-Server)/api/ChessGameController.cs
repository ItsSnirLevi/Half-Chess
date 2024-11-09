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

            List<CustomPoint> validMoves = GetRandomMove(board, request.PieceColor);
            return Ok(validMoves);
        }

        public List<CustomPoint> GetRandomMove(ChessPiece[,] board, BoardRequest.ChessColor pieceColor)
        {
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
                List<Point> validMoves = selectedPiece.CalculateValidMoves(board);

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

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            return Ok("Connection successful!");
        }


    }
}
