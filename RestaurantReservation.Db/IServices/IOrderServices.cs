using RestaurantReservation.Db.DataModels;

namespace RestaurantReservation.Db.IServices
{
    public interface IOrderServices
    {
        Task<IEnumerable<Order>> ListOrdersAndMenuItemsAsync(int reservationId);

        Task<IEnumerable<MenuItem>> ListOrderedMenuItemsAsync(int reservationId);

        Task<decimal> CalculateAverageOrderAmountAsync(int employeeId);
    }
}

