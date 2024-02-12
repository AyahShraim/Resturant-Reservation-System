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

            if (matchingTableRestaurant == null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByCustomerAsync(int customerId)
        {
            var reservations = await _dbContext.Reservations
                .Where(reservation => reservation.CustomerId == customerId)
                .ToListAsync();
            return reservations;
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

        public async Task<IEnumerable<CustomerWithLargePartySizeReservation>> GetCustomersWithLargePartyReservations(int partySizeThreshold)
        {
            return await _dbContext.CustomersWithLargePartySizeReservation
                .FromSqlRaw("EXEC sp_FindCustomersWithThresholdLargerPartySize {0}", partySizeThreshold)
                .ToListAsync();
        }
    }
}
