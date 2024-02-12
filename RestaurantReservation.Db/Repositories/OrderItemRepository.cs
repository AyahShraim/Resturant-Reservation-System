using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public OrderItemRepository(RestaurantReservationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
