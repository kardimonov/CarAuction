using MediatR;

namespace CarAuction.Logic.Commands
{
    public class AddBidCommand : IRequest<bool>
    {
        public int AuctionId { get; set; }
        public int CarId { get; set; }
        public string UserId { get; set; }
        public int Amount { get; set; }
    }
}
