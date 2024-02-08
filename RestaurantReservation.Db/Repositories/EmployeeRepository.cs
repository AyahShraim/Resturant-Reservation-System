using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Enums;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Utilities;

namespace RestaurantReservation.Db.Repositories
{
    public class EmployeeRepository : IRepositoryServices<Employee, string>, IEmployeeServices
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public EmployeeRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<string>> AddAsync(Employee employee)
        {
            var existingRestaurant = await _dbContext.Restaurants.FindAsync(employee.RestaurantId);

            if (existingRestaurant == null)
            {
                return OperationResult<string>.FailureResult("Invalid RestaurantId. The associated restaurant does not exist.");
            }

            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return OperationResult<string>.SuccessResult($"id: {employee.Id}");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employeeToDelete = await _dbContext.Employees.FindAsync(id);
            if (employeeToDelete != null)
            {
                _dbContext.Employees.Remove(employeeToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<OperationResult<string>> UpdateAsync(int id, Employee updatedEmployee)
        {
            var existingRestaurant = await _dbContext.Restaurants.FindAsync(updatedEmployee.RestaurantId);

            if (existingRestaurant == null)
            {
                return OperationResult<string>.FailureResult("Invalid RestaurantId. The associated restaurant does not exist.");
            }
            var rowsEffected = await _dbContext.Employees.Where(employee => employee.Id == id)
                .ExecuteUpdateAsync(updates =>
                     updates.SetProperty(employee => employee.FirstName, updatedEmployee.FirstName)
                            .SetProperty(employee => employee.LastName, updatedEmployee.LastName)
                            .SetProperty(employee => employee.Position, updatedEmployee.Position)
                            .SetProperty(employee => employee.RestaurantId, updatedEmployee.RestaurantId));
            if (rowsEffected == 0)
            {
                return OperationResult<string>.FailureResult($"No employee with id {id} found for the update.");
            }
            return OperationResult<string>.SuccessResult($"id: {id}");
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _dbContext.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> ListManagersAsync()
        {
            return await _dbContext.Employees
                .Where(employee => employee.Position == EmployeePosition.Manager)
                .ToListAsync();
        }
    }
}
