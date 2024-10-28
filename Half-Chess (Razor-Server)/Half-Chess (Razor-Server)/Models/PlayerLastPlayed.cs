namespace Half_Chess__Razor_Server_.Models
{
    public class PlayerLastPlayed
    {
        public string Name { get; set; } = string.Empty;
        public DateTime? LastGameDate { get; set; }
    }
}
