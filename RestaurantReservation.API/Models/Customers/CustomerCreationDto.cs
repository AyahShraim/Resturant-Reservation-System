using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.API.Models.Customers
{
    public class CustomerCreationDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
    }
}
