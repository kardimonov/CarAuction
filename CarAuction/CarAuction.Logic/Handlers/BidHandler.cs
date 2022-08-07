using AutoMapper;
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
        private readonly IMapper _mapper;

        public BidHandler(IBidRepository repository, IMapper map)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = map;
        }

        public async Task<ResponseModel> Handle(AddBidCommand request, CancellationToken cancellationToken = default)
        {
            var bid = _mapper.Map<Bid>(request);
            await _repo.Create(bid);
                        
            return new ResponseModel { Result = true, Message = $"The Bid is accepted!" };
        }
    }
}
