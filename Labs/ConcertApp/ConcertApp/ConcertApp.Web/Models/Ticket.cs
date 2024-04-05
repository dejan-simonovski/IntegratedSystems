using System.ComponentModel.DataAnnotations;

namespace ConcertApp.Web.Models
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public int NumberOfPeople { get; set; }

        public string UserId { get; set; }
        public User? User { get; set; }

        public Guid ConcertId { get; set; }
        public Concert? Concert { get; set; }
    }
}
