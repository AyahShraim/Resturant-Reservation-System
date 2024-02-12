using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Interfaces
{
    public interface IOrderServices
    {
        Task<IEnumerable<Order>> ListOrdersAndMenuItemsAsync(int reservationId);

        Task<IEnumerable<MenuItem>> ListOrderedMenuItemsAsync(int reservationId);
    }
}

