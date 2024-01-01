using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class CalculateRestaurantTotalRevenueFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR ALTER FUNCTION fn_CalculateRestaurantTotalRevenue (@RestaurantID INT)
                RETURNS DECIMAL(18,2)
                AS
                BEGIN
                    DECLARE @TotalRevenue DECIMAL(18,2)

                    SELECT
                        @totalRevenue = COALESCE(SUM(orders.TotalAmount), 0)
                    FROM 
                        Orders 
                    INNER JOIN
                        Reservations
                    ON
                        Reservations.Id = Orders.ReservationId
                    WHERE
                        Reservations.RestaurantId = @RestaurantID;
                    RETURN @TotalRevenue
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS dbo.fn_CalculateRestaurantTotalRevenue");
        }
    }
}
