using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.API.Models.Authentication

{
    public class AuthenticationCredentials
    {
        [Required(ErrorMessage = "Security key is required.")]
        public string Key { get; set; }
    }
}
