using CarAuction.Logic.Models;
using FluentValidation;

namespace CarAuction.Web.Validators
{
    public class RegisterUserValidator : AbstractValidator<UserRegisterModel>
    {
        public RegisterUserValidator()
        {
            RuleFor(i => i.UserName)
                .NotEmpty()
                .WithMessage($"Enter login")
                .Length(3, 20)
                .WithMessage($"Login must have from 3 to 20 characters.");

            RuleFor(i => i.Password)
                .NotEmpty()
                .WithMessage($"Enter password")
                .Length(3, 20)
                .WithMessage($"Password must have from 3 to 20 characters.");
        }
    }
}
