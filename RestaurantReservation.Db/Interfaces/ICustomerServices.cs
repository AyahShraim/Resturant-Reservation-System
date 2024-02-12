using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Interfaces
{
    public interface ICustomerServices
    {
        Task<Customer?> GetByEmailAsync(string email);
    }
}
