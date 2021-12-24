using CarAuction.Data.Interfaces;
using CarAuction.Logic.Models;
using FluentValidation;

namespace CarAuction.Web.Validators
{
    public class RegisterUserValidator : AbstractValidator<UserRegisterModel>
    {
        private readonly IAuthRepository _repo;

        public RegisterUserValidator(IAuthRepository repository)
        {
            _repo = repository;

            RuleFor(i => i.UserName)
                .NotEmpty()
                .WithMessage($"Enter login")
                .Length(3, 20)
                .WithMessage($"Login must have from 3 to 20 characters.")
                .Must(VerifyUserName)
                .WithMessage($"A user with the same Login already exists in the system. Choose another login."); ;

            RuleFor(i => i.Password)
                .NotEmpty()
                .WithMessage($"Enter password")
                .Length(3, 20)
                .WithMessage($"Password must have from 3 to 20 characters.");
        }

        private bool VerifyUserName(string userName)
        {
            return !_repo.ExistsLogin(userName);
        }
    }
}
