using CarAuction.Logic.Models;
using MediatR;

namespace CarAuction.Logic.Commands
{
    public class AddBidCommand : IRequest<ResponseModel>
    {
        public int Amount { get; set; }
        public int AuctionCarId { get; set; }
        public string UserId { get; set; }        
    }
}
