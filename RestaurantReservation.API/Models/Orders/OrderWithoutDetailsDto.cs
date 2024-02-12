namespace RestaurantReservation.API.Models.Orders
{
    public class OrderWithoutDetailsDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public int EmployeeId { get; set; }
        public int ReservationId { get; set; }
    }
}
