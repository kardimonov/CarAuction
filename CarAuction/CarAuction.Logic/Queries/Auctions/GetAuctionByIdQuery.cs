using CarAuction.Logic.Models;
using MediatR;

namespace CarAuction.Logic.Queries.Auctions
{
    public class GetAuctionByIdQuery : IRequest<AuctionModel>
    {
        public int Id { get; set; }
    }
}
