using CarAuction.Logic.Commands.Auction;
using FluentValidation;
using System;

namespace CarAuction.Web.Validators
{
    public class AddAuctionValidator : AbstractValidator<AddAuctionCommand>
    {
        public AddAuctionValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                .WithMessage($"Enter Auction name");

            RuleFor(i => i.StartTime)
               .GreaterThanOrEqualTo(DateTime.Today)
               .WithMessage($"Enter correct date and time of the Auction start");

            RuleFor(i => i.EndTime)
                .GreaterThan(DateTime.Today)
                .WithMessage($"Enter correct date and time of the Auction end");

            RuleFor(i => i)
                .Must(ValidateEndTime)
                .WithMessage($"Auction beginning cannot be after the auction end");
        }

        private bool ValidateEndTime(AddAuctionCommand source) =>
            source.StartTime < source.EndTime;
    }
}
