using MediatR;

namespace CarAuction.Logic.Commands.Auction
{
    public class DeleteAuctionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
