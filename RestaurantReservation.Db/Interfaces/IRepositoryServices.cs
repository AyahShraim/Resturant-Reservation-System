using RestaurantReservation.Db.Utilities;

namespace RestaurantReservation.Db.Interfaces
{
    public interface IRepositoryServices<T, TResult>
    {
        Task<OperationResult<TResult>> AddAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);

        Task<OperationResult<TResult>> UpdateAsync(int id, T entity);
    }
}
