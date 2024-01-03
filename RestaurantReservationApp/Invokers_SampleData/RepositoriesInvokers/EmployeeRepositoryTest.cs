using RestaurantReservation.Db.DataModels;
using RestaurantReservation.Db.Enums;
using RestaurantReservation.Db.IServices;
using RestaurantReservation.Db.Repositories;
using RestaurantReservationApp.Utilities;

namespace RestaurantReservationApp.Tests.RepositoryInvokers_SampleData
{
    public class EmployeeRepositoryTest
    {
        private readonly IRepositoryServices<Employee, string> _employeeRepository;
        private readonly IEmployeeServices _employeeServices;

        public EmployeeRepositoryTest(IRepositoryServices<Employee, string> employeeRepository, IEmployeeServices employeeServices)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _employeeServices = employeeServices ?? throw new ArgumentNullException(nameof(employeeServices));
        }

        public async Task TestGetAllAsync()
        {
            Console.WriteLine("\n\nTesting Get All Employees...");
            var employees = await _employeeRepository.GetAllAsync();
            var pageSize = 4;
            PaginationConsoleTable<Employee>.Paginate(employees, pageSize);
        }

        public async Task TestGetByIdAsync()
        {
            Console.WriteLine("\n\nTesting Get Employee by Id...");

            int employeeToGetId = 1;
            var employee = await _employeeRepository.GetByIdAsync(employeeToGetId);
            if (employee != null)
            {
                var pageSize = 1;
                PaginationConsoleTable<Employee>.Paginate(new List<Employee> { employee! }, pageSize);
            }
            else
            {
                Console.WriteLine($"\nNo such employee with id:{employeeToGetId}");
            }
        }

        public async Task TestAddAsync()
        {
            Console.WriteLine("\n\nTesting Add Employee...");
            var employee = new Employee
            {
                FirstName = "sample",
                LastName = "employee",
                Position = EmployeePosition.Chef,
                RestaurantId = 6,
            };
            var result = await _employeeRepository.AddAsync(employee);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nEmployee with {result.Message} added successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestUpdateAsync()
        {
            Console.WriteLine("\n\nTesting Update Employee...");

            var updatedEmployee = new Employee
            {
                FirstName = "newFName",
                LastName = "newLName",
                Position = EmployeePosition.Cashier,
                RestaurantId = 8,
            };

            int employeeToUpdateId = 19;
            var result = await _employeeRepository.UpdateAsync(employeeToUpdateId, updatedEmployee);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nEmployee with {result.Message} updated successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestDeleteAsync()
        {
            Console.WriteLine("\n\nTesting Delete Employee...");

            int employeeToDeleteId = 20;
            var deleteResult = await _employeeRepository.DeleteAsync(employeeToDeleteId);
            if (deleteResult)
            {
                Console.WriteLine($"\nEmployee with id:{employeeToDeleteId} deleted successfully");
            }
            else
            {
                Console.WriteLine($"\nNo Restaurant with id:{employeeToDeleteId} found!");
            }
        }

        public async Task TestListManagerAsync()
        {
            Console.WriteLine("\n\nTesting List Managers...");
            var managers = await _employeeServices.ListManagersAsync();
            var pageSize = 8;
            PaginationConsoleTable<Employee>.Paginate(managers, pageSize);

        }
    }
}
