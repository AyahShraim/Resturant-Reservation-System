using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class FindingCustomersWithCertainPartySizeReservationProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE sp_FindCustomersWithThresholdLargerPartySize
                    @PartySizeThreshold INT
                    AS
                    BEGIN
                        SELECT DISTINCT 
						    CustomerId,
						    Customers.FirstName,
						    Customers.LastName,
						    Customers.Email,
						    Reservations.Id,
						    Reservations.PartySize
                        FROM
						    Customers 
                        INNER JOIN
						    Reservations 
					    ON Customers.Id = Reservations.CustomerId
                        WHERE Reservations.PartySize > @PartySizeThreshold;
                    END
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_FindCustomersWithLargePartySize;");
        }
    }
}
