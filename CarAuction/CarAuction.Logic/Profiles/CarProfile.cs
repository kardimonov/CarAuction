using AutoMapper;
using CarAuction.Data.Models;
using CarAuction.Logic.Commands.Car;

namespace CarAuction.Logic.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<AddCarCommand, Car>()
                .ForMember(c => c.Grade, opt => opt.MapFrom((src, dest, _, context) => context.Options.Items["Grade"]));
            CreateMap<UpdateCarCommand, Car>()
                .ForMember(c => c.Grade, opt => opt.MapFrom((src, dest, _, context) => context.Options.Items["Grade"]));
        }
    }
}
