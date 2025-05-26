using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HIPO.Core
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly string _secretKey;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _secretKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT secret key is missing.");
        }

        public string GenerateToken(string aceid, string email)
        {
            if (string.IsNullOrWhiteSpace(aceid))
                throw new ArgumentException("AceID cannot be null or empty.", nameof(aceid));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            if (!int.TryParse(_configuration["Jwt:ExpireTime"], out int expirationMinutes))
                throw new InvalidOperationException("JWT expiration time is missing or invalid.");

            var expiration = DateTime.UtcNow.AddMinutes(expirationMinutes);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, aceid),
            new Claim(ClaimTypes.NameIdentifier, aceid),
            new Claim(ClaimTypes.Email, email),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            //role claim
        };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiration,
                signingCredentials: creds
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
