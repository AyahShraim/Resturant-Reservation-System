using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.API.Models.Restaurants
{
    public class RestaurantWithoutDetailsDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required] 
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]  
        public string OpeningHours { get; set; }
    }
}
