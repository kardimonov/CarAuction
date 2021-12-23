using CarAuction.Logic.Models;
using MediatR;
using System.Collections.Generic;

namespace CarAuction.Logic.Queries.Auctions
{
    public class GetAllAuctionsQuery : IRequest<IEnumerable<AuctionModel>>
    {
    }
}
