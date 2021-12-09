using AutoMapper;
using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using CarAuction.Logic.Queries.Auctions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuction.Logic.Handlers
{
    internal class AuctionQueryHandler :
        IRequestHandler<GetAuctionByIdQuery, Auction>,
        IRequestHandler<GetAllAuctionsQuery, List<Auction>>
    {
        private readonly IAuctionRepository _repo;
        private readonly IMapper _mapper;

        public AuctionQueryHandler(IAuctionRepository repository, IMapper map)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = map;
        }

        public async Task<Auction> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken = default)
        {
            return await _repo.GetById(request.Id);
        }

        public async Task<List<Auction>> Handle(GetAllAuctionsQuery request, CancellationToken cancellationToken = default)
        {
            return await _repo.GetAll();
        }
    }
}
