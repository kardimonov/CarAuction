using CarAuction.Data.Context;
using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CarAuction.Data.Repositories
{
    internal class AuthRepository : IAuthRepository
    {
        private readonly ApplicationContext _db;

        public AuthRepository(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<User> CheckPassword(string userName, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
        }

        public async Task AddCustomer(string userName, string password)
        {
            _db.Users.Add(new User()
            {
                UserName = userName,
                Password = password
            });
            await _db.SaveChangesAsync();
        }
    }
}
