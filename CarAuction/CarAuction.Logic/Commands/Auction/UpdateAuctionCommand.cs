using CarAuction.Logic.Models;
using MediatR;
using System;

namespace CarAuction.Logic.Commands.Auction
{
    public class UpdateAuctionCommand : IRequest<ResponseModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
