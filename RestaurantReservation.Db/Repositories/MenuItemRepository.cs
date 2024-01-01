using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.DataModels;
using RestaurantReservation.Db.Utilities;

namespace RestaurantReservation.Db.Repositories
{
    public class MenuItemRepository : IRepositoryServices<MenuItem, string>
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public MenuItemRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<string>> AddAsync(MenuItem menuItem)
        {
            var existingRestaurant = await _dbContext.Restaurants.FindAsync(menuItem.RestaurantId);

            if (existingRestaurant == null)
            {
                return OperationResult<string>.FailureResult("Invalid RestaurantId. The associated restaurant does not exist.");
            }

            await _dbContext.MenuItems.AddAsync(menuItem);
            await _dbContext.SaveChangesAsync();
            return OperationResult<string>.SuccessResult($"id: {menuItem.Id}");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var menuItemToDelete = await _dbContext.MenuItems.FindAsync(id);
            if (menuItemToDelete != null)
            {
                _dbContext.MenuItems.Remove(menuItemToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<OperationResult<string>> UpdateAsync(int id, MenuItem updatedMenuItem)
        {
            var existingRestaurant = await _dbContext.Restaurants.FindAsync(updatedMenuItem.RestaurantId);

            if (existingRestaurant == null)
            {
                return OperationResult<string>.FailureResult("Invalid RestaurantId. The associated restaurant does not exist.");
            }
            var rowsEffected = await _dbContext.MenuItems.Where(menuItem => menuItem.Id == id)
                .ExecuteUpdateAsync(updates =>
                     updates.SetProperty(menuItem => menuItem.Name, updatedMenuItem.Name)
                            .SetProperty(menuItem => menuItem.Description, updatedMenuItem.Description)
                            .SetProperty(menuItem => menuItem.Price, updatedMenuItem.Price)
                            .SetProperty(menuItem => menuItem.RestaurantId, updatedMenuItem.RestaurantId));
            if (rowsEffected == 0)
            {
                return OperationResult<string>.FailureResult($"No menu item with id {id} found for the update.");
            }
            return OperationResult<string>.SuccessResult($"id: {id}");
        }

        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await _dbContext.MenuItems.ToListAsync();
        }

        public async Task<MenuItem?> GetByIdAsync(int id)
        {
            return await _dbContext.MenuItems.FindAsync(id);
        }
    }
}
