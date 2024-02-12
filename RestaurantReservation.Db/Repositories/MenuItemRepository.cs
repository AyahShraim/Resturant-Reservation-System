using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories
{
    public class MenuItemRepository : Repository<MenuItem>
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public MenuItemRepository(RestaurantReservationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
