using RestaurantReservation.API.Models.ErrorsHandling;

namespace RestaurantReservation.API.DomainErrors
{
    public class TableErrors
    {
        public static Error NotFound(int id) => Error.NotFound(
           "Table.NotFound", $"Table with id {id} is not found");
    }
}
