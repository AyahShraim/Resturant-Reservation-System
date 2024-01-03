using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.DataModels;
using RestaurantReservation.Db.IServices;
using RestaurantReservation.Db.StoredProcedureModels;
using RestaurantReservation.Db.Utilities;

namespace RestaurantReservation.Db.Repositories
{
    public class ReservationRepository : IRepositoryServices<Reservation, string>, IReservationServices
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public ReservationRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<string>> AddAsync(Reservation reservation)
        {
            try
            {
                var matchingTableRestaurant = await _dbContext.Tables
                    .Where(
                    table => table.Id == reservation.TableId && table.RestaurantId == reservation.RestaurantId)
                    .FirstOrDefaultAsync();

                if (matchingTableRestaurant == null)
                {
                    return OperationResult<string>.FailureResult("Invalid Reservation. The associated table does not exist or does not belong to the specified restaurant");
                }
                await _dbContext.Reservations.AddAsync(reservation);
                await _dbContext.SaveChangesAsync();
                return OperationResult<string>.SuccessResult($"id: {reservation.Id}");
            }
            catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
            {
                return OperationResult<string>.FailureResult("Foreign key constraint violation. Make sure associated entities exist.");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.FailureResult($"An error occurred while processing the reservation.{ex.Message}");
            }
            bool IsForeignKeyViolation(DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;

                return sqlException?.Number == 547;
            }
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var reservationToDelete = await _dbContext.Reservations.FindAsync(id);
            if (reservationToDelete != null)
            {
                _dbContext.Reservations.Remove(reservationToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<OperationResult<string>> UpdateAsync(int id, Reservation updatedReservation)
        {
            try
            {
                var rowsEffected = await _dbContext.Reservations.Where(reservation => reservation.Id == id)
                .ExecuteUpdateAsync(updates =>
                     updates.SetProperty(reservation => reservation.Date, updatedReservation.Date)
                            .SetProperty(reservation => reservation.PartySize, updatedReservation.PartySize)
                            .SetProperty(reservation => reservation.CustomerId, updatedReservation.CustomerId)
                            .SetProperty(reservation => reservation.TableId, updatedReservation.TableId)
                            .SetProperty(reservation => reservation.RestaurantId, updatedReservation.RestaurantId));
                if (rowsEffected == 0)
                {
                    return OperationResult<string>.FailureResult($"No reservation with id {id} found for the update.");
                }
                return OperationResult<string>.SuccessResult($"id: {id}");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.FailureResult($"An error occurred while updating the reservation.{ex.Message}");
            }
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _dbContext.Reservations.ToListAsync();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _dbContext.Reservations.FindAsync(id);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByCustomerAsync(int customerId)
        {
            var reservations = await _dbContext.Reservations
                .Where(reservation => reservation.CustomerId == customerId)
                .Include(r => r.Customer)
                .ToListAsync();

            return reservations;
        }

        public async Task<IEnumerable<CustomerWithLargePartySizeReservation>> GetCustomersWithLargePartyReservations(int partySizeThreshold)
        {
            return await _dbContext.CustomersWithLargePartySizeReservation
                .FromSqlRaw("EXEC sp_FindCustomersWithThresholdLargerPartySize {0}", partySizeThreshold)
                .ToListAsync();
        }
    }
}
