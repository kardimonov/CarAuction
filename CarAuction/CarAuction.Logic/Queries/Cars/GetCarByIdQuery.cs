using MediatR;
using CarAuction.Data.Models;

namespace CarAuction.Logic.Queries.Cars
{
    public class GetCarByIdQuery : IRequest<Car>
    {
        public int Id { get; set; }
    }
}
