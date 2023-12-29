using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.DataModels
{
    public class Restaurant
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(255)]
        public required string Address { get; set; }

        [Phone]
        [MaxLength(15)]
        public required string PhoneNumber { get; set; }

        [MaxLength(100)]
        public required string OpeningHours { get; set; }

        public List<Table> Tables { get; set; } = new();
        public List<Reservation> Reservations { get; set; } = new();
        public List<Employee> Employees { get; set; } = new();
        public List<MenuItem> MenuItems { get; set; } = new();
    }
}
