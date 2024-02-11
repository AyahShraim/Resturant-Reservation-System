namespace RestaurantReservation.API.Models.Tables
{
    public class TableWithoutDetailsDto
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public int RestaurantId { get; set; }
    }
}
