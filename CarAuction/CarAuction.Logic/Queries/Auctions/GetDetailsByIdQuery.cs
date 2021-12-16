using CarAuction.Logic.Models;
using MediatR;

namespace CarAuction.Logic.Queries.Auctions
{
    public class GetDetailsByIdQuery : IRequest<AuctionModel>
    {
        public int Id { get; set; }
    }
}
