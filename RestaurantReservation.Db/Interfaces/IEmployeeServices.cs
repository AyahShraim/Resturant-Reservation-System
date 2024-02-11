using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Pagination;

namespace RestaurantReservation.Db.Interfaces
{
    public interface IEmployeeServices
    {
        Task<(IEnumerable<Employee>, PaginationMetaData)> ListManagersAsync(int pageNumber, int pageSize);
        Task<decimal> CalculateAverageOrderAmountAsync(int employeeId);
    }
}
