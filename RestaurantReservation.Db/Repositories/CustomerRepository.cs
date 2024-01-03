using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.DataModels;
using RestaurantReservation.Db.Utilities;

namespace RestaurantReservation.Db.Repositories
{
    public class CustomerRepository : IRepositoryServices<Customer,string>
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public CustomerRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<string>> AddAsync(Customer customer)
        {
            try
            {
                await _dbContext.Customers.AddAsync(customer);
                await _dbContext.SaveChangesAsync();

                return OperationResult<string>.SuccessResult($"id:{customer.Id}");
            }
            catch (Exception ex)
            {
                return OperationResult<string>.FailureResult($"Failed to add customer. {ex.Message}");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customerToDelete = await _dbContext.Customers.FindAsync(id);
            if(customerToDelete != null)
            {
                 _dbContext.Customers.Remove(customerToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _dbContext.Customers.ToListAsync();    
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }

        public async Task<OperationResult<string>> UpdateAsync(int id, Customer updatedCustomer)
        {
            var rowsEffected = await _dbContext.Customers.Where(customer => customer.Id == id)
                .ExecuteUpdateAsync(updates =>
                    updates.SetProperty(customer => customer.FirstName, updatedCustomer.FirstName)
                            .SetProperty(customer => customer.LastName, updatedCustomer.LastName)
                            .SetProperty(customer => customer.Email, updatedCustomer.Email)
                            .SetProperty(customer => customer.PhoneNumber, updatedCustomer.PhoneNumber));
            if (rowsEffected == 0)
            {
                return OperationResult<string>.FailureResult($"No customer with id {id} found for the update.");
            }
            return OperationResult<string>.SuccessResult($"id:{id}");
        }
    
    }
}
