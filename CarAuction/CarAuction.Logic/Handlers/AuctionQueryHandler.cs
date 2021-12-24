using AutoMapper;
using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using CarAuction.Logic.Models;
using CarAuction.Logic.Queries.Auctions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuction.Logic.Handlers
{
    internal class AuctionQueryHandler :
        IRequestHandler<GetAuctionByIdQuery, AuctionModel>,
        IRequestHandler<GetAllAuctionsQuery, IEnumerable<AuctionModel>>,
        IRequestHandler<GetDetailsByIdQuery, AuctionWithCarsModel>
    {
        private readonly IAuctionRepository _repo;
        private readonly IMapper _mapper;

        public AuctionQueryHandler(IAuctionRepository repository, IMapper map)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = map;
        }

        public async Task<AuctionModel> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken = default)
        {
            var auction =  await _repo.GetById(request.Id);
            return _mapper.Map<AuctionModel>(auction);
        }

        public async Task<IEnumerable<AuctionModel>> Handle(GetAllAuctionsQuery request, CancellationToken cancellationToken = default)
        {
            var auctions = await _repo.GetAll();
            return _mapper.Map<IEnumerable<AuctionModel>>(auctions);
        }

        public async Task<AuctionWithCarsModel> Handle(GetDetailsByIdQuery request, CancellationToken cancellationToken = default)
        {
            var auction = await _repo.GetByIdWithCarsAndBids(request.Id);
            if (auction == null)
            {
                return null;
            }

            return new AuctionWithCarsModel
            {
                Id = auction.Id,
                Name = auction.Name,
                Status = auction.Status,
                Cars = auction.Assignments.Select(ac => new Car
                { 
                    Id = ac.Car.Id,
                    Manufacture = ac.Car.Manufacture,
                    Model = ac.Car.Model,
                    VIN = ac.Car.VIN,
                    Odometer = ac.Car.Odometer,
                    Year = ac.Car.Year,
                    ExteriorColor = ac.Car.ExteriorColor,
                    InteriorColor = ac.Car.InteriorColor,
                    StrongScratches = ac.Car.StrongScratches,
                    SmallScratches = ac.Car.SmallScratches,
                    SuspensionProblems = ac.Car.SuspensionProblems,
                    ElectricsFailures = ac.Car.ElectricsFailures,
                    MSRPrice = ac.Car.MSRPrice,
                    Grade = ac.Car.Grade,
                    Bids = ac.Bids.Select(b => new Bid
                    { 
                        Id = b.Id,
                        Amount = b.Amount,
                        Time = b.Time,
                        WinResult = b.WinResult
                    }).ToList() 
                }).ToList()
            };           
        }
    }
}
