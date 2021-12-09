using CarAuction.Data.Models;

namespace CarAuction.Logic.Interfaces
{
    public interface ICarGradeService
    {
        int CalculateGrade(Car car);
    }
}
