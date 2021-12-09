using CarAuction.Data.Models;
using MediatR;

namespace CarAuction.Logic.Queries.Auctions
{
    public class GetAuctionByIdQuery : IRequest<Auction>
    {
        public int Id { get; set; }
    }
}
