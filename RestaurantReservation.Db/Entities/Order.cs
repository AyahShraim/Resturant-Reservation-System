using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Db.Entities
{
    public class Order : BaseEntity
    {

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
