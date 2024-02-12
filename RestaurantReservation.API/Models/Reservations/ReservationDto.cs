using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.API.Models.Reservations
{
    public class ReservationDto
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int PartySize { get; set; }

        public int RestaurantId { get; set; }

        public int CustomerId { get; set; }

        public int TableId { get; set; }
    }
}
