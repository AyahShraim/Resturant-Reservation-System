namespace RestaurantReservation.API.Models.ErrorsHandling
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
