using CarAuction.Logic.Models;
using MediatR;

namespace CarAuction.Logic.Queries.Auctions
{
    public class GetDetailsByIdQuery : IRequest<AuctionWithCarsModel>
    {
        public int Id { get; set; }
    }
}
