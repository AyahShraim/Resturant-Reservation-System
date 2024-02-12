using RestaurantReservation.API.Models.ErrorsHandling;

namespace RestaurantReservation.API.DomainErrors
{
    public static class CustomerErrors
    {
        public static Error NotFound(int id) => Error.NotFound(
            "Customers.NotFound", $"Customer with id {id} not found");

        public static readonly Error EmailNotUnique = Error.Conflict(
            "Customers.EmailNotUnique", $"The provided email isn't unique");
    }
}
