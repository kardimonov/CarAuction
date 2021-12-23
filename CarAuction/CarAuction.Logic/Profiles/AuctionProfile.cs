using AutoMapper;
using CarAuction.Data.Enums;
using CarAuction.Data.Models;
using CarAuction.Logic.Commands.Auction;
using CarAuction.Logic.Models;
using System;

namespace CarAuction.Logic.Profiles
{
    public class AuctionProfile : Profile
    {
        public AuctionProfile()
        {
            CreateMap<AddAuctionCommand, Auction>()
                .ForMember(a => a.Status, opt => opt.MapFrom(aac => AuctionStatus.Planned))
                .ForMember(a => a.StartTime, opt => opt.MapFrom(aac => DateTime.SpecifyKind(aac.StartTime, DateTimeKind.Utc)))
                .ForMember(a => a.EndTime, opt => opt.MapFrom(aac => DateTime.SpecifyKind(aac.EndTime, DateTimeKind.Utc)));  

            CreateMap<UpdateAuctionCommand, Auction>()
                .ForMember(a => a.StartTime, opt => opt.MapFrom(uac => DateTime.SpecifyKind(uac.StartTime, DateTimeKind.Utc)))
                .ForMember(a => a.EndTime, opt => opt.MapFrom(uac => DateTime.SpecifyKind(uac.EndTime, DateTimeKind.Utc)));

            CreateMap<Auction, AuctionModel>();
        }
    }
}
