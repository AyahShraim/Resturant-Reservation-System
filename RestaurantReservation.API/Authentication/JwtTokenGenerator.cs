using Microsoft.Extensions.Options;
using RestaurantReservation.API.Models.Authentication;
using System.IdentityModel.Tokens.Jwt;



namespace RestaurantReservation.API.Authentication
{
    public class JwtTokenGenerator : IJwtTokenService
    {
        private readonly SigningConfiguration _signingConfiguration;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(SigningConfiguration signingConfiguration, IOptions<JwtConfiguration> jwtConfiguration, IConfiguration configuration)
        {
            _signingConfiguration = signingConfiguration;
            _jwtConfiguration = jwtConfiguration?.Value ?? throw new ArgumentNullException(nameof(jwtConfiguration), "JwtConfiguration must not be null.");
            _configuration = configuration;
        }

        public async Task<string> TryGenerateToken(AuthenticationCredentials authenticationCredentials)
        {
            var securityKey = _configuration["AuthenticationUserKey"];
            var isValid = await Task.Run(() => ValidateCredentials(securityKey, authenticationCredentials.Key));

            if (!isValid)
            {
                return null;
            }

            var jwtSecurityToken = new JwtSecurityToken(
                _jwtConfiguration.Issuer,
                _jwtConfiguration.Audience,
                null,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(_jwtConfiguration.TokenExpiryHours),
                _signingConfiguration.SigningCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;   
        }

        private bool ValidateCredentials(string storedKey, string? key)
        {
            return storedKey == key;
        }
    }
}
