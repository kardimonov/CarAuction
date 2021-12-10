using CarAuction.Logic.Commands.Car;
using FluentValidation;
using System;

namespace CarAuction.Web.Validators
{
    public class AddCarValidator : AbstractValidator<AddCarCommand>
    {
        public AddCarValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(i => i.VIN)
                .NotEmpty()
                .WithMessage($"Enter VIN")
                .Length(17, 17)
                .WithMessage($"The field must have 17 characters");            

            RuleFor(i => i.Odometer)
                .InclusiveBetween(0, 9999999)
                .WithMessage($"Enter correct odometer value");

            RuleFor(i => i.Year)
                .InclusiveBetween(1800, DateTime.Today.Year)
                .WithMessage($"Enter correct year of production");
            
            RuleFor(i => i.ExteriorColor)
                .NotEmpty()
                .WithMessage($"Enter exterior color");

            RuleFor(i => i.InteriorColor)
                .NotEmpty()
                .WithMessage($"Enter interior color");
        }
    }
}
