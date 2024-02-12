using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.API.Models.Tables
{
    public class TableDto
    {
        [Required]
        public int Capacity { get; set; }

        public int RestaurantId { get; set; }
    }
}
