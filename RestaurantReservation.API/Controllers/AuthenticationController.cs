using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Authentication;
using RestaurantReservation.API.Models.Authentication;

namespace RestaurantReservation.API.Controllers
{
    /// <summary>
    /// Controller for  authentication
    /// </summary>
    [Route("api/authentication")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public readonly IJwtTokenService _jwtTokenService;

        /// <summary>
        /// Constructor for AuthenticationController
        /// </summary>
        /// <param name="jwtTokenService">The service for generating JWT tokens</param>
        public AuthenticationController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        /// <summary>
        /// Authenticate a user and generate a JWT token
        /// </summary>
        /// <param name="authenticationCredentials">The credentials for user authentication</param>
        /// <returns>A JWT token if authentication is successful</returns>
        /// <response code="200">Returns the JWT token if authentication is successful</response>
        /// <response code="401">If authentication fails</response>
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
