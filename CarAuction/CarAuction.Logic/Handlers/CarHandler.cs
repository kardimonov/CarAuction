using AutoMapper;
using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using CarAuction.Logic.Commands.Car;
using CarAuction.Logic.Queries;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuction.Logic.Handlers
{
    internal class CarHandler : 
        IRequestHandler<GetCarByIdQuery, Car>,
        IRequestHandler<AddCarCommand, Unit>,
        IRequestHandler<UpdateCarCommand, Unit>,
        IRequestHandler<DeleteCarCommand, Unit>,
        IRequestHandler<AssignToAuctionCommand, Unit>
    {
        private readonly ICarRepository _repo;
        private readonly IMapper _mapper;

        public CarHandler(ICarRepository repository, IMapper map)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = map;
        }

        public async Task<Car> Handle(GetCarByIdQuery request, CancellationToken cancellationToken = default)
        {
            return await _repo.GetById(request.Id);
        }

        public async Task<Unit> Handle(AddCarCommand request, CancellationToken cancellationToken = default)
        {
            var car = _mapper.Map<Car>(request);
            await _repo.Create(car);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateCarCommand request, CancellationToken cancellationToken = default)
        {
            var car = _mapper.Map<Car>(request);
            await _repo.Create(car);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteCarCommand request, CancellationToken cancellationToken = default)
        {
            await _repo.Delete(request.Id);
            return Unit.Value;
        }

        public async Task<Unit> Handle(AssignToAuctionCommand request, CancellationToken cancellationToken = default)
        {
            var carToAssign = await _repo.GetById(request.CarId);
            carToAssign.Assignments.Add(new AuctionCar { CarId = request.CarId, AuctionId = request.AuctionId });
            await _repo.Update(carToAssign);
            return Unit.Value;
        }
    }
}
