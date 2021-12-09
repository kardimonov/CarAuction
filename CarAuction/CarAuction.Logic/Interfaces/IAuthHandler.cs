using System.Security.Claims;

namespace CarAuction.Logic.Interfaces
{
    public interface IAuthHandler
    {
        ClaimsIdentity GetIdentity(string userName, string role, int userId);
    }
}
