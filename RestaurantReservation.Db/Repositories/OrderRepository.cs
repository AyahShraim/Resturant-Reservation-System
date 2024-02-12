using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public OrderRepository(RestaurantReservationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
