using Microsoft.Extensions.Configuration;

namespace RestaurantReservationApp.Utilities
{
    public static class DbHelper
    {
        public static string GetConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            return config.GetConnectionString("RestaurantReservationDb");
        }
    }
}
