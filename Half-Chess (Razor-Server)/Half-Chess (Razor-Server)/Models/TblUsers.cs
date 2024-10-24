namespace Half_Chess__Razor_Server_.Models
{
    public class TblUsers
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Country { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
