using RestaurantReservation.Db.DataModels;
using RestaurantReservation.Db.StoredProcedureModels;

namespace RestaurantReservation.Db.IServices
{
    public interface IReservationServices
    {
        Task<IEnumerable<Reservation>> GetReservationsByCustomerAsync(int customerId);

        Task <IEnumerable<CustomerWithLargePartySizeReservation>> GetCustomersWithLargePartyReservations(int partySizeThreshold);
    }
}
