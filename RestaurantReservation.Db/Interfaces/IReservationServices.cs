using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.StoredProcedureModels;

namespace RestaurantReservation.Db.Interfaces
{
    public interface IReservationServices
    {
        Task<IEnumerable<Reservation>> GetReservationsByCustomerAsync(int customerId);

        Task<IEnumerable<CustomerWithLargePartySizeReservation>> GetCustomersWithLargePartyReservations(int partySizeThreshold);
    }
}
