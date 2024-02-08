using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Utilities;

namespace RestaurantReservation.Db.Repositories
{
    public class OrderItemRepository : IRepositoryServices<OrderItem, string>
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public OrderItemRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<string>> AddAsync(OrderItem orderItem)
        {
            try
            {
                await _dbContext.OrderItems.AddAsync(orderItem);
                await _dbContext.SaveChangesAsync();
                return OperationResult<string>.SuccessResult($"id:{orderItem.Id}");
            }
            catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
            {
                return OperationResult<string>.FailureResult("Foreign key constraint violation. Make sure associated entities exist.");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.FailureResult($"An error occurred while processing the reservation.{ex.Message}");
            }
            bool IsForeignKeyViolation(DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;

                return sqlException?.Number == 547;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var orderItemToDelete = await _dbContext.OrderItems.FindAsync(id);
            if (orderItemToDelete != null)
            {
                _dbContext.OrderItems.Remove(orderItemToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<OperationResult<string>> UpdateAsync(int id, OrderItem updatedOrderItem)
        {
            try
            {
                var rowsEffected = await _dbContext.OrderItems
                    .Where(orderItem => orderItem.Id == id)
                    .ExecuteUpdateAsync(updates =>
                        updates.SetProperty(orderItem => orderItem.Quantity, updatedOrderItem.Quantity)
                                .SetProperty(orderItem => orderItem.OrderId, updatedOrderItem.OrderId)
                                .SetProperty(orderItem => orderItem.MenuItemId, updatedOrderItem.MenuItemId));
                if (rowsEffected == 0)
                {
                    return OperationResult<string>.FailureResult($"No order item with id {id} found for the update.");
                }

                return OperationResult<string>.SuccessResult($"id: {id}");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.FailureResult($"An unexpected error occurred. {ex.Message}");
            }
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _dbContext.OrderItems.ToListAsync();
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            return await _dbContext.OrderItems.FindAsync(id);
        }
    }
}
