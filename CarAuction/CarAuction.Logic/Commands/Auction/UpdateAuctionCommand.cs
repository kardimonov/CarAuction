using MediatR;
using System;

namespace CarAuction.Logic.Commands.Auction
{
    public class UpdateAuctionCommand : IRequest<Unit>
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
