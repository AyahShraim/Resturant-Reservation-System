using RestaurantReservation.Db.Enums;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public required string FirstName { get; set; }

        [MaxLength(100)]
        public required string LastName { get; set; }

        [MaxLength(10)]
        public required EmployeePosition Position { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;

        public List<Order> Orders { get; set; } = new();
    }
}
