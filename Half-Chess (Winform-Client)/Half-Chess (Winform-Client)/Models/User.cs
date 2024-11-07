using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Half_Chess__Winform_Client_.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default;
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastPlayed { get; set; }
    }
}
