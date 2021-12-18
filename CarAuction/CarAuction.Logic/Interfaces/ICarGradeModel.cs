namespace CarAuction.Logic.Interfaces
{
    public interface ICarGradeModel
    {
        public int Odometer { get; set; }
        public int Year { get; set; }
        public bool StrongScratches { get; set; }
        public bool SmallScratches { get; set; }
        public bool SuspensionProblems { get; set; }
        public bool ElectricsFailures { get; set; }
    }
}
