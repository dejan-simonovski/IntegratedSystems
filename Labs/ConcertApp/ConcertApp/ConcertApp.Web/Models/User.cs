using Microsoft.AspNetCore.Identity;

namespace ConcertApp.Web.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
    }
}
