using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace ConcertApp.Web.Models
{
    public class Concert
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? ConcertName { get; set; }
        [Required]
        public DateTime? ConcertDate { get; set; }
        [Required]
        public double ConcertPrice { get; set; }
        [Required]
        public string ConcertLocation { get; set; }
        public string? ConcertImgUrl { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
    }
}
