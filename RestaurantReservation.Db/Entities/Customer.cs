using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entities
{
    public class Customer : BaseEntity
    {

        [MaxLength(100)]
        public  string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        public string? Email { get; set; }

        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public List<Reservation> Reservations { get; set; } = new();
    }
}
