using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class Views : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW ReservationsDetails AS
                SELECT
                    R.Id AS Id,
                    R.Date AS ReservationDate,
                    C.Id AS CustomerId,
                    C.FirstName AS CustomerFirstName,
                    C.LastName AS CustomerLastName,
                    RS.Id AS RestaurantId,
                    RS.Name AS RestaurantName,
                    RS.Address AS RestaurantAddress
                FROM
                    Reservations R
                JOIN
                    Customers C
                ON
                    R.CustomerId = C.Id
                JOIN
                    Restaurants RS
                ON
                    R.RestaurantId = RS.Id;");

            migrationBuilder.Sql(@"CREATE VIEW EmployeesWithRestaurantDetails AS
                SELECT
                    E.Id AS EmployeeId,
                    E.FirstName AS EmployeeFirstName,
                    E.LastName AS EmployeeLastName,
	                E.Position AS Position,
                    R.Id AS RestaurantId,
                    R.Name AS RestaurantName,
                    R.Address AS RestaurantAddress,
	                R.PhoneNumber AS RestaurantPhone
                FROM
	                Employees E
                JOIN
	                Restaurants R 
                ON
	                E.RestaurantId = R.Id;");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS ReservationsDetails;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS EmployeesWithRestaurantDetails;");
        }
    }
}
