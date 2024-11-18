namespace Half_Chess__Razor_Server_.Models
{
    public class PlayersByCountry
    {
        public string Country { get; set; } = string.Empty;
        public List<TblUsers> Players { get; set; } = new List<TblUsers>();
    }
}
