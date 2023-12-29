using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.DataModels
{
    public class Reservation
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public required DateTime Date { get; set; }

        public int PartySize { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int TableId { get; set; }
        public Table Table { get; set; }

        public List<Order> Orders { get; set; } = new();
    }
}
