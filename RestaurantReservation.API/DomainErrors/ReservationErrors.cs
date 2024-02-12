using RestaurantReservation.API.Models.ErrorsHandling;

namespace RestaurantReservation.API.DomainErrors
{
    public class ReservationErrors
    {
        public static Error NotFound(int id) => Error.NotFound(
            "Reservation.NotFound", $" Reservation with id {id} not found");

        public static Error NotMatchingTableRestaurant(int RestaurantId, int tableId) => Error.Validate(
            "Reservation.NotValid", $" Reservation can't add, couldn't find table id = {tableId} related to restaurant with id {RestaurantId}");
    }
}
