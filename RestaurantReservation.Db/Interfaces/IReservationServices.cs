using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.StoredProcedureModels;

namespace RestaurantReservation.Db.Interfaces
{
    public interface IReservationServices
    {
        Task<IEnumerable<Reservation>> GetReservationsByCustomerAsync(int customerId);

        Task<bool> MatchingTableRestaurant(int restaurantId, int tableId);

        Task<IEnumerable<Order>> ListOrdersAndMenuItemsAsync(int reservationId);

        Task<IEnumerable<MenuItem>> ListOrderedMenuItemsAsync(int reservationId);

        Task<IEnumerable<CustomerWithLargePartySizeReservation>> GetCustomersWithLargePartyReservations(int partySizeThreshold);
    }
}
