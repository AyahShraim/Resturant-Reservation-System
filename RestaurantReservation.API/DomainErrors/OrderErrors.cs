using RestaurantReservation.API.Models.ErrorsHandling;

namespace RestaurantReservation.API.DomainErrors
{
    public class OrderErrors
    {
        public static Error NotFound(int id) => Error.NotFound(
          "Orders.NotFound", $"Order with id {id} not found");
    }
}
