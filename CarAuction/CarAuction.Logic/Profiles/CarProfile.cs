using AutoMapper;
using CarAuction.Data.Models;
using CarAuction.Logic.Commands.Car;

namespace CarAuction.Logic.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<AddCarCommand, Car>();
            CreateMap<UpdateCarCommand, Car>();
        }
    }
}
