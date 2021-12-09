using AutoMapper;
using CarAuction.Data;
using CarAuction.Data.Models;
using CarAuction.Logic.Commands.Auction;

namespace CarAuction.Logic.Profiles
{
    public class AuctionProfile : Profile
    {
        public AuctionProfile()
        {
            CreateMap<AddAuctionCommand, Auction>()
                .ForMember(a => a.Status, opt => opt.MapFrom(aac => AuctionStatus.Planned));
            CreateMap<UpdateAuctionCommand, Auction>();
        }
    }
}
