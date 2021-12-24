using AutoMapper;
using CarAuction.Data.Enums;
using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using CarAuction.Logic.Commands;
using CarAuction.Logic.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuction.Logic.Handlers
{
    internal class BidHandler :
        IRequestHandler<AddBidCommand, ResponseModel>
    {
        private readonly IBidRepository _repo;
        private readonly IAuctionCarRepository _auctionCarRepo;
        private readonly IMapper _mapper;

        public BidHandler(IBidRepository repository, IAuctionCarRepository auctionCarRepo, IMapper map)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = map;
            _auctionCarRepo = auctionCarRepo;
        }

        public async Task<ResponseModel> Handle(AddBidCommand request, CancellationToken cancellationToken = default)
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

            var bid = _mapper.Map<Bid>(request);
            await _repo.Create(bid);

            return new ResponseModel { Result = true, Message = $"The Bid is accepted!" };
        }
    }
}
