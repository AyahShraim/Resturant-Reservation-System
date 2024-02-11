using RestaurantReservation.Db.Enum;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.API.Models.Employees
{
    public class EmployeeDto
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public EmployeePosition Position { get; set; }

        public int RestaurantId { get; set; }
    }
}
