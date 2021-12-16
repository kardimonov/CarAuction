namespace CarAuction.Logic.Interfaces
{
    public interface ICarGradeService : IService
    {
        int CalculateGrade(ICarGradeModel car);
    }
}
