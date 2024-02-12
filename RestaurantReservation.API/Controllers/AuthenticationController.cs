using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Authentication;
using RestaurantReservation.API.Models.Authentication;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/authentication")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public readonly IJwtTokenService _jwtTokenService;
        public AuthenticationController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Authenticate(AuthenticationCredentials authenticationCredentials)
        {
            var token = await _jwtTokenService.TryGenerateToken(authenticationCredentials);
            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(new
            {
                success = true,
                message = "Token generated successfully.",
                token
            });
        }
    }
}
