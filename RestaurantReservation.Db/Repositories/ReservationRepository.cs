using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.StoredProcedureModels;

namespace RestaurantReservation.Db.Repositories
{
    public class ReservationRepository : Repository<Reservation>, IReservationServices
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public ReservationRepository(RestaurantReservationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> MatchingTableRestaurant(int restaurantId, int tableId)
        {
            var matchingTableRestaurant = await _dbContext.Tables
                .Where(
                table => table.Id == tableId && table.RestaurantId == restaurantId)
                .FirstOrDefaultAsync();  

            if(matchingTableRestaurant == null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByCustomerAsync(int customerId)
        {
            var reservations = await _dbContext.Reservations
                .Where(reservation => reservation.CustomerId == customerId)
                .Include(r => r.Customer)
                .ToListAsync();

            return reservations;
        }

        public async Task<IEnumerable<CustomerWithLargePartySizeReservation>> GetCustomersWithLargePartyReservations(int partySizeThreshold)
        {
            return await _dbContext.CustomersWithLargePartySizeReservation
                .FromSqlRaw("EXEC sp_FindCustomersWithThresholdLargerPartySize {0}", partySizeThreshold)
                .ToListAsync();
        }
    }
}
