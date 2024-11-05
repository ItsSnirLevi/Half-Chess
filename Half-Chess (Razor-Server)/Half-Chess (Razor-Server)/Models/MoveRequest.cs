using System.Drawing;

namespace Half_Chess__Razor_Server_.Models
{
    public class MoveRequest
    {
        public string? Piece { get; set; }
        public Point CurrentPosition { get; set; }
        public Point TargetPosition { get; set; }
    }
}
