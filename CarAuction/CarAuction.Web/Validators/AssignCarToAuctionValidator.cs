using CarAuction.Logic.Commands.Car;
using FluentValidation;

namespace CarAuction.Web.Validators
{
    public class AssignCarToAuctionValidator : AbstractValidator<AssignToAuctionCommand>
    {
        public AssignCarToAuctionValidator()
        {
            RuleFor(i => i.AuctionPrice)
                .GreaterThan(0)
                .WithMessage($"Vehicle price in auction must be more than 0");

            RuleFor(i => i.AuctionId)
                .NotEmpty()
                .WithMessage($"Auction id cannot be empty");

            RuleFor(i => i.CarId)
                .NotEmpty()
                .WithMessage($"Car id cannot be empty");
        }           
    }
}
