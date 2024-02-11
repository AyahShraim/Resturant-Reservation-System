using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entities
{
    public class Restaurant : BaseEntity
    {

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string OpeningHours { get; set; }

        public List<Table> Tables { get; set; } = new();
        public List<Reservation> Reservations { get; set; } = new();
        public List<Employee> Employees { get; set; } = new();
        public List<MenuItem> MenuItems { get; set; } = new();
    }
}
