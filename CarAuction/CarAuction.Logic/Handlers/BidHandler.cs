using AutoMapper;
using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using CarAuction.Logic.Commands;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuction.Logic.Handlers
{
    internal class BidHandler :
        IRequestHandler<AddBidCommand, bool>
    {
        private readonly IBidRepository _repo;
        private readonly IAuctionRepository _auctionRepo;
        private readonly IMapper _mapper;

        public BidHandler(IBidRepository repository, IAuctionRepository auctionRepository, IMapper map)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
            _auctionRepo = auctionRepository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = map;
        }

        public async Task<bool> Handle(AddBidCommand request, CancellationToken cancellationToken = default)
        {
            var auction = await _auctionRepo.GetById(request.AuctionId);
            var auctionCar = auction.Assignments.FirstOrDefault(i => i.AuctionId == request.AuctionId);
            if (auctionCar.CarId != request.CarId)
            {
                return false;
            }

            var bid = _mapper.Map<Bid>(request);
            await _repo.Create(bid);
            return true;
        }
    }
}
