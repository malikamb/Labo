using Labo.BLL.Interfaces;
using Labo.IL.Configurations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Labo.IL.Services
{
    public class JwtManager(JwtConfiguration config, JwtSecurityTokenHandler tokenHandler) : IJwtManager
    {
        private SecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Signature));

        public string CreateToken(string identifier, string email, string role)
        {
            DateTime now = DateTime.Now;
            JwtSecurityToken token = new(
                config.Issuer,
                config.Audience,
                CreateClaims(identifier, email, role),
                now,
                now.AddSeconds(config.LifeTime),
                CreateCredentials()
            );

            return tokenHandler.WriteToken(token);
        }

        private SigningCredentials CreateCredentials()
        {
            return new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
        }

        private static IEnumerable<Claim> CreateClaims(string identifier, string email, string role)
        {
            yield return new Claim(ClaimTypes.NameIdentifier, identifier);
            yield return new Claim(ClaimTypes.Role, role);
            yield return new Claim(ClaimTypes.Email, email);
        }
    }
}
