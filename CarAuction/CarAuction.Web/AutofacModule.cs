using Autofac;
using CarAuction.Data.Interfaces;
using CarAuction.Logic.Commands.Auction;
using CarAuction.Logic.Handlers;
using CarAuction.Logic.Interfaces;
using CarAuction.Logic.Services.AuthInfrastructure;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using Quartz.Impl;
using System.Linq;
using System.Reflection;

namespace CarAuction.Web
{
    public class AutofacModule : Autofac.Module
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

            builder.RegisterAssemblyTypes(typeof(ICarGradeModel).Assembly)
                .Where(t => typeof(ICarGradeModel).IsAssignableFrom(t))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<AuthHandler>().As<IAuthHandler>();

            builder.RegisterMediatR(typeof(AddAuctionCommand).Assembly);

            builder.RegisterAssemblyTypes(typeof(ValidateAuctionBehavior).Assembly)
                .Where(t => t.GetTypeInfo().ImplementedInterfaces.Any(
                    i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();            

            var scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
            builder.RegisterInstance(scheduler).SingleInstance();
        }
    }
}
