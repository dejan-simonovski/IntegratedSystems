using ConcertApp.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConcertApp.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<ConcertApp.Web.Models.Concert> Concert { get; set; }
        public virtual DbSet<ConcertApp.Web.Models.Ticket> Ticket { get; set; }
    }
}
