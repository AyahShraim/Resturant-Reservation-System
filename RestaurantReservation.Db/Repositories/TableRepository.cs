using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.DataModels;
using RestaurantReservation.Db.Utilities;

namespace RestaurantReservation.Db.Repositories
{
    public class TableRepository : IRepositoryServices<Table, string>
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public TableRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<string>> AddAsync(Table table)
        {
            var existingRestaurant = await _dbContext.Restaurants.FindAsync(table.RestaurantId);

            if (existingRestaurant == null)
            {
                return OperationResult<string>.FailureResult("Invalid RestaurantId. The associated restaurant does not exist.");
            }

            await _dbContext.Tables.AddAsync(table);
            await _dbContext.SaveChangesAsync();
            return OperationResult<string>.SuccessResult($"id: {table.Id}");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tableToDelete = await _dbContext.Tables.FindAsync(id);
            if (tableToDelete != null)
            {
                _dbContext.Tables.Remove(tableToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<OperationResult<string>> UpdateAsync(int id, Table updatedTable)
        {
            var existingRestaurant = await _dbContext.Restaurants.FindAsync(updatedTable.RestaurantId);

            if (existingRestaurant == null)
            {
                return OperationResult<string>.FailureResult("Invalid RestaurantId. The associated restaurant does not exist.");
            }
            var rowsEffected = await _dbContext.Tables.Where(table => table.Id == id)
                .ExecuteUpdateAsync(updates =>
                     updates.SetProperty(table => table.Capacity, updatedTable.Capacity)
                            .SetProperty(table => table.RestaurantId, updatedTable.RestaurantId));

            if (rowsEffected == 0)
            {
                return OperationResult<string>.FailureResult($"No tables with id {id} found for the update.");
            }
            return OperationResult<string>.SuccessResult($"id: {id}");
        }

        public async Task<IEnumerable<Table>> GetAllAsync()
        {
            return await _dbContext.Tables.ToListAsync();
        }

        public async Task<Table?> GetByIdAsync(int id)
        {
            return await _dbContext.Tables.FindAsync(id);
        }
    }
}
