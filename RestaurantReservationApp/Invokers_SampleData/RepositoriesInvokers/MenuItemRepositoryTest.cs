using RestaurantReservation.Db.DataModels;
using RestaurantReservation.Db.Repositories;
using RestaurantReservationApp.Utilities;

namespace RestaurantReservationApp.Tests.RepositoryInvokers_SampleData
{
    public class MenuItemRepositoryTest
    {
        private readonly IRepositoryServices<MenuItem, string> _menuItemRepository;

        public MenuItemRepositoryTest(MenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository ?? throw new ArgumentNullException(nameof(menuItemRepository));
        }

        public async Task TestGetAllAsync()
        {
            Console.WriteLine("\n\nTesting Get All Menu Items...");
            var menuItems = await _menuItemRepository.GetAllAsync();
            var pageSize = 8;
            PaginationConsoleTable<MenuItem>.Paginate(menuItems, pageSize);
        }

        public async Task TestGetByIdAsync()
        {
            Console.WriteLine("\n\nTesting Get Menu Item by Id...");

            int menuItemToGetId = 2;
            var menuItem = await _menuItemRepository.GetByIdAsync(menuItemToGetId);
            if (menuItem != null)
            {
                var pageSize = 1;
                PaginationConsoleTable<MenuItem>.Paginate(new List<MenuItem> { menuItem! }, pageSize);
            }
            else
            {
                Console.WriteLine($"\nNo such Menu Item with id:{menuItemToGetId}");
            }
        }

        public async Task TestAddAsync()
        {
            Console.WriteLine("\n\nTesting Add MenuItem...");
            var menuItem = new MenuItem
            {
                Name = "Test",
                Description = "sample menu item for testing",
                Price = 20,
                RestaurantId = 5
            };
            var result = await _menuItemRepository.AddAsync(menuItem);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nMenuItem with {result.Message} added successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestUpdateAsync()
        {
            Console.WriteLine("\n\nTesting Update MenuItem...");

            var updatedMenuItem = new MenuItem
            {
                Name = "newName",
                Description = "menu item updated",
                Price = 30,
                RestaurantId = 9
            };

            int menuItemToUpdateId = 16;
            var result = await _menuItemRepository.UpdateAsync(menuItemToUpdateId, updatedMenuItem);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nMenu Item with {result.Message} updated successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestDeleteAsync()
        {
            Console.WriteLine("\n\nTesting Delete Menu Item...");

            int menuItemToDeleteId = 28;
            var deleteResult = await _menuItemRepository.DeleteAsync(menuItemToDeleteId);
            if (deleteResult)
            {
                Console.WriteLine($"\nMenu Item with id:{menuItemToDeleteId} deleted successfully");
            }
            else
            {
                Console.WriteLine($"\nNo menu item with id:{menuItemToDeleteId} found!");
            }
        }
    }
}
