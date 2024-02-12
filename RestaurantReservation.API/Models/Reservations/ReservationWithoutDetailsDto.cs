namespace RestaurantReservation.API.Models.Reservations
{
    public class ReservationWithoutDetailsDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int PartySize { get; set; }
        public int RestaurantId { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
    }
}
