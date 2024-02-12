using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Pagination;

namespace RestaurantReservation.Db.Interfaces
{
    public interface IRepositoryServices<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);

        Task<(IEnumerable<T>, PaginationMetaData)> GetAllAsync(int pageNumber, int pageSize);

        Task<T?> GetByIdAsync(int id);

        Task<bool> DeleteAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> Save();

        Task<bool> ExistsAsync(int id);
    }
}
