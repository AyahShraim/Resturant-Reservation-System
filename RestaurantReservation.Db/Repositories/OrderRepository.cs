using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Utilities;

namespace RestaurantReservation.Db.Repositories
{
    public class OrderRepository : IRepositoryServices<Order, string>, IOrderServices
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public OrderRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<string>> AddAsync(Order order)
        {
            try
            {
                await _dbContext.Orders.AddAsync(order);
                await _dbContext.SaveChangesAsync();
                return OperationResult<string>.SuccessResult($"id:{order.Id}");
            }
            catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
            {
                return OperationResult<string>.FailureResult("Foreign key constraint violation. Make sure associated entities exist.");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.FailureResult($"An error occurred while processing the order.{ex.Message}");
            }

            bool IsForeignKeyViolation(DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;

                return sqlException?.Number == 547;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var orderToDelete = await _dbContext.Orders.FindAsync(id);
            if (orderToDelete != null)
            {
                _dbContext.Orders.Remove(orderToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<OperationResult<string>> UpdateAsync(int id, Order updatedOrder)
        {
            try
            {
                var rowsEffected = await _dbContext.Orders
                    .Where(order => order.Id == id)
                    .ExecuteUpdateAsync(updates =>
                        updates.SetProperty(order => order.Date, updatedOrder.Date)
                               .SetProperty(order => order.TotalAmount, updatedOrder.TotalAmount)
                               .SetProperty(order => order.EmployeeId, updatedOrder.EmployeeId)
                               .SetProperty(order => order.ReservationId, updatedOrder.ReservationId));
                if (rowsEffected == 0)
                {
                    return OperationResult<string>.FailureResult($"No order with id {id} found for the update.");
                }

                return OperationResult<string>.SuccessResult($"id: {id}");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.FailureResult($"An unexpected error occurred. {ex.Message}");
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> ListOrdersAndMenuItemsAsync(int reservationId)
        {
            var orders = await _dbContext.Orders
                .Where(order => order.ReservationId == reservationId)
                .Include(order => order.OrderItems)
                .ThenInclude(orderItem => orderItem.MenuItem)
                .ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<MenuItem>> ListOrderedMenuItemsAsync(int reservationId)
        {
            var orderedMenuItems = await _dbContext.OrderItems
                .Where(orderItem => orderItem.Order.ReservationId == reservationId)
                .Include(orderItem => orderItem.MenuItem)
                .Select(orderItem => orderItem.MenuItem)
                .Distinct()
                .ToListAsync();
            return orderedMenuItems;
        }

        public async Task<decimal> CalculateAverageOrderAmountAsync(int employeeId)
        {
            var averageOrderAmount = await _dbContext.Orders
                .Where(order => order.EmployeeId == employeeId)
                .AverageAsync(order => (decimal?)order.TotalAmount);
               
            return averageOrderAmount.HasValue? averageOrderAmount.Value : 0 ;
        }
    }
}
