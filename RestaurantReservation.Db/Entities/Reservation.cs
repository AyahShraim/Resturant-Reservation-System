using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entities
{
    public class Reservation : BaseEntity
    {

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int PartySize { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int TableId { get; set; }
        public Table Table { get; set; } = null!;

        public List<Order> Orders { get; set; } = new();
    }
}
