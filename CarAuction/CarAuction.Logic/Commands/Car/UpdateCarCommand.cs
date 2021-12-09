using MediatR;

namespace CarAuction.Logic.Commands.Car
{
    public class UpdateCarCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string VIN { get; set; }
        public int Odometer { get; set; }
        public int Year { get; set; }
        public string ExteriorColor { get; set; }
        public string InteriorColor { get; set; }
        public bool Scratches { get; set; }
        public bool SuspensionProblems { get; set; }
        public bool ElectricsFailures { get; set; }
    }
}
