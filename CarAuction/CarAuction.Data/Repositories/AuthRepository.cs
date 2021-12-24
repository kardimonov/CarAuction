using CarAuction.Data.Context;
using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<User> GetByName(string userName)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task AddCustomer(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }
        
        public bool CheckIfLoginExists(string name)
        {
            return _db.Users.Any(u => u.UserName == name);
        }
    }
}
