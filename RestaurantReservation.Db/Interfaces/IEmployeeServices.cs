using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Interfaces
{
    public interface IEmployeeServices
    {
        Task<IEnumerable<Employee>> ListManagersAsync();
    }
}
