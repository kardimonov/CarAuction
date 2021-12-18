using CarAuction.Logic.Models;
using MediatR;

namespace CarAuction.Logic.Commands.Car
{
    public class AssignToAuctionCommand : IRequest<ResponseModel>
    {
        public int CarId { get; set; }
        public int AuctionId { get; set; }
        public int AuctionPrice { get; set; }
    }
}
