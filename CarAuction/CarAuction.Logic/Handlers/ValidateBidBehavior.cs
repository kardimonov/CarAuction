using CarAuction.Data.Enums;
using CarAuction.Data.Interfaces;
using CarAuction.Logic.Commands;
using CarAuction.Logic.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuction.Logic.Handlers
{
    public class ValidateBidBehavior :
        IPipelineBehavior<AddBidCommand, ResponseModel>
    {
        private readonly IAuctionCarRepository _auctionCarRepo;

        public ValidateBidBehavior(IAuctionCarRepository auctionCarRepo)
        {
            _auctionCarRepo = auctionCarRepo;
        }

        public async Task<ResponseModel> Handle(AddBidCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<ResponseModel> next)
        {
            var auctionStatus = await _auctionCarRepo.GetAuctionStatus(request.AuctionCarId);
            if (auctionStatus != AuctionStatus.Started)
            {
                return new ResponseModel { Result = false, Message = $"The bids are accepted only while the auction is open" };
            }

            var auctionCarPrice = await _auctionCarRepo.GetAuctionCarPrice(request.AuctionCarId);
            if (request.Amount < auctionCarPrice)
            {
                return new ResponseModel { Result = false, Message = $"Bid amount must be equal or bigger than car price in the auction" };
            }

            return await next();
        }
    }
}
