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
            using (Brush brush = new SolidBrush(Color))
            {
                Rectangle cell = GameForm.boardCells[Position.X, Position.Y];
                cell.Y += 10;
                g.DrawString(Type.Substring(0, 1), new Font("Arial", 40),
                             new SolidBrush(Color), cell.Location);
            }
        }
    }
}
