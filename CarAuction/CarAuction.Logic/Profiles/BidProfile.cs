using AutoMapper;
using CarAuction.Data.Models;
using CarAuction.Logic.Commands;
using System;

namespace CarAuction.Logic.Profiles
{
    public class BidProfile : Profile
    {
        public BidProfile()
        {
            CreateMap<AddBidCommand, Bid>()
                .ForMember(b => b.UserId, opt => opt.MapFrom(abc => int.Parse(abc.UserId)))
                .ForMember(b => b.Time, opt => opt.MapFrom(abc => DateTime.UtcNow));
        }
    }
}
