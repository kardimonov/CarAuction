using AutoMapper;
using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using CarAuction.Logic.Commands.Car;
using CarAuction.Logic.Interfaces;
using CarAuction.Logic.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuction.Logic.Handlers
{
    internal class CarCommandHandler : 
        IRequestHandler<AddCarCommand, Unit>,
        IRequestHandler<UpdateCarCommand, Unit>,
        IRequestHandler<DeleteCarCommand, Unit>,
        IRequestHandler<AssignToAuctionCommand, ResponseModel>
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
            var grade = _service.CalculateGrade(request);
            var car = _mapper.Map<Car>(request, opt => opt.Items["Grade"] = grade);
            await _repo.Create(car);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateCarCommand request, CancellationToken cancellationToken = default)
        {            
            var grade = _service.CalculateGrade(request);
            var car = _mapper.Map<Car>(request, opt => opt.Items["Grade"] = grade);
            await _repo.Update(car);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteCarCommand request, CancellationToken cancellationToken = default)
        {
            await _repo.Delete(request.Id);
            return Unit.Value;
        }

        public async Task<ResponseModel> Handle(AssignToAuctionCommand request, CancellationToken cancellationToken = default)
        {
            var carToAssign = await _repo.GetDetailsById(request.CarId);
            if (carToAssign == null)
            {
                return new ResponseModel
                {
                    Result = false,
                    Message = $"Car with id: {request.CarId} is not found"
                };
            }
            if (carToAssign.Assignments.Any(i => i.AuctionId == request.AuctionId))
            {
                return new ResponseModel
                {
                    Result = false,
                    Message = "This car has already been assigned to this auction"
                };
            }

            carToAssign.Assignments.Add(new AuctionCar { AuctionId = request.AuctionId, CarId = request.CarId });
            await _repo.Update(carToAssign);
            return new ResponseModel
            {
                Result = true,
                Message = "This car was successfully assigned to this auction"
            };
        }
    }
}
