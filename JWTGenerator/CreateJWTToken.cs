using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JWTGenerator
{
    public class CreateJWTToken : IJWTTokenCreator
    {
        private readonly string _tokenString;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly DateTime _expiryTime;
        private readonly List<Claim> _claims;

        public CreateJWTToken(string Username, string Role, string tokenString, string issuer, string audience, DateTime expiryTime)
        {
            List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Name, Username), new Claim(ClaimTypes.Role, Role) };
            _claims = claims;
            _tokenString = tokenString;
            _issuer = issuer;
            _audience = audience;
            _expiryTime = expiryTime;
        }

        public CreateJWTToken(string Username, List<string> Roles, string tokenString, string issuer, string audience, DateTime expiryTime)
        {
            List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Name, Username) };

            foreach (var role in Roles)
            {
                claims.Add(new Claim ( ClaimTypes.Role, role));
            }

            _claims = claims;
            _tokenString = tokenString;
            _issuer = issuer;
            _audience = audience;
            _expiryTime = expiryTime;
        }

        public string GenerateJWTToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenString));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer : _issuer, 
                audience : _audience, 
                claims : _claims,
                expires : _expiryTime,
                signingCredentials : credentials
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
