using Half_Chess__Winform_Client_.Models;
using System.Collections.Generic;

namespace Half_Chess__Winform_Client_.Models
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
    }
}