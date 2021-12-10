using CarAuction.Data.Models;

namespace CarAuction.Logic.Interfaces
{
    public interface ICarGradeService : IService
    {
        int CalculateGrade(Car car);
    }
}
