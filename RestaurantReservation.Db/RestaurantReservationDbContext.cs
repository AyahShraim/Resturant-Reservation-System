using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Enums;
using RestaurantReservation.Db.SampleData;
using RestaurantReservation.Db.ViewsModels;
using RestaurantReservation.Db.StoredProcedureModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace RestaurantReservation.Db
{
    public class RestaurantReservationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ReservationDetails> ReservationsDetails { get; set; }
        public DbSet<EmployeesWithRestaurantDetails> EmployeesWithRestaurantDetails { get; set; }
        public DbSet<CustomerWithLargePartySizeReservation> CustomersWithLargePartySizeReservation { get; set; }

        public RestaurantReservationDbContext(DbContextOptions<RestaurantReservationDbContext> options)
           : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-62OPL8I;Database=RestaurantReservationCore;Trusted_Connection=True;TrustServerCertificate=True")
                          .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Name },
                                 LogLevel.Information);
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder modelConfigurationBuilder)
        {
            modelConfigurationBuilder.Properties<EmployeePosition>().HaveColumnType("varchar(10)");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>()
                .HasMany(r => r.Reservations)
                .WithOne(t => t.Table)
                .HasForeignKey(t => t.TableId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Orders)
                .WithOne(o => o.Reservation)
                .HasForeignKey(o => o.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.MenuItem)
                .WithMany(menuItem => menuItem.OrderItems)
                .HasForeignKey(oi => oi.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ReservationDetails>().HasNoKey()
                .ToView(nameof(ReservationsDetails));

            modelBuilder.Entity<EmployeesWithRestaurantDetails>().HasNoKey()
                .ToView(nameof(EmployeesWithRestaurantDetails));

            modelBuilder.HasDbFunction(typeof(RestaurantReservationDbContext)
               .GetMethod(nameof(CalculateRestaurantTotalRevenue)))
               .HasName("fn_CalculateRestaurantTotalRevenue");

            modelBuilder.Entity<CustomerWithLargePartySizeReservation>().HasNoKey();

            modelBuilder.Seed();
        }

        [DbFunction("fn_CalculateRestaurantTotalRevenue", Schema = "dbo")]
        public decimal CalculateRestaurantTotalRevenue(int restaurantId)
        {
            return Employees.Take(1).Select(x => CalculateRestaurantTotalRevenue(restaurantId))
            .SingleOrDefault();
        }
    }
}
