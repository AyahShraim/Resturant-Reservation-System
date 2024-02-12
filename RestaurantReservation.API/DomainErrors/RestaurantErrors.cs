using RestaurantReservation.API.Models.ErrorsHandling;

namespace RestaurantReservation.API.DomainErrors
{
    public static class RestaurantErrors
    {
        public static Error NotFound(int id) => Error.NotFound(
            "Restaurant.NotFound", $"Restaurant with id {id} not found");

        public static readonly Error EmailNotUnique = Error.Conflict(
            "Restaurant.EmailNotUnique", $"The provided email isn't unique");
    }
}
