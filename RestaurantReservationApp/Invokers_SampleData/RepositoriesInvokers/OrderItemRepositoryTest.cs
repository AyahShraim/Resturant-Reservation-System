using RestaurantReservation.Db.DataModels;
using RestaurantReservation.Db.Repositories;
using RestaurantReservationApp.Utilities;

namespace RestaurantReservationApp.Tests.RepositoryInvokers_SampleData
{
    public class OrderItemRepositoryTest
    {
        private readonly IRepositoryServices<OrderItem, string> _orderItemRepository;

        public OrderItemRepositoryTest(IRepositoryServices<OrderItem, string> orderItemRepository)
        {
            _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
        }

        public async Task TestGetAllAsync()
        {
            Console.WriteLine("\n\nTesting Get All Order Items...");
            var orderItems = await _orderItemRepository.GetAllAsync();
            var pageSize = 8;
            PaginationConsoleTable<OrderItem>.Paginate(orderItems, pageSize);
        }

        public async Task TestGetByIdAsync()
        {
            Console.WriteLine("\n\nTesting Get Order Item by Id...");

            int orderItemToGetId = 2;
            var orderItem = await _orderItemRepository.GetByIdAsync(orderItemToGetId);
            if (orderItem != null)
            {
                var pageSize = 1;
                PaginationConsoleTable<OrderItem>.Paginate(new List<OrderItem> { orderItem! }, pageSize);
            }
            else
            {
                Console.WriteLine($"\nNo such Order Item with id:{orderItemToGetId}");
            }
        }

        public async Task TestAddAsync()
        {
            Console.WriteLine("\n\nTesting Add Order Item...");
            var orderItem = new OrderItem
            {
                Quantity = 3,
                OrderId = 14,
                MenuItemId = 15
            };
            var result = await _orderItemRepository.AddAsync(orderItem);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nOrder Item with {result.Message} added successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestUpdateAsync()
        {
            Console.WriteLine("\n\nTesting Update Order Item...");

            var updatedOrderItem = new OrderItem
            {
                Quantity = 2,
                OrderId = 14,
                MenuItemId = 12
            };

            int orderItemToUpdateId = 31;
            var result = await _orderItemRepository.UpdateAsync(orderItemToUpdateId, updatedOrderItem);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nOrder Item with {result.Message} updated successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestDeleteAsync()
        {
            Console.WriteLine("\n\nTesting Delete Order Item...");

            int orderItemToDeleteId = 32;
            var deleteResult = await _orderItemRepository.DeleteAsync(orderItemToDeleteId);
            if (deleteResult)
            {
                Console.WriteLine($"\nOrder Item with id:{orderItemToDeleteId} deleted successfully");
            }
            else
            {
                Console.WriteLine($"\nNo order item with id:{orderItemToDeleteId} found!");
            }
        }
    }
}
