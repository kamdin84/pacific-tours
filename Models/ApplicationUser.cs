using Microsoft.AspNetCore.Identity;

namespace ccse_cw1.Models
{
    public class ApplicationUser : IdentityUser //Add namespace on top: using Microsoft.AspNetCore.Identity
    {

        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Address { get; set; } = "";
        public DateTime CreatedAt { get; set; }

        public ICollection<Booking>? Bookings { get; set; }

    }
}