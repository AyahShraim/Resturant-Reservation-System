using RestaurantReservation.API.Models.ErrorsHandling;

namespace RestaurantReservation.API.DomainErrors
{
    public class EmployeeErrors
    {
        public static Error NotFound(int id) => Error.NotFound(
           "Employees.NotFound", $"Employee with id {id} not found");

    }
}
