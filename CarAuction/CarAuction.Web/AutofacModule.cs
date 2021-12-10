using Autofac;
using CarAuction.Data.Interfaces;
using CarAuction.Logic.Commands.Auction;
using CarAuction.Logic.Interfaces;
using CarAuction.Logic.Services.AuthInfrastructure;
using MediatR.Extensions.Autofac.DependencyInjection;
using System.Linq;

namespace CarAuction.Web
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        { 
            builder.RegisterAssemblyTypes(typeof(IRepository).Assembly)
            .Where(t => typeof(IRepository).IsAssignableFrom(t))
            .AsImplementedInterfaces()
            .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(IService).Assembly)
            .Where(t => typeof(IService).IsAssignableFrom(t))
            .AsImplementedInterfaces()
            .InstancePerDependency();

            builder.RegisterType<AuthHandler>().As<IAuthHandler>();
            
            builder.RegisterMediatR(typeof(AddAuctionCommand).Assembly);
        }
    }
}
