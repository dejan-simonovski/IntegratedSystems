namespace MovieApp.Models.DTO
{
    public class AddToOrderDTO
    {
        public Guid SelectedTicketId { get; set; }
        public string? SelectedTicketName { get; set; }
        public int Quantity { get; set; }

    }
}
