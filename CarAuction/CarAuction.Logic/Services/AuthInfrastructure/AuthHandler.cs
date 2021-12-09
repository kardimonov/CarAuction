using CarAuction.Logic.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;

namespace CarAuction.Logic.Services.AuthInfrastructure
{
    public class AuthHandler : IAuthHandler
    {
        public ClaimsIdentity GetIdentity(string userName, string role, int userId)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
                    new Claim("user_id", userId.ToString())
                };
            var claimsIdentity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );

            return claimsIdentity;
        }
    }
}
