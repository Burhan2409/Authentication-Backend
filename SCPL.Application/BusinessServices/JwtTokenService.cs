using Microsoft.IdentityModel.Tokens;
using SCPL.Application.BusinessInterfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPL.Application.BusinessServices
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly string _key = "FE21E44D2C263AACAF64BB329B619FE21E44D2C263AACAF64BB329B619";

        public string GenerateJwtToken(string email)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(_key);
            var key = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new Dictionary<string, object>
            {
                { "useremail", email }
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = credentials,
                Claims = claims
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
