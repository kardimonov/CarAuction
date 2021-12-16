using CarAuction.Logic.Commands.Auction;
using FluentValidation;
using System;

namespace CarAuction.Web.Validators
{
    public class UpdateAuctionValidator : AbstractValidator<UpdateAuctionCommand>
    {
        public UpdateAuctionValidator()
        {
            RuleFor(i => i.Id)
                .NotEmpty()
                .WithMessage($"Auction id cannot be empty");

            RuleFor(i => i.Name)
                .NotEmpty()
                .WithMessage($"Enter Auction name");

            RuleFor(i => i.StartTime)
               .GreaterThanOrEqualTo(DateTime.UtcNow)
               .WithMessage($"Enter correct date and time of the Auction start");

            RuleFor(i => i.EndTime)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage($"Enter correct date and time of the Auction end");

            RuleFor(i => i)
                .Must(ValidateEndTime)
                .WithMessage($"Auction start must be earlier than its end");
        }

        private bool ValidateEndTime(UpdateAuctionCommand source) =>
            source.StartTime < source.EndTime;
    }
}
