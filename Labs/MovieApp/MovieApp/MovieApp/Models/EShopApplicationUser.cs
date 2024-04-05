using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace MovieApp.Models
{
    public class EShopApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Ticket>? MyTickets { get; set; }
        public Order? UserOrder { get; set; }
    }

}
