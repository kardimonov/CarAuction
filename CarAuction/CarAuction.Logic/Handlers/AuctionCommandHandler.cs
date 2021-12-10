using AutoMapper;
using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using CarAuction.Logic.Commands.Auction;
using CarAuction.Logic.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuction.Logic.Handlers
{
    internal class AuctionCommandHandler :
        IRequestHandler<AddAuctionCommand, Unit>,
        IRequestHandler<UpdateAuctionCommand, Unit>,
        IRequestHandler<DeleteAuctionCommand, Unit>
    {
        private readonly IAuctionRepository _repo;
        private readonly IAuctionManagerService _auctionManagerService;
        private readonly IMapper _mapper;

        public AuctionCommandHandler(IAuctionRepository repository, IAuctionManagerService service, IMapper map)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
            _auctionManagerService = service;
            _mapper = map;
        }        

        public async Task<Unit> Handle(AddAuctionCommand request, CancellationToken token = default)
        {
            var auction = _mapper.Map<Auction>(request);
            var id = await _repo.Create(auction);
            //_ = Task.Run(async () => await _auctionManagerService.ManageAuction(auction.StartTime, id));
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateAuctionCommand request, CancellationToken cancellationToken = default)
        {
            var auction = _mapper.Map<Auction>(request);
            await _repo.Create(auction);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteAuctionCommand request, CancellationToken cancellationToken = default)
        {
            await _repo.Delete(request.Id);
            return Unit.Value;
        }


    }
}
