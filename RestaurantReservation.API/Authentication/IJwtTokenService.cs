using RestaurantReservation.API.Models.Authentication;

namespace RestaurantReservation.API.Authentication
{
    public interface IJwtTokenService
    {
        Task<string> TryGenerateToken(AuthenticationCredentials authenticationCredentials);
    }
}
