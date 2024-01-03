using RestaurantReservation.Db.DataModels;
using RestaurantReservation.Db.Repositories;
using RestaurantReservationApp.Utilities;

namespace RestaurantReservationApp.Tests.RepositoryInvokers_SampleData
{
    public class RestaurantRepositoryTest
    {
        private readonly IRepositoryServices<Restaurant, string> _restaurantRepository;

        public RestaurantRepositoryTest(RestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
        }

        public async Task TestGetAllAsync()
        {
            Console.WriteLine("\n\nTesting Get All Restaurants...");
            var restaurants = await _restaurantRepository.GetAllAsync();
            var pageSize = 4;
            PaginationConsoleTable<Restaurant>.Paginate(restaurants, pageSize);
        }

        public async Task TestGetByIdAsync()
        {
            Console.WriteLine("\n\nTesting Get Restaurant by Id...");
            int restaurantToGetId = 1;
            var restaurant = await _restaurantRepository.GetByIdAsync(restaurantToGetId);
            if (restaurant != null)
            {
                var pageSize = 1;
                PaginationConsoleTable<Restaurant>.Paginate(new List<Restaurant> { restaurant! }, pageSize);
            }
            else
            {
                Console.WriteLine($"\nNo such restaurant with id:{restaurantToGetId}");
            }
        }

        public async Task TestAddAsync()
        {
            Console.WriteLine("\n\nTesting Add Restaurant...");
            var restaurant = new Restaurant
            {
                Name = "Sample Restaurant",
                Address = "123 main street",
                PhoneNumber = "9876543210",
                OpeningHours = "Saturday-Thursday: 10:00AM-5:00AM",
            };
            var result = await _restaurantRepository.AddAsync(restaurant);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nRestaurant with {result.Message} added successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestUpdateAsync()
        {
            Console.WriteLine("\n\nTesting Update Restaurant...");

            var updatedRestaurant = new Restaurant
            {
                Name = "new Restaurant name",
                Address = "12 street",
                PhoneNumber = "9876543210",
                OpeningHours = "Saturday-Thursday: 7:00AM-5:00AM",
            };

            int restaurantToUpdateId = 8;
            var result = await _restaurantRepository.UpdateAsync(restaurantToUpdateId, updatedRestaurant);
            if (result.IsSuccess)
            {
                Console.WriteLine($"\nRestaurant with {result.Message} updated successfully");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public async Task TestDeleteAsync()
        {
            Console.WriteLine("\n\nTesting Delete Restaurant...");

            int restaurantToDeleteId = 9;
            var deleteResult = await _restaurantRepository.DeleteAsync(restaurantToDeleteId);
            if (deleteResult)
            {
                Console.WriteLine($"\nRestaurant with id:{restaurantToDeleteId} deleted successfully");
            }
            else
            {
                Console.WriteLine($"\nNo Restaurant with id:{restaurantToDeleteId} found!");
            }
        }
    }
}
