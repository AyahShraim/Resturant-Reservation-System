using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Interfaces
{
    public interface IRestaurantServices
    {
        Task<Restaurant?> GetWithDetails(int id);
    }
}
