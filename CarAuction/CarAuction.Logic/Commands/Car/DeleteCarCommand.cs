using MediatR;

namespace CarAuction.Logic.Commands.Car
{
    public class DeleteCarCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
