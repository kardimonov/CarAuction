using CarAuction.Logic.Commands;
using FluentValidation;

namespace CarAuction.Web.Validators
{
    public class AddBidValidator : AbstractValidator<AddBidCommand>
    {
        public AddBidValidator()
        {
            RuleFor(i => i.AuctionCarId)
                .NotEmpty()
                .WithMessage($"AuctionCar id cannot be empty");

            RuleFor(i => i.Amount)
                .NotEmpty()
                .WithMessage($"Enter the amount of bid");
        }  
    }
}
