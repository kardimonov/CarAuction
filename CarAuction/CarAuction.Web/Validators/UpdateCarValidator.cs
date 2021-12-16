using CarAuction.Logic.Commands.Car;
using FluentValidation;
using System;

namespace CarAuction.Web.Validators
{
    public class UpdateCarValidator : AbstractValidator<UpdateCarCommand>
    {
        public UpdateCarValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(i => i.Id)
                .NotEmpty()
                .WithMessage($"Car id cannot be empty");

            RuleFor(i => i.Manufacture)
                .NotEmpty()
                .WithMessage($"Enter manufacture of the car");

            RuleFor(i => i.Model)
                .NotEmpty()
                .WithMessage($"Enter model of the car");

            RuleFor(i => i.VIN)
                .Matches("[A-HJ-NPR-Z0-9]{17}")
                .WithMessage($"Enter correct VIN");

            RuleFor(i => i.Odometer)
                .InclusiveBetween(0, 9999999).WithMessage($"Enter correct odometer value");

            RuleFor(i => i.Year)
                .InclusiveBetween(1800, DateTime.UtcNow.Year).WithMessage($"Enter correct year of production");

            RuleFor(i => i.ExteriorColor)
                .NotEmpty().WithMessage($"Enter exterior color");

            RuleFor(i => i.InteriorColor)
                .NotEmpty().WithMessage($"Enter interior color");

            RuleFor(i => i.MSRPrice)
                .GreaterThan(0)
                .WithMessage($"Manufacture's Suggested Retail Price must be more than 0");

            RuleFor(i => i.MSRPrice)
                .GreaterThan(0)
                .WithMessage($"Manufacture's Suggested Retail Price must be more than 0");
        }
    }
}
