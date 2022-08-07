using AutoMapper;
using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using CarAuction.Logic.Commands.Auction;
using CarAuction.Logic.Interfaces;
using CarAuction.Logic.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuction.Logic.Handlers
{
    internal class AuctionCommandHandler :
        IRequestHandler<AddAuctionCommand, Unit>,
        IRequestHandler<UpdateAuctionCommand, ResponseModel>,
        IRequestHandler<DeleteAuctionCommand, Unit>
    {
        private readonly IAuctionRepository _repo;
        private readonly IAuctionManagerService _auctionService;
        private readonly IMapper _mapper;

        public AuctionCommandHandler(IAuctionRepository repository, IAuctionManagerService service, IMapper map)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
            _auctionService = service;
            _mapper = map;
        }        

        public async Task<Unit> Handle(AddAuctionCommand request, CancellationToken token = default)
        {
            var auction = _mapper.Map<Auction>(request);
            var id = await _repo.Create(auction);

            await _auctionService.StartAuction(auction.StartTime, id);
            await _auctionService.EndAuction(auction.EndTime, id);

            return Unit.Value;
        }

        public async Task<ResponseModel> Handle(UpdateAuctionCommand request, CancellationToken cancellationToken = default)
        {
            var auctionToUpdate = await _repo.GetById(request.Id);            
            var auction = _mapper.Map<Auction>(request);

            await _repo.Update(auction);

            if (request.StartTime != auctionToUpdate.StartTime)
            {
                await _auctionService.RescheduleAuctionStart(auction.StartTime, auction.Id);
            }
            if (request.EndTime != auctionToUpdate.EndTime)
            {
                await _auctionService.RescheduleAuctionEnd(auction.EndTime, auction.Id);
            }
            
            return new ResponseModel 
            { 
                Result = true, 
                Message = $"Auction with id: {auction.Id} has been updated successfully" 
            };
        }

        public async Task<Unit> Handle(DeleteAuctionCommand request, CancellationToken cancellationToken = default)
        {
            await _repo.Delete(request.Id);
            await _auctionService.RemoveAuctionJobs(request.Id);
            return Unit.Value;
        }
    }
}
