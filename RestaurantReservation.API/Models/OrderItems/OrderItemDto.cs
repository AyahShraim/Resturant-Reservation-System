using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.API.Models.OrderItems
{
    public class OrderItemDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 1")]
        public int Quantity { get; set; }

        public int OrderId { get; set; }

        public int MenuItemId { get; set; }
    }
}
