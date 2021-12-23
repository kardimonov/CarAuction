using CarAuction.Data.Context;
using CarAuction.Logic.Commands;
using FluentValidation;
using System.Linq;

namespace CarAuction.Web.Validators
{
    public class AddBidValidator : AbstractValidator<AddBidCommand>
    {
        private readonly ApplicationContext db;

        public AddBidValidator(ApplicationContext context)
        {
            db = context;

            RuleFor(i => i.AuctionCarId)
                .NotEmpty()
                .WithMessage($"AuctionCar id cannot be empty");

            RuleFor(i => i.Amount)
                .NotEmpty()
                .WithMessage($"Enter the amount of bid")
                .Must((source, amount) => VerifyBidAmount(source.AuctionCarId, amount))
                .WithMessage($"Bid amount must be equal or bigger than car price in the auction");
        }

        private bool VerifyBidAmount(int auctionCarId, int bidAmount)
        {
            var auctionPrice = db.AuctionCars.FirstOrDefault(a => a.Id == auctionCarId).AuctionPrice;
            return bidAmount >= auctionPrice;
        }
            
    }
}
