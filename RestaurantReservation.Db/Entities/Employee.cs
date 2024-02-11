using RestaurantReservation.Db.Enum;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entities
{
    public class Employee : BaseEntity
    {

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(10)]
        public EmployeePosition Position { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;

        public List<Order> Orders { get; set; } = new();
    }
}
