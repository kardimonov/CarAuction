using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using CarAuction.Logic.Interfaces;
using CarAuction.Logic.Queries.Cars;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuction.Logic.Handlers
{
    internal class CarQueryHandler :
        IRequestHandler<GetCarByIdQuery, Car>,
        IRequestHandler<GetRecommendedPriceQuery, int>,
        IRequestHandler<GetMSRPriceQuery, int>
    {
        private readonly ICarRepository _repo;
        private readonly ICarGradeService _service;

        public CarQueryHandler(ICarRepository repository, ICarGradeService service)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
            _service = service;
        }

        public async Task<Car> Handle(GetCarByIdQuery request, CancellationToken cancellationToken = default)
        {
            var output = await _repo.GetById(request.Id);
            return output;
        }

        public async Task<int> Handle(GetRecommendedPriceQuery request, CancellationToken token)
        {
            var grade = await Task.Run(() => _service.CalculateGrade(request));
            return request.MSRPrice * 2 * grade / 100;
        }

        public async Task<int> Handle(GetMSRPriceQuery request, CancellationToken token)
        {
            return await _repo.GetMSRPrice(request.CarId);
        }
    }
}
