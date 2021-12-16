using CarAuction.Data.Context;
using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarAuction.Data.Repositories
{
    internal class CarRepository : ICarRepository
    {
        private readonly ApplicationContext _db;

        public CarRepository(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<Car> GetById(int id)
        {
            return await _db.Cars.FindAsync(id);
        }

        public async Task<Car> GetDetailsById(int id)
        {
            return await _db.Cars
                .Include(s => s.Assignments)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<int> GetMSRPrice(int id)
        {
            return await _db.Cars
                .Where(c => c.Id == id)
                .Select(c => c.MSRPrice)
                .FirstOrDefaultAsync();
        }

        public async Task Create(Car car)
        {
            _db.Cars.Add(car);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Car car)
        {
            _db.Cars.Update(car);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _db.Cars.Remove(await _db.Cars.FindAsync(id));
            await _db.SaveChangesAsync();
        }
    }
}
