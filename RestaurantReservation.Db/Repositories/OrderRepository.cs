using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Interfaces;

namespace RestaurantReservation.Db.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderServices
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public OrderRepository(RestaurantReservationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
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
    }
}
