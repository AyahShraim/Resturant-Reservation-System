using RestaurantReservation.Db.DataModels;
using RestaurantReservation.Db.IServices;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.StoredProcedureModels;
using RestaurantReservationApp.Utilities;

namespace RestaurantReservationApp.Tests.RepositoryInvokers_SampleData
{
    public class ReservationRepositoryTest
    {
        private readonly IRepositoryServices<Reservation, string> _reservationRepository;
        private readonly IReservationServices _reservationServices;

        public ReservationRepositoryTest(IRepositoryServices<Reservation, string> reservationRepository, IReservationServices reservationServices)
        {
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _reservationServices = reservationServices ?? throw new ArgumentNullException(nameof(reservationServices));
        }

        public async Task TestGetAllAsync()
        {
            Console.WriteLine("\n\nTesting Get All Reservations...");
            var reservations = await _reservationRepository.GetAllAsync();
            var pageSize = 4;
            PaginationConsoleTable<Reservation>.Paginate(reservations, pageSize);
        }

        public async Task TestGetByIdAsync()
        {
            Console.WriteLine("\n\nTesting Get Reservation by Id...");

            int reservationToGetId = 11;
            var reservation = await _reservationRepository.GetByIdAsync(reservationToGetId);
            if (reservation != null)
            {
                var pageSize = 1;
                PaginationConsoleTable<Reservation>.Paginate(new List<Reservation> { reservation! }, pageSize);
            }
            else
            {
                Console.WriteLine($"\nNo such reservation with id:{reservationToGetId}");
            }
        }

        public async Task TestAddAsync()
        {
            Console.WriteLine("\n\nTesting Add Reservation...");
            var reservation = new Reservation
            {
                Date = DateTime.Now.AddDays(20),
                PartySize = 2,
                CustomerId = 1,
                TableId = 2,
                RestaurantId = 1,
            };
            var result = await _reservationRepository.AddAsync(reservation);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nReservation with {result.Message} added successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestUpdateAsync()
        {
            Console.WriteLine("\n\nTesting Update Reservation...");

            var updatedReservation = new Reservation
            {
                Date = DateTime.Now.AddDays(20),
                PartySize = 13,
                CustomerId = 200,
                TableId = 27,
                RestaurantId = 1,
            };

            int reservationToUpdateId = 1;
            var result = await _reservationRepository.UpdateAsync(reservationToUpdateId, updatedReservation);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nReservation with {result.Message} updated successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestDeleteAsync()
        {
            Console.WriteLine("\n\nTesting Delete Reservation...");

            int reservationToDeleteId = 13;
            var deleteResult = await _reservationRepository.DeleteAsync(reservationToDeleteId);
            if (deleteResult)
            {
                Console.WriteLine($"\nReservation with id:{reservationToDeleteId} deleted successfully");
            }
            else
            {
                Console.WriteLine($"\nNo Reservation with id:{reservationToDeleteId} found!");
            }
        }

        public async Task TestGetReservationByCustomerIdAsync()
        {
            Console.WriteLine("\n\nTesting Get Reservations By Customer Id ...");
            int customerId = 1;
            var reservations = await _reservationServices.GetReservationsByCustomerAsync(customerId);
            if (reservations.Any())
            {
                var pageSize = 4;
                PaginationConsoleTable<Reservation>.Paginate(reservations, pageSize);
            }
            else
            {
                Console.WriteLine($"\nNo reservations found for customer with id:{customerId}");
            }
        }
        public async Task TestCustomerWithLargePartySizeReservation()
        {
            Console.WriteLine("\n\nTesting Stored procedure >> Finding all customers who have made reservations with a party size greater than a certain value ...");
            int partySizeThreshold = 5;
            var customers = await _reservationServices.GetCustomersWithLargePartyReservations(partySizeThreshold);
            if (customers.Any())
            {
                var pageSize = 4;
                PaginationConsoleTable<CustomerWithLargePartySizeReservation>.Paginate(customers, pageSize);
            }
            else
            {
                Console.WriteLine($"\nNo customer reservation found with party size > {partySizeThreshold}");
            }
        }
    }
}
