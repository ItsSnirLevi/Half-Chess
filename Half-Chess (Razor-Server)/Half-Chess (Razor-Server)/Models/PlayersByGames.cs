namespace Half_Chess__Razor_Server_.Models
{
    public class PlayersByGames
    {
        public int GamesCount { get; set; }
        public List<TblUsers> Players { get; set; } = new List<TblUsers>();
    }
}
