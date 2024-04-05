﻿using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using static NuGet.Packaging.PackagingConstants;

namespace MovieApp.Models
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public double Price { get; set; }
        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }
        public virtual EShopApplicationUser? CreatedBy { get; set; }
        public ICollection<TicketInOrder>? TicketsInOrder { get; set; }

    }
}
