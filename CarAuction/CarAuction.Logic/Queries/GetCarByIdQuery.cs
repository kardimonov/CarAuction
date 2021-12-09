using MediatR;
using CarAuction.Data.Models;

namespace CarAuction.Logic.Queries
{
    public class GetCarByIdQuery : IRequest<Car>
    {
        public int Id { get; set; }
    }
}
