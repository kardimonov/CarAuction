using CarAuction.Data.Models;
using System.Threading.Tasks;

namespace CarAuction.Data.Interfaces
{
    public interface ICarRepository : IRepository
    {
        Task<Car> GetById(int id);
        Task Create(Car auction);
        Task Update(Car auction);
        Task Delete(int id);
    }
}
