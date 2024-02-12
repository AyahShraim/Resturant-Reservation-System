using RestaurantReservation.API.Models.ErrorsHandling;

namespace RestaurantReservation.API.DomainErrors
{
    public class MenuItemsErrors
    {
        public static Error NotFound(int id) => Error.NotFound(
          "MenuItems.NotFound", $"MenuItem with id {id} not found");
    }
}
