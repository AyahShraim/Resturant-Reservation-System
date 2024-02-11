using RestaurantReservation.Db.Interfaces;
using Table = RestaurantReservation.Db.Entities.Table;

namespace RestaurantReservation.Db.Repositories
{
    public class TableRepository : Repository<Table>, ITableServices
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public TableRepository(RestaurantReservationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
