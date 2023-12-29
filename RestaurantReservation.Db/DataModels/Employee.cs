using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.DataModels
{
    public class Employee
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public required string FirstName { get; set; }

        [MaxLength(100)]
        public required string LastName { get; set; }

        [MaxLength(50)]
        public required string Position { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public List<Order> Orders { get; set; } = new();
    }
}
