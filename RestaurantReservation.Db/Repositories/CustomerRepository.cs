using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Interfaces;


namespace RestaurantReservation.Db.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerServices
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public CustomerRepository(RestaurantReservationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _dbContext.Customers
                .Where(customer => customer.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}
