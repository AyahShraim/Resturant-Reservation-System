using RestaurantReservation.API.Models.MenuItems;

namespace RestaurantReservation.API.Models.OrderItems
{
    public class OrderItemWithMenuItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int MenuItemId { get; set; }
        public MenuItemDto MenuItem { get; set; } = new MenuItemDto();
    }
}
