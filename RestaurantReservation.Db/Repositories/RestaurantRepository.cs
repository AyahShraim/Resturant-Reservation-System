using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Interfaces;

namespace RestaurantReservation.Db.Repositories
{
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantServices
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public RestaurantRepository(RestaurantReservationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Restaurant?> GetWithDetails(int id)
        {
            return await _dbContext.Restaurants
              //  .Include(restaurant => restaurant.Tables)
                .Include(restaurant => restaurant.Reservations)
                .Include(restaurant => restaurant.MenuItems)
                .Include(restaurant => restaurant.Reservations)   
                .FirstOrDefaultAsync(restaurant => restaurant.Id == id);
        }

    }
}
