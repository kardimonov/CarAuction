using CarAuction.Logic.Queries.Cars;
using FluentValidation;
using System;

namespace CarAuction.Web.Validators
{
    public class GetRecommendedPriceValidator : AbstractValidator<GetRecommendedPriceQuery>
    {
        public GetRecommendedPriceValidator()
        {
            RuleFor(i => i.MSRPrice)
                .GreaterThan(0)
                .WithMessage($"Manufacture's Suggested Retail Price must be more than 0");

            RuleFor(i => i.Odometer)
                .InclusiveBetween(0, 9999999)
                .WithMessage($"Enter correct odometer value");

            RuleFor(i => i.Year)
                .InclusiveBetween(1800, DateTime.UtcNow.Year)
                .WithMessage($"Enter correct year of production");
        }
    }
}
