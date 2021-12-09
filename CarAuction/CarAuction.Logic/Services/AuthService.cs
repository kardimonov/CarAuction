using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using CarAuction.Logic.Interfaces;
using CarAuction.Logic.Models;
using CarAuction.Logic.Services.AuthInfrastructure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace CarAuction.Logic.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IAuthHandler _authHandler;
        private readonly IOptions<AuthServiceModel> _options;

        public AuthService(
            IAuthRepository repository,
            IAuthHandler authHandler,
            IOptions<AuthServiceModel> options)
        {
            _repo = repository;
            _authHandler = authHandler;
            _options = options;
        }

        public async Task<LoginSuccessModel> LoginCustomer(UserModel model, string audience)
        {
            User user = await _repo.CheckPassword(model.UserName, model.Password);
            if (user == null)
            {
                return new LoginSuccessModel() { Success = false };
            }

            var identity = _authHandler.GetIdentity(model.UserName, user.Role, user.Id);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                audience: audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.AddMonths(1),
                signingCredentials: new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(_options), SecurityAlgorithms.HmacSha256));

            var endcodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new LoginSuccessModel
            {
                AccessToken = endcodedJwt,
                UserName = user.UserName,
                Role = user.Role,
                UserId = user.Id.ToString(),
                Success = true
            };
        }

        public async Task AddCustomer(UserModel model)
        {
            await _repo.AddCustomer(model.UserName, model.Password);
        }
    }
}
