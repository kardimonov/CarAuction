using CarAuction.Logic.Models;
using System.Threading.Tasks;

namespace CarAuction.Logic.Interfaces
{
    public interface IAuthService : IService
    {
        Task<LoginSuccessModel> LoginCustomer(UserModel model, string audience);
        Task AddCustomer(UserRegisterModel model);
    }
}
