using AutoMapper;
using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using CarAuction.Logic.Commands.Car;
using CarAuction.Logic.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuction.Logic.Handlers
{
    internal class CarCommandHandler : 
        IRequestHandler<AddCarCommand, Unit>,
        IRequestHandler<UpdateCarCommand, Unit>,
        IRequestHandler<DeleteCarCommand, Unit>,
        IRequestHandler<AssignToAuctionCommand, Unit>
    {
        private readonly ICarRepository _repo;
        private readonly ICarGradeService _service;
        private readonly IMapper _mapper;

        public CarCommandHandler(ICarRepository repository, ICarGradeService service, IMapper map)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
            _service = service;
            _mapper = map;
        }

        public async Task<Unit> Handle(AddCarCommand request, CancellationToken cancellationToken = default)
        {
            var car = _mapper.Map<Car>(request);
            car.Grade = _service.CalculateGrade(car);
            await _repo.Create(car);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateCarCommand request, CancellationToken cancellationToken = default)
        {
            var car = _mapper.Map<Car>(request);
            car.Grade = _service.CalculateGrade(car);
            await _repo.Update(car);
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
