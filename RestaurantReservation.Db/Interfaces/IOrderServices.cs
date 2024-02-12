using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Interfaces
{
    public interface IOrderServices
    {
        

        Task<IEnumerable<MenuItem>> ListOrderedMenuItemsAsync(int reservationId);
    }
}

