using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using CarAuction.Logic.Queries;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuction.Logic.Handlers
{
    internal class CarQueryHandler :
        IRequestHandler<GetCarByIdQuery, Car>
    {
        private readonly ICarRepository _repo;

        public CarQueryHandler(ICarRepository repository)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Car> Handle(GetCarByIdQuery request, CancellationToken cancellationToken = default)
        {
            return await _repo.GetById(request.Id);
        }
    }
}
