using System.ComponentModel.DataAnnotations;

namespace Half_Chess__Razor_Server_.Models
{
    public class TblUsers
    {
        [Range(1, 1000, ErrorMessage = "ID must be between 1 and 1000.")]
        public int Id { get; set; }

        [MinLength(2, ErrorMessage = "First Name must have at least 2 letters.")]
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string? Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastPlayed { get; set; }
    }
}
