using CarAuction.Data.Models;
using System.Threading.Tasks;

namespace CarAuction.Data.Interfaces
{
    public interface IAuthRepository : IRepository
    {
        Task<User> GetByName(string userName);
        Task AddCustomer(User user);
        bool ExistsLogin(string name);
    }
}
