namespace Half_Chess__Razor_Server_.Models
{
    public class TblGames
    {
        public int Id { get; set; }
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
        public DateTime StartGameTime { get; set; }
        public double GameDuration { get; set; }
        public string GameMoves { get; set; }
        public bool IsWhite { get; set; }
        public string Winner { get; set; }
    }
}
