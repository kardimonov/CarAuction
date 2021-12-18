using CarAuction.Logic.Interfaces;
using MediatR;

namespace CarAuction.Logic.Queries.Cars
{
    public class GetRecommendedPriceQuery : IRequest<int>, ICarGradeModel
    {
        public int MSRPrice { get; set; }
        public int Odometer { get; set; }
        public int Year { get; set; }
        public bool StrongScratches { get; set; }
        public bool SmallScratches { get; set; }
        public bool SuspensionProblems { get; set; }
        public bool ElectricsFailures { get; set; }        
    }
}
