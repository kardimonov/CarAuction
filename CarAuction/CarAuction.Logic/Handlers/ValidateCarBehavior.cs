using CarAuction.Data.Interfaces;
using CarAuction.Logic.Commands.Car;
using CarAuction.Logic.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuction.Logic.Handlers
{
    public class ValidateCarBehavior : 
        IPipelineBehavior<AssignToAuctionCommand, ResponseModel>
    {
        private readonly ICarRepository _repo;

        public ValidateCarBehavior(ICarRepository repository)
        {
            _repo = repository;
        }

        public async Task<ResponseModel> Handle(AssignToAuctionCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<ResponseModel> next)
        {
            var carToAssign = await _repo.GetDetailsById(request.CarId);
            if (carToAssign == null)
            {
                return new ResponseModel { Result = false, Message = $"Car with id: {request.CarId} is not found" };
            }
            if (carToAssign.Assignments.Any(i => i.AuctionId == request.AuctionId))
            {
                return new ResponseModel { Result = false, Message = "This car has already been assigned to this auction" };
            }

            return await next();
        }
    }
}
