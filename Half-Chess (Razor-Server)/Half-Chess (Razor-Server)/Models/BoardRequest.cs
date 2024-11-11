using System.Drawing;

namespace Half_Chess__Razor_Server_.Models
{
    public class BoardRequest
    {
        public enum ChessColor
        {
            White,
            Black
        }

        public ChessColor PieceColor { get; set; }
        public List<ChessPiece> Board { get; set; }

        public int KingPositionX { get; set; }
        public int KingPositionY { get; set; }
    }
}
