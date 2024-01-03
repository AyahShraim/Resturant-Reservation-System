using RestaurantReservation.Db.DataModels;

namespace RestaurantReservation.Db.IServices
{
    public interface IEmployeeServices
    {
        Task<IEnumerable<Employee>> ListManagersAsync();
    }
}
