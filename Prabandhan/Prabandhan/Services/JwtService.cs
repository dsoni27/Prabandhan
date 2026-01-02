using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.Services
{
    public class JwtService
    {
        public UserClaims DecodeToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(accessToken);

            var claims = new UserClaims
            {
                UserId = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                Email = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                Name = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                AvatarUrl = jwt.Claims.FirstOrDefault(c => c.Type == "AvatarUrl")?.Value,
                Expiry = GetExpiry(jwt),
                Permissions = GetPermissions(jwt)
            };

            return claims;
        }

        private DateTime GetExpiry(JwtSecurityToken jwt)
        {
            var exp = jwt.Claims.First(c => c.Type == "exp").Value;
            return DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp)).UtcDateTime;
        }

        private List<JobPermission> GetPermissions(JwtSecurityToken jwt)
        {
            var permissionJson = jwt.Claims.First(c => c.Type == "permission").Value;
            return JsonConvert.DeserializeObject<List<JobPermission>>(permissionJson);
        }
    }
}
