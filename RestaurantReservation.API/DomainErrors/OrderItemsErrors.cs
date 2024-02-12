using RestaurantReservation.API.Models.ErrorsHandling;

namespace RestaurantReservation.API.DomainErrors
{
    public class OrderItemsErrors
    {
        public static Error NotFound(int id) => Error.NotFound(
       "OrderItems.NotFound", $"Order Item with id {id} not found");
    }
}
