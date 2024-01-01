using ConsoleTables;
using RestaurantReservation.Db.DataModels;
using RestaurantReservation.Db.IServices;
using RestaurantReservation.Db.Repositories;
using RestaurantReservationApp.Utilities;

namespace RestaurantReservationApp.Tests.RepositoryInvokers_SampleData
{
    public class OrderRepositoryTest
    {
        private readonly IRepositoryServices<Order, string> _orderRepository;
        private readonly IOrderServices _orderServices;

        public OrderRepositoryTest(IRepositoryServices<Order, string> orderRepository, IOrderServices orderServices)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _orderServices = orderServices ?? throw new ArgumentNullException(nameof(orderServices));

        }

        public async Task TestGetAllAsync()
        {
            Console.WriteLine("\n\nTesting Get All Orders...");
            var orders = await _orderRepository.GetAllAsync();
            var pageSize = 8;
            PaginationConsoleTable<Order>.Paginate(orders, pageSize);
        }

        public async Task TestGetByIdAsync()
        {
            Console.WriteLine("\n\nTesting Get Order by Id...");

            int orderToGetId = 2;
            var order = await _orderRepository.GetByIdAsync(orderToGetId);
            if (order != null)
            {
                var pageSize = 1;
                PaginationConsoleTable<Order>.Paginate(new List<Order> { order! }, pageSize);
            }
            else
            {
                Console.WriteLine($"\nNo such Order with id:{orderToGetId}");
            }
        }

        public async Task TestAddAsync()
        {
            Console.WriteLine("\n\nTesting Add Order...");
            var order = new Order
            {
                Date = DateTime.Now.AddDays(5),
                TotalAmount = 65,
                EmployeeId = 8,
                ReservationId = 17,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { Quantity = 2, MenuItemId = 10 },
                    new OrderItem { Quantity = 3, MenuItemId = 15 },
                }
            };
            var result = await _orderRepository.AddAsync(order);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nOrder with {result.Message} added successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestUpdateAsync()
        {
            Console.WriteLine("\n\nTesting Update Order...");

            var updatedOrder = new Order
            {
                Date = DateTime.Now.AddDays(7),
                TotalAmount = 120,
                EmployeeId = 12,
                ReservationId = 17
            };

            int orderToUpdateId = 17;
            var result = await _orderRepository.UpdateAsync(orderToUpdateId, updatedOrder);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nOrder with {result.Message} updated successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestDeleteAsync()
        {
            Console.WriteLine("\n\nTesting Delete Order...");

            int orderToDeleteId = 17;
            var deleteResult = await _orderRepository.DeleteAsync(orderToDeleteId);
            if (deleteResult)
            {
                Console.WriteLine($"\nOrder with id:{orderToDeleteId} deleted successfully");
            }
            else
            {
                Console.WriteLine($"\nNo order with id:{orderToDeleteId} found!");
            }
        }

        public async Task TestListOrdersAndMenuItemsAsync()

        {
            Console.WriteLine("\n\nTesting List Orders And Menu Items in specific reservation...");
            int reservationId = 9;
            var orders = await _orderServices.ListOrdersAndMenuItemsAsync(reservationId);
            if (orders.Any())
            {
                DisplayOrdersAndMenuItems(orders);
            }
            else
            {
                Console.WriteLine($"\nNo orders found for reservation with id:{reservationId}");
            }

            void DisplayOrdersAndMenuItems(IEnumerable<Order> orders)
            {
                var table = new ConsoleTable("Order ID", "Menu Item", "Quantity");

                foreach (var order in orders)
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        table.AddRow(order.Id, orderItem.MenuItem.Name, orderItem.Quantity);
                    }
                }

                table.Write();
                Console.WriteLine();
            }
        }

        public async Task TestListOrderedMenuItemsAsync()
        {
            Console.WriteLine("\n\nTesting List Ordered Menu Items in specific reservation...");
            int reservationId = 9;
            var orderedMenuItems = await _orderServices.ListOrderedMenuItemsAsync(reservationId);

            if (orderedMenuItems.Any())
            {
                DisplayOrderedMenuItems(orderedMenuItems);
            }
            else
            {
                Console.WriteLine($"\nNo ordered menu items found for reservation with id:{reservationId}");
            }

            void DisplayOrderedMenuItems(IEnumerable<MenuItem> menuItems)
            {
                var table = new ConsoleTable("Menu Item ID", "Name", "Description", "Price");

                foreach (var menuItem in menuItems)
                {
                    table.AddRow(menuItem.Id, menuItem.Name, menuItem.Description, menuItem.Price);
                }

                table.Write();
                Console.WriteLine();
            }
        }

        public async Task TestCalculateAverageOrderAmountAsync()
        {
            Console.WriteLine("\n\nTesting Calculate avg order amount for specific employee...");
            int employeeId = 16;
            var averageOrderAmount = await _orderServices.CalculateAverageOrderAmountAsync(employeeId);
            Console.WriteLine($"\n\nAverage Order Amount for employee with id {employeeId}: {averageOrderAmount:C}\"");
        }
    }
}
