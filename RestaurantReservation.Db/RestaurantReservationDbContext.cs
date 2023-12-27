using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Db
{
    public class RestaurantReservationDbContext : DbContext
    {
        private readonly string _connectionString;

        public RestaurantReservationDbContext(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
