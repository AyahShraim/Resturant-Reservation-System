using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "allan_emma@gmail.com", "Emma", "Allan", "+14304466390" },
                    { 2, "arnold_jasmine@gmail.com", "Jasmine", "Arnold", "+14304466390" },
                    { 3, "anderson_sophie@gmail.com", "Sophie", "Anderson", "+18704197636" },
                    { 4, "ayah_shcameron_ryan@gmail.com", "Ryan", "Cameron", "+13106081534" },
                    { 5, "lewis_william@gmail.com", "William", "Lewis", "+12205090941" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Name", "OpeningHours", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "19 Old Fulton St., Brooklyn", "BURGER KING", "Saturday-Thursday: 10:00AM-5:00AM", "+17185551234" },
                    { 2, "123 Main St., Cityville", "ABC SUSHI", "Monday-Sunday: 11:00AM-8:00PM", "+14304461234" },
                    { 3, "456 Elm St., Townsville", "CHINESE KITCHEN", "Tuesday-Saturday: 12:30PM-9:00PM", "+14304462345" },
                    { 4, "789 Oak St., Villageton", "DASH SEAFOOD & CHICKEN", "Wednesday-Monday: 3:00PM-10:30PM", "+14304463456" },
                    { 5, "321 Maple St., Hamletville", "Patsy's Pizza", "Thursday-Tuesday: 4:00PM-11:00PM", "+14304464567" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Position", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "John", "Doe", "Manager", 1 },
                    { 2, "Alice", "Smith", "Waiter", 1 },
                    { 3, "Bob", "Johnson", "Chef", 1 },
                    { 4, "Emily", "Williams", "Cashier", 1 },
                    { 5, "Sophia", "Jones", "Manager", 2 },
                    { 6, "Daniel", "Miller", "Waiter", 2 },
                    { 7, "Olivia", "Davis", "Waiter", 2 },
                    { 8, "Bella", "Hamilton", "Chef", 2 },
                    { 9, "Emma", "Rodriguez", "Manager", 3 },
                    { 10, "Michael", "Brown", "Waiter", 3 },
                    { 11, "James", "Garcia", "Chef", 3 },
                    { 12, "Sophie", "Taylor", "Manager", 4 },
                    { 13, "William", "Johnson", "Waiter", 4 },
                    { 14, "Peter", "Ellison", "Chef", 4 },
                    { 15, "Oliver", "Smith", "Manager", 5 },
                    { 16, "Amelia", "Brown", "Waiter", 5 },
                    { 17, "Ethan", "Davis", "Chef", 5 },
                    { 18, "William", "Davis", "Cashier", 5 },
                    { 19, "Ali", "Clark", "Chef", 5 }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Description", "Name", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Juicy beef patty with lettuce, tomato, and cheese", "Classic Burger", 8.99m, 1 },
                    { 2, "Golden fried chicken with mayo and pickles", "Crispy Chicken Sandwich", 9.99m, 1 },
                    { 3, "Grilled veggies wrapped in a tortilla with hummus", "Vegetarian Wrap", 7.99m, 1 },
                    { 4, "Assorted sushi rolls with soy sauce", "Sushi Combo", 15.99m, 2 },
                    { 5, "Crispy shrimp tempura with dipping sauce", "Tempura Shrimp", 12.99m, 2 },
                    { 6, "Traditional Japanese soybean soup with tofu", "Miso Soup", 4.99m, 2 },
                    { 7, "Battered and fried chicken with sweet and sour sauce", "Sweet and Sour Chicken", 11.99m, 3 },
                    { 8, "Stir-fried beef with broccoli in a savory sauce", "Beef and Broccoli", 13.99m, 3 },
                    { 9, "Fried rice with shrimp, vegetables, and soy sauce", "Shrimp Fried Rice", 10.99m, 3 },
                    { 10, "Assorted seafood including shrimp, fish, and calamari", "Seafood Platter", 18.99m, 4 },
                    { 11, "Spicy buffalo chicken wings with ranch dressing", "Chicken Wings", 9.99m, 4 },
                    { 12, "Fresh salmon fillet grilled to perfection", "Grilled Salmon", 14.99m, 4 },
                    { 13, "Classic pizza with tomato, mozzarella, and basil", "Margherita Pizza", 12.99m, 5 },
                    { 14, "Creamy pasta with bacon, eggs, and parmesan cheese", "Pasta Carbonara", 15.99m, 5 },
                    { 15, "Italian dessert with layers of coffee-soaked ladyfingers and mascarpone", "Tiramisu", 7.99m, 5 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "Capacity", "RestaurantId" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 4, 1 },
                    { 3, 6, 1 },
                    { 4, 8, 1 },
                    { 5, 10, 1 },
                    { 6, 2, 2 },
                    { 7, 4, 2 },
                    { 8, 4, 2 },
                    { 9, 4, 2 },
                    { 10, 8, 2 },
                    { 11, 2, 3 },
                    { 12, 10, 3 },
                    { 13, 14, 3 },
                    { 14, 4, 3 },
                    { 15, 10, 3 },
                    { 16, 2, 4 },
                    { 17, 18, 4 },
                    { 18, 4, 4 },
                    { 19, 6, 4 },
                    { 20, 20, 4 },
                    { 21, 4, 5 },
                    { 22, 6, 5 },
                    { 23, 8, 5 },
                    { 24, 10, 5 },
                    { 25, 12, 5 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CustomerId", "Date", "PartySize", "RestaurantId", "TableId" },
                values: new object[,]
                {
                    { 1, 5, new DateTime(2023, 12, 30, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(636), 2, 1, 1 },
                    { 2, 1, new DateTime(2023, 12, 31, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(715), 4, 2, 7 },
                    { 3, 2, new DateTime(2023, 12, 31, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(722), 6, 2, 10 },
                    { 4, 3, new DateTime(2023, 12, 28, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(729), 8, 3, 15 },
                    { 5, 4, new DateTime(2023, 12, 30, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(736), 10, 1, 5 },
                    { 6, 5, new DateTime(2023, 12, 30, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(744), 8, 3, 15 },
                    { 7, 1, new DateTime(2023, 12, 30, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(750), 14, 3, 13 },
                    { 8, 2, new DateTime(2023, 12, 27, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(757), 2, 3, 11 },
                    { 9, 3, new DateTime(2023, 12, 29, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(763), 4, 4, 18 },
                    { 10, 1, new DateTime(2023, 12, 25, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(771), 20, 4, 20 },
                    { 11, 3, new DateTime(2023, 12, 29, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(778), 6, 5, 22 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "EmployeeId", "ReservationId", "TotalAmount" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 30, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(1191), 2, 1, 37.96m },
                    { 2, new DateTime(2023, 12, 31, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(1205), 6, 2, 33.97m },
                    { 3, new DateTime(2023, 12, 28, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(1212), 10, 4, 68.94m },
                    { 4, new DateTime(2023, 12, 30, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(1219), 13, 9, 87.94m },
                    { 5, new DateTime(2023, 12, 29, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(1226), 16, 11, 36.97m },
                    { 6, new DateTime(2023, 12, 30, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(1234), 2, 5, 33.96m },
                    { 7, new DateTime(2023, 12, 31, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(1241), 7, 3, 95.94m },
                    { 8, new DateTime(2023, 12, 27, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(1248), 10, 8, 110.91m },
                    { 9, new DateTime(2023, 12, 30, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(1254), 13, 9, 37.98m },
                    { 10, new DateTime(2023, 12, 30, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(1263), 13, 9, 39.97m },
                    { 11, new DateTime(2023, 12, 28, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(1269), 10, 4, 23.98m },
                    { 12, new DateTime(2023, 12, 28, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(1275), 10, 4, 13.99m },
                    { 13, new DateTime(2023, 12, 29, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(1282), 16, 11, 57.96m },
                    { 14, new DateTime(2023, 12, 29, 16, 49, 29, 425, DateTimeKind.Local).AddTicks(1289), 16, 11, 15.99m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "MenuItemId", "OrderId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 2 },
                    { 2, 2, 1, 2 },
                    { 3, 4, 2, 1 },
                    { 4, 5, 2, 1 },
                    { 5, 6, 2, 1 },
                    { 6, 7, 3, 3 },
                    { 7, 9, 3, 3 },
                    { 8, 10, 4, 2 },
                    { 9, 11, 4, 2 },
                    { 10, 12, 4, 2 },
                    { 11, 13, 5, 1 },
                    { 12, 14, 5, 1 },
                    { 13, 15, 5, 1 },
                    { 14, 1, 6, 2 },
                    { 15, 3, 6, 2 },
                    { 16, 4, 7, 6 },
                    { 17, 7, 8, 3 },
                    { 18, 8, 8, 3 },
                    { 19, 9, 8, 3 },
                    { 20, 10, 9, 2 },
                    { 21, 11, 10, 1 },
                    { 22, 12, 10, 2 },
                    { 23, 7, 11, 2 },
                    { 24, 8, 12, 1 },
                    { 25, 13, 13, 2 },
                    { 26, 14, 13, 2 },
                    { 27, 14, 14, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
