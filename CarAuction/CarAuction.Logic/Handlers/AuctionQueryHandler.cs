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
        IRequestHandler<GetAuctionByIdQuery, Auction>,
        IRequestHandler<GetAllAuctionsQuery, List<Auction>>,
        IRequestHandler<GetDetailsByIdQuery, AuctionModel>,
        IRequestHandler<GetByAuctionCarIdQuery, Auction>
    {
        private readonly IAuctionRepository _repo;

        public AuctionQueryHandler(IAuctionRepository repository)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Auction> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken = default)
        {
            var auction =  await _repo.GetById(request.Id);
            return auction;
        }

        public async Task<List<Auction>> Handle(GetAllAuctionsQuery request, CancellationToken cancellationToken = default)
        {
            return await _repo.GetAll();
        }

        public async Task<AuctionModel> Handle(GetDetailsByIdQuery request, CancellationToken cancellationToken = default)
        {
            var auction = await _repo.GetByIdWithCarsAndBids(request.Id);
            if (auction == null)
            {
                return null;
            }

            return new AuctionModel
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
                    Bids = ac.Car.Bids.Select(b => new Bid
                    { 
                        Id = b.Id,
                        Amount = b.Amount,
                        Time = b.Time,
                        WinResult = b.WinResult
                    }).ToList() //Where(b => b.AuctionCarId == ac.Id).ToList()
                }).ToList()
            };           
        }

        public async Task<Auction> Handle(GetByAuctionCarIdQuery request, CancellationToken cancellationToken = default)
        {
            return await _repo.GetByAuctionCarId(request.AuctionCarId);
        }
    }
}
