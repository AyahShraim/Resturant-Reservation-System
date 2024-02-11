using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Enum;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Pagination;


namespace RestaurantReservation.Db.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeServices
    {
        private readonly RestaurantReservationDbContext _dbContext;
        private const int DefaultPageSize = 10;
        public EmployeeRepository(RestaurantReservationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(IEnumerable<Employee>, PaginationMetaData)> ListManagersAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = DefaultPageSize;

            var entities = _dbContext.Employees
                            .Where(employee => employee.Position == EmployeePosition.Manager);

            var totalRecords = await entities.CountAsync();

            var paginationMetaData = new PaginationMetaData(totalRecords, pageSize, pageNumber);

            var managers = await entities
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (managers, paginationMetaData);
        }

        public async Task<decimal> CalculateAverageOrderAmountAsync(int employeeId)
        {
            var averageOrderAmount = await _dbContext.Orders
                .Where(order => order.EmployeeId == employeeId)
                .AverageAsync(order => (decimal?)order.TotalAmount);

            return averageOrderAmount.HasValue ? averageOrderAmount.Value : 0;
        }
    }
}
