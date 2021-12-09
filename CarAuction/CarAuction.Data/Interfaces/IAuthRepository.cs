using CarAuction.Data.Models;
using System.Threading.Tasks;

namespace CarAuction.Data.Interfaces
{
    public interface IAuthRepository : IRepository
    {
        Task<User> CheckPassword(string userName, string password);
        Task AddCustomer(string userName, string password);
    }
}
