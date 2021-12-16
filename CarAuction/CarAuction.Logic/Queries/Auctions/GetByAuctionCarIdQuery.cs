using CarAuction.Data.Models;
using MediatR;

namespace CarAuction.Logic.Queries.Auctions
{
    public class GetByAuctionCarIdQuery : IRequest<Auction>
    {
        public int AuctionCarId { get; set; }
    }
}
