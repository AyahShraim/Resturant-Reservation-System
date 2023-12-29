using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Db.DataModels
{
    public class Order
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public required DateTime Date { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
