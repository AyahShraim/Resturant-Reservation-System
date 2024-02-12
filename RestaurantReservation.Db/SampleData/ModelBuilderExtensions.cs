using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Enum;

namespace RestaurantReservation.Db.SampleData
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(LoadCustomers());
            modelBuilder.Entity<Restaurant>().HasData(LoadRestaurants());
            modelBuilder.Entity<Table>().HasData(LoadTables());
            modelBuilder.Entity<Reservation>().HasData(LoadReservations());
            modelBuilder.Entity<Employee>().HasData(LoadEmployees());
            modelBuilder.Entity<MenuItem>().HasData(LoadMenuItems());
            modelBuilder.Entity<OrderItem>().HasData(LoadOrderItems());
            modelBuilder.Entity<Order>().HasData(LoadOrders());

        }

        private static List<Customer> LoadCustomers()
        {
            return new List<Customer>()
            {
                new() { Id = 1, FirstName = "Emma", LastName = "Allan", Email = "allan_emma@gmail.com", PhoneNumber = "+14304466390" },
                new() { Id = 2, FirstName = "Jasmine", LastName = "Arnold", Email = "arnold_jasmine@gmail.com", PhoneNumber = "+14304466390" },
                new() { Id = 3, FirstName = "Sophie", LastName = "Anderson", Email = "anderson_sophie@gmail.com", PhoneNumber = "+18704197636" },
                new() { Id = 4, FirstName = "Ryan", LastName = "Cameron", Email = "ayah_shcameron_ryan@gmail.com", PhoneNumber = "+13106081534" },
                new() { Id = 5, FirstName = "William", LastName = "Lewis", Email = "lewis_william@gmail.com", PhoneNumber = "+12205090941" },
            };
        }

        private static List<Restaurant> LoadRestaurants()
        {
            return new List<Restaurant>()
            {
                    new() { Id = 1, Name = "BURGER KING", Address = "19 Old Fulton St., Brooklyn", PhoneNumber = "+17185551234", OpeningHours = "Saturday-Thursday: 10:00AM-5:00AM" },
                    new() { Id = 2, Name = "ABC SUSHI", Address = "123 Main St., Cityville", PhoneNumber = "+14304461234", OpeningHours = "Monday-Sunday: 11:00AM-8:00PM" },
                    new() { Id = 3, Name = "CHINESE KITCHEN", Address = "456 Elm St., Townsville", PhoneNumber = "+14304462345", OpeningHours = "Tuesday-Saturday: 12:30PM-9:00PM" },
                    new() { Id = 4, Name = "DASH SEAFOOD & CHICKEN", Address = "789 Oak St., Villageton", PhoneNumber = "+14304463456", OpeningHours = "Wednesday-Monday: 3:00PM-10:30PM" },
                    new() { Id = 5, Name = "Patsy's Pizza", Address = "321 Maple St., Hamletville", PhoneNumber = "+14304464567", OpeningHours = "Thursday-Tuesday: 4:00PM-11:00PM" },
            };
        }

        private static List<Table> LoadTables()
        {
            return new List<Table>()
            {
                new() { Id = 1, Capacity = 2, RestaurantId = 1 },
                new() { Id = 2, Capacity = 4, RestaurantId = 1 },
                new() { Id = 3, Capacity = 6, RestaurantId = 1 },
                new() { Id = 4, Capacity = 8, RestaurantId = 1 },
                new() { Id = 5, Capacity = 10, RestaurantId = 1 },
                new() { Id = 6, Capacity = 2, RestaurantId = 2 },
                new() { Id = 7, Capacity = 4, RestaurantId = 2 },
                new() { Id = 8, Capacity = 4, RestaurantId = 2 },
                new() { Id = 9, Capacity = 4, RestaurantId = 2 },
                new() { Id = 10, Capacity = 8, RestaurantId = 2 },
                new() { Id = 11, Capacity = 2, RestaurantId = 3 },
                new() { Id = 12, Capacity = 10, RestaurantId = 3 },
                new() { Id = 13, Capacity = 14, RestaurantId = 3 },
                new() { Id = 14, Capacity = 4, RestaurantId = 3 },
                new() { Id = 15, Capacity = 10, RestaurantId = 3 },
                new() { Id = 16, Capacity = 2, RestaurantId = 4 },
                new() { Id = 17, Capacity = 18, RestaurantId = 4 },
                new() { Id = 18, Capacity = 4, RestaurantId = 4 },
                new() { Id = 19, Capacity = 6, RestaurantId = 4 },
                new() { Id = 20, Capacity = 20, RestaurantId = 4 },
                new() { Id = 21, Capacity = 4, RestaurantId = 5 },
                new() { Id = 22, Capacity = 6, RestaurantId = 5 },
                new() { Id = 23, Capacity = 8, RestaurantId = 5 },
                new() { Id = 24, Capacity = 10, RestaurantId = 5 },
                new() { Id = 25, Capacity = 12, RestaurantId = 5 },
            };
        }

        private static List<Reservation> LoadReservations()
        {
            return new List<Reservation>()
            {
                new() { Id = 1, CustomerId = 5, RestaurantId = 1, TableId = 1, Date = DateTime.Now.AddDays(-2), PartySize = 2 },
                new() { Id = 2, CustomerId = 1, RestaurantId = 2, TableId = 7, Date = DateTime.Now.AddDays(-1), PartySize = 4 },
                new() { Id = 3, CustomerId = 2, RestaurantId = 2, TableId = 10, Date = DateTime.Now.AddDays(-1), PartySize = 6 },
                new() { Id = 4, CustomerId = 3, RestaurantId = 3, TableId = 15, Date = DateTime.Now.AddDays(-4), PartySize = 8 },
                new() { Id = 5, CustomerId = 4, RestaurantId = 1, TableId = 5, Date = DateTime.Now.AddDays(-2), PartySize = 10 },
                new() { Id = 6, CustomerId = 5, RestaurantId = 3, TableId = 15, Date = DateTime.Now.AddDays(-2), PartySize = 8 },
                new() { Id = 7, CustomerId = 1, RestaurantId = 3, TableId = 13, Date = DateTime.Now.AddDays(-2), PartySize = 14 },
                new() { Id = 8, CustomerId = 2, RestaurantId = 3, TableId = 11, Date = DateTime.Now.AddDays(-5), PartySize = 2 },
                new() { Id = 9, CustomerId = 3, RestaurantId = 4, TableId = 18, Date = DateTime.Now.AddDays(-3), PartySize = 4 },
                new() { Id = 10, CustomerId = 1, RestaurantId = 4, TableId = 20, Date = DateTime.Now.AddDays(-7), PartySize = 20 },
                new() { Id = 11, CustomerId = 3, RestaurantId = 5, TableId = 22, Date = DateTime.Now.AddDays(-3), PartySize = 6 },
            };
        }

        private static List<Employee> LoadEmployees()
        {
            return new List<Employee>()
            {
                new() { Id = 1, FirstName = "John", LastName = "Doe", Position = EmployeePosition.Manager, RestaurantId = 1 },
                new() { Id = 2, FirstName = "Alice", LastName = "Smith", Position = EmployeePosition.Waiter, RestaurantId = 1},
                new() { Id = 3, FirstName = "Bob", LastName = "Johnson", Position = EmployeePosition.Chef, RestaurantId = 1 },
                new() { Id = 4, FirstName = "Emily", LastName = "Williams", Position = EmployeePosition.Cashier, RestaurantId = 1 },
                new() { Id = 5, FirstName = "Sophia", LastName = "Jones", Position = EmployeePosition.Manager, RestaurantId = 2 },
                new() { Id = 6, FirstName = "Daniel", LastName = "Miller", Position = EmployeePosition.Waiter, RestaurantId = 2 },
                new() { Id = 7, FirstName = "Olivia", LastName = "Davis", Position = EmployeePosition.Waiter, RestaurantId = 2 },
                new() { Id = 8, FirstName = "Bella", LastName = "Hamilton", Position = EmployeePosition.Chef, RestaurantId = 2 },
                new() { Id = 9, FirstName = "Emma", LastName = "Rodriguez", Position = EmployeePosition.Manager, RestaurantId = 3 },
                new() { Id = 10, FirstName = "Michael", LastName = "Brown", Position = EmployeePosition.Waiter, RestaurantId = 3 },
                new() { Id = 11, FirstName = "James", LastName = "Garcia", Position = EmployeePosition.Chef, RestaurantId = 3 },
                new() { Id = 12, FirstName = "Sophie", LastName = "Taylor", Position = EmployeePosition.Manager, RestaurantId = 4 },
                new() { Id = 13, FirstName = "William", LastName = "Johnson", Position = EmployeePosition.Waiter, RestaurantId = 4 },
                new() { Id = 14, FirstName = "Peter", LastName = "Ellison", Position = EmployeePosition.Chef, RestaurantId = 4 },
                new() { Id = 15, FirstName = "Oliver", LastName = "Smith", Position = EmployeePosition.Manager, RestaurantId = 5 },
                new() { Id = 16, FirstName = "Amelia", LastName = "Brown", Position = EmployeePosition.Waiter, RestaurantId = 5 },
                new() { Id = 17, FirstName = "Ethan", LastName = "Davis", Position = EmployeePosition.Chef, RestaurantId = 5 },
                new() { Id = 18, FirstName = "William", LastName = "Davis", Position = EmployeePosition.Cashier, RestaurantId = 5 },
                new() { Id = 19, FirstName = "Ali", LastName = "Clark", Position = EmployeePosition.Chef, RestaurantId = 5 },
            };
        }

        private static List<MenuItem> LoadMenuItems()
        {
            return new List<MenuItem>()
            {
                new() { Id = 1, Name = "Classic Burger", Description = "Juicy beef patty with lettuce, tomato, and cheese", Price = 8.99M, RestaurantId = 1 },
                new() { Id = 2, Name = "Crispy Chicken Sandwich", Description = "Golden fried chicken with mayo and pickles", Price = 9.99M, RestaurantId = 1 },
                new() { Id = 3, Name = "Vegetarian Wrap", Description = "Grilled veggies wrapped in a tortilla with hummus", Price = 7.99M, RestaurantId = 1 },
                new() { Id = 4, Name = "Sushi Combo", Description = "Assorted sushi rolls with soy sauce", Price = 15.99M, RestaurantId = 2 },
                new() { Id = 5, Name = "Tempura Shrimp", Description = "Crispy shrimp tempura with dipping sauce", Price = 12.99M, RestaurantId = 2 },
                new() { Id = 6, Name = "Miso Soup", Description = "Traditional Japanese soybean soup with tofu", Price = 4.99M, RestaurantId = 2 },
                new() { Id = 7, Name = "Sweet and Sour Chicken", Description = "Battered and fried chicken with sweet and sour sauce", Price = 11.99M, RestaurantId = 3 },
                new() { Id = 8, Name = "Beef and Broccoli", Description = "Stir-fried beef with broccoli in a savory sauce", Price = 13.99M, RestaurantId = 3 },
                new() { Id = 9, Name = "Shrimp Fried Rice", Description = "Fried rice with shrimp, vegetables, and soy sauce", Price = 10.99M, RestaurantId = 3 },
                new() { Id = 10, Name = "Seafood Platter", Description = "Assorted seafood including shrimp, fish, and calamari", Price = 18.99M, RestaurantId = 4 },
                new() { Id = 11, Name = "Chicken Wings", Description = "Spicy buffalo chicken wings with ranch dressing", Price = 9.99M, RestaurantId = 4 },
                new() { Id = 12, Name = "Grilled Salmon", Description = "Fresh salmon fillet grilled to perfection", Price = 14.99M, RestaurantId = 4 },
                new() { Id = 13, Name = "Margherita Pizza", Description = "Classic pizza with tomato, mozzarella, and basil", Price = 12.99M, RestaurantId = 5 },
                new() { Id = 14, Name = "Pasta Carbonara", Description = "Creamy pasta with bacon, eggs, and parmesan cheese", Price = 15.99M, RestaurantId = 5 },
                new() { Id = 15, Name = "Tiramisu", Description = "Italian dessert with layers of coffee-soaked ladyfingers and mascarpone", Price = 7.99M, RestaurantId = 5 },
            };
        }

        private static List<OrderItem> LoadOrderItems()
        {
            return new List<OrderItem>()
            {
                new() { Id = 1, MenuItemId = 1, Quantity = 2, OrderId = 1 },
                new() { Id = 2, MenuItemId = 2, Quantity = 2, OrderId = 1 },
                new() { Id = 3, MenuItemId = 4, Quantity = 1, OrderId = 2 },
                new() { Id = 4, MenuItemId = 5, Quantity = 1, OrderId = 2 },
                new() { Id = 5, MenuItemId = 6, Quantity = 1, OrderId = 2 },
                new() { Id = 6, MenuItemId = 7, Quantity = 3, OrderId = 3 },
                new() { Id = 7, MenuItemId = 9, Quantity = 3, OrderId = 3 },
                new() { Id = 8, MenuItemId = 10, Quantity = 2, OrderId = 4 },
                new() { Id = 9, MenuItemId = 11, Quantity = 2, OrderId = 4 },
                new() { Id = 10, MenuItemId = 12, Quantity = 2, OrderId = 4 },
                new() { Id = 11, MenuItemId = 13, Quantity = 1, OrderId = 5 },
                new() { Id = 12, MenuItemId = 14, Quantity = 1, OrderId = 5 },
                new() { Id = 13, MenuItemId = 15, Quantity = 1, OrderId = 5 },
                new() { Id = 14, MenuItemId = 1, Quantity = 2, OrderId = 6 },
                new() { Id = 15, MenuItemId = 3, Quantity = 2, OrderId = 6 },
                new() { Id = 16, MenuItemId = 4, Quantity = 6, OrderId = 7 },
                new() { Id = 17, MenuItemId = 7, Quantity = 3, OrderId = 8 },
                new() { Id = 18, MenuItemId = 8, Quantity = 3, OrderId = 8 },
                new() { Id = 19, MenuItemId = 9, Quantity = 3, OrderId = 8 },
                new() { Id = 20, MenuItemId = 10, Quantity = 2, OrderId = 9 },
                new() { Id = 21, MenuItemId = 11, Quantity = 1, OrderId = 10 },
                new() { Id = 22, MenuItemId = 12, Quantity = 2, OrderId = 10 },
                new() { Id = 23, MenuItemId = 7, Quantity = 2, OrderId = 11 },
                new() { Id = 24, MenuItemId = 8, Quantity = 1, OrderId = 12 },
                new() { Id = 25, MenuItemId = 13, Quantity = 2, OrderId = 13 },
                new() { Id = 26, MenuItemId = 14, Quantity = 2, OrderId = 13 },
                new() { Id = 27, MenuItemId = 14, Quantity = 2, OrderId = 14 },
            };
        }

        private static List<Order> LoadOrders()
        {
            return new List<Order>()
            {
                new() { Id = 1, ReservationId = 1, EmployeeId = 2, Date = DateTime.Now.AddDays(-2), TotalAmount = 2 * (8.99m + 9.99M) },
                new() { Id = 2, ReservationId = 2, EmployeeId = 6, Date = DateTime.Now.AddDays(-1), TotalAmount = 15.99M + 12.99M + 4.99M },
                new() { Id = 3, ReservationId = 4, EmployeeId = 10, Date = DateTime.Now.AddDays(-4), TotalAmount = 3 * (11.99M + 10.99M) },
                new() { Id = 4, ReservationId = 9, EmployeeId = 13, Date = DateTime.Now.AddDays(-2), TotalAmount = 2 * (18.99M + 9.99M + 14.99M) },
                new() { Id = 5, ReservationId = 11, EmployeeId = 16, Date = DateTime.Now.AddDays(-3), TotalAmount = 12.99M + 15.99M + 7.99M },
                new() { Id = 6, ReservationId = 5, EmployeeId = 2, Date = DateTime.Now.AddDays(-2), TotalAmount = 2 * (8.99M + 7.99M) },
                new() { Id = 7, ReservationId = 3, EmployeeId = 7, Date = DateTime.Now.AddDays(-1), TotalAmount = 6 * 15.99M },
                new() { Id = 8, ReservationId = 8 , EmployeeId = 10, Date = DateTime.Now.AddDays(-5), TotalAmount = 3 * (11.99M + 13.99M + 10.99M) },
                new() { Id = 9, ReservationId = 9, EmployeeId = 13, Date = DateTime.Now.AddDays(-2), TotalAmount = 2 * 18.99M },
                new() { Id = 10, ReservationId = 9, EmployeeId = 13, Date = DateTime.Now.AddDays(-2), TotalAmount = 9.99M + (2 * 14.99M) },
                new() { Id = 11, ReservationId = 4 , EmployeeId = 10, Date = DateTime.Now.AddDays(-4), TotalAmount = 2 * 11.99M },
                new() { Id = 12, ReservationId = 4, EmployeeId = 10, Date = DateTime.Now.AddDays(-4), TotalAmount = 1 *  13.99M },
                new() { Id = 13, ReservationId = 11, EmployeeId = 16, Date = DateTime.Now.AddDays(-3), TotalAmount = 2 * (12.99M + 15.99M) },
                new() { Id = 14, ReservationId = 11 , EmployeeId = 16, Date = DateTime.Now.AddDays(-3), TotalAmount = 1 * 15.99M },
            };
        }
    }
}

