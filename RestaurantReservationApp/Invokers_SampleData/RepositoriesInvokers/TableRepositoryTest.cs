using RestaurantReservation.Db.DataModels;
using RestaurantReservation.Db.Repositories;
using RestaurantReservationApp.Utilities;

namespace RestaurantReservationApp.Tests.RepositoryInvokers_SampleData
{
    public class TableRepositoryTest
    {
        private readonly IRepositoryServices<Table, string> _tableRepository;

        public TableRepositoryTest(IRepositoryServices<Table, string> tableRepository)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
        }

        public async Task TestGetAllAsync()
        {
            Console.WriteLine("\n\nTesting Get All Tables...");
            var tables = await _tableRepository.GetAllAsync();
            var pageSize = 8;
            PaginationConsoleTable<Table>.Paginate(tables, pageSize);
        }

        public async Task TestGetByIdAsync()
        {
            Console.WriteLine("\n\nTesting Get Table by Id...");

            int tableToGetId = 2;
            var table = await _tableRepository.GetByIdAsync(tableToGetId);
            if (table != null)
            {
                var pageSize = 1;
                PaginationConsoleTable<Table>.Paginate(new List<Table> { table! }, pageSize);
            }
            else
            {
                Console.WriteLine($"\nNo such Table with id:{tableToGetId}");
            }
        }

        public async Task TestAddAsync()
        {
            Console.WriteLine("\n\nTesting Add Table...");
            var table = new Table
            {
                Capacity = 20,
                RestaurantId = 1,
            };
            var result = await _tableRepository.AddAsync(table);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nTable with {result.Message} added successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestUpdateAsync()
        {
            Console.WriteLine("\n\nTesting Update Table...");

            var updatedTable = new Table
            {
                Capacity = 20,
                RestaurantId = 6,
            };

            int tableToUpdateId = 26;
            var result = await _tableRepository.UpdateAsync(tableToUpdateId, updatedTable);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nTable with {result.Message} updated successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestDeleteAsync()
        {
            Console.WriteLine("\n\nTesting Delete Table...");

            int tableToDeleteId = 10;
            var deleteResult = await _tableRepository.DeleteAsync(tableToDeleteId);
            if (deleteResult)
            {
                Console.WriteLine($"\nTable with id:{tableToDeleteId} deleted successfully");
            }
            else
            {
                Console.WriteLine($"\nNo table with id:{tableToDeleteId} found!");
            }
        }
    }
}
