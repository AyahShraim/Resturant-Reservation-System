using RestaurantReservation.Db.DataModels;
using RestaurantReservation.Db.Repositories;
using RestaurantReservationApp.Utilities;

namespace RestaurantReservationApp.Tests.RepositoryInvokers_SampleData
{
    public class CustomerRepositoryTest
    {
        private readonly IRepositoryServices<Customer, string> _customerRepository;

        public CustomerRepositoryTest(IRepositoryServices<Customer, string> customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task TestGetAllAsync()
        {
            Console.WriteLine("\n\nTesting Get All Customers...");
            var customers = await _customerRepository.GetAllAsync();
            var pageSize = 4;
            PaginationConsoleTable<Customer>.Paginate(customers, pageSize);
        }

        public async Task TestGetByIdAsync()
        {
            Console.WriteLine("\n\nTesting Get Customer by Id...");

            int customerToGetId = 1;
            var customer = await _customerRepository.GetByIdAsync(customerToGetId);
            if (customer != null)
            {
                var pageSize = 1;
                PaginationConsoleTable<Customer>.Paginate(new List<Customer> { customer! }, pageSize);
            }
            else
            {
                Console.WriteLine($"\nNo such customer with id:{customerToGetId}");
            }
        }

        public async Task TestAddAsync()
        {
            Console.WriteLine("\n\nTesting Add Customer...");
            var customer = new Customer
            {
                FirstName = "sample",
                LastName = "customer",
                Email = "sample_customer@gmail.com",
                PhoneNumber = "1234567890",
            };
            var result = await _customerRepository.AddAsync(customer);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nCustomer with {result.Message} added successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestUpdateAsync()
        {
            Console.WriteLine("\n\nTesting Update Customer...");

            var updatedCustomer = new Customer
            {
                FirstName = "newFName",
                LastName = "newLName",
                PhoneNumber = "12345678909",
            };

            int customerToUpdateId = 1;
            var result = await _customerRepository.UpdateAsync(customerToUpdateId, updatedCustomer);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nCustomer with {result.Message} updated successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestDeleteAsync()
        {
            Console.WriteLine("\n\nTesting Delete Customer...");

            int customerToDeleteId = 7;
            var deleteResult = await _customerRepository.DeleteAsync(customerToDeleteId);
            if (deleteResult)
            {
                Console.WriteLine($"\nCustomer with id:{customerToDeleteId} has been deleted successfully");
            }
            else
            {
                Console.WriteLine($"\nNo customer with id:{customerToDeleteId} found!");
            }
        }
    }
}
