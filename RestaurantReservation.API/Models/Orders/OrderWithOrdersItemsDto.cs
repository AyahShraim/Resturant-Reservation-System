using RestaurantReservation.API.Models.OrderItems;

namespace RestaurantReservation.API.Models.Orders
{
    public class OrderWithOrdersItemsDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemWithMenuItemDto> OrderItems { get; set; } = new List<OrderItemWithMenuItemDto>();  
    }
}
