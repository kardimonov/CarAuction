using MediatR;

namespace CarAuction.Logic.Queries.Cars
{
    public class GetMSRPriceQuery : IRequest<int>
    {
        public int CarId { get; set; }
    }
}
