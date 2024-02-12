using RestaurantReservation.API.Models.Employees;
using RestaurantReservation.API.Models.MenuItems;
using RestaurantReservation.API.Models.Reservations;
using RestaurantReservation.API.Models.Tables;
using RestaurantReservation.Db.Entities;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.API.Models.Restaurants
{
    public class RestaurantWithDetails
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string OpeningHours { get; set; }

        public int NumberOfTables {  get { return Tables.Count; } }

        public int NumberOfEmployees { get { return Employees.Count; } }

        public int NumberOfMenuItems { get { return MenuItems.Count; } }

        public int NumberOfReservations { get { return Reservations.Count; } }
       
        public List<TableWithoutDetailsDto> Tables { get; set; } = new();
        
        public List<ReservationWithoutDetailsDto> Reservations { get; set; } = new();
        
        public List<EmployeeWithoutDetailsDto> Employees { get; set; } = new();
        
        public List<MenuItemWithoutDetailsDto> MenuItems { get; set; } = new();
    }
}
