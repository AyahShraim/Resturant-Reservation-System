using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.API.Models.MenuItems
{
    public class MenuItemDto
    {
        [Required]
        public string Name { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }
  
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
    }
}
