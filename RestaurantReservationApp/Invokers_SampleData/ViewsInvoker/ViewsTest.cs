using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;
using RestaurantReservation.Db.ViewsModels;
using RestaurantReservationApp.Utilities;

namespace RestaurantReservationApp.Tests.ViewsTest
{
    public class ViewsTest
    {

        private readonly RestaurantReservationDbContext _dbContext;

        public ViewsTest(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task TestReservationsWithDetailsAsync()
        {
            Console.WriteLine("\n\nTesting Reservations With Details View...");
            var reservationsDetails = await _dbContext.ReservationsDetails.ToListAsync();
            var pageSize = 4;
            PaginationConsoleTable<ReservationDetails>.Paginate(reservationsDetails, pageSize);
        }

        public async Task TestEmployeesWithRestaurantDetailsAsync()
        {
            Console.WriteLine("\n\nTesting Employees With Respective restaurants View...");
            var employeesRestaurantDetails = await _dbContext.EmployeesWithRestaurantDetails.ToListAsync();
            var pageSize = 4;
            PaginationConsoleTable<EmployeesWithRestaurantDetails>.Paginate(employeesRestaurantDetails, pageSize);
        }
    }
}
