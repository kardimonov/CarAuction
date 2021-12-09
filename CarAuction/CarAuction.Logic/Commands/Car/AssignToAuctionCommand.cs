using MediatR;

namespace CarAuction.Logic.Commands.Car
{
    public class AssignToAuctionCommand : IRequest<Unit>
    {
        public int CarId { get; set; }
        public int AuctionId { get; set; }
    }
}
