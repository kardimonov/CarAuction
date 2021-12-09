using CarAuction.Data.Models;
using MediatR;
using System.Collections.Generic;

namespace CarAuction.Logic.Queries.Auctions
{
    public class GetAllAuctionsQuery : IRequest<List<Auction>>
    {
    }
}
