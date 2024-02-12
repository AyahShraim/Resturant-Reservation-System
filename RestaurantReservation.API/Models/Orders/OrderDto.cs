using RestaurantReservation.Db.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.API.Models.Orders
{
    public class OrderDto
    {

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public decimal TotalAmount { get; set; }

        public int EmployeeId { get; set; }

        public int ReservationId { get; set; }
    }
}
