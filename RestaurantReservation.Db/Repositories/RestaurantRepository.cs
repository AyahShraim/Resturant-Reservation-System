using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Utilities;

namespace RestaurantReservation.Db.Repositories
{
    public class RestaurantRepository : IRepositoryServices<Restaurant, string>
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public RestaurantRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<string>> AddAsync(Restaurant restaurant)
        {
            try
            {
                await _dbContext.Restaurants.AddAsync(restaurant);
                await _dbContext.SaveChangesAsync();
                return OperationResult<string>.SuccessResult($"id:{restaurant.Id}"); ;
            }
            catch (Exception ex)
            {
                return OperationResult<string>.FailureResult($"Failed to add restaurant. {ex.Message}");
            }   
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var restaurantToDelete = await _dbContext.Restaurants.FindAsync(id);
            if (restaurantToDelete != null)
            {
                _dbContext.Restaurants.Remove(restaurantToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _dbContext.Restaurants
                .OrderBy(restaurant => restaurant.Name)
                .ToListAsync();
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            return await _dbContext.Restaurants.FindAsync(id);
        }

        public async Task<OperationResult<string>> UpdateAsync(int id, Restaurant updatedRestaurant)
        {
            var rowsEffected = await _dbContext.Restaurants.Where(restaurant => restaurant.Id == id)
                .ExecuteUpdateAsync(updates =>
                    updates.SetProperty(restaurant => restaurant.Name, updatedRestaurant.Name)
                            .SetProperty(restaurant => restaurant.Address, updatedRestaurant.Address)
                            .SetProperty(restaurant => restaurant.PhoneNumber, updatedRestaurant.PhoneNumber)
                            .SetProperty(restaurant => restaurant.OpeningHours, updatedRestaurant.OpeningHours));
            if (rowsEffected == 0)
            {
                return OperationResult<string>.FailureResult($"No restaurant with id {id} found for the update.");
            }
            return OperationResult<string>.SuccessResult($"id:{id}");
        }
    }
}
