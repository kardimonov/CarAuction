using Autofac;
using CarAuction.Data.Context;
using CarAuction.Logic.Profiles;
using CarAuction.Logic.Services.AuthInfrastructure;
using CarAuction.Web;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using System.Linq;
using System.Text;

namespace CarAuction
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins(Configuration.GetSection("CorsOrigins").Get<string[]>())
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin();
            }));

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
            });
            services.AddQuartzHostedService(q =>
            {
                q.WaitForJobsToComplete = true;
            });

            var scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
            services.AddSingleton(scheduler);

            var connection = Configuration.GetConnectionString("DefaultConnection");            
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

            services.AddAutoMapper(typeof(AuctionProfile));
            services.AddOptions();
            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.Configure<AuthServiceModel>(Configuration.GetSection("AuthService"));
            var result = Configuration.GetSection("AuthService").GetChildren();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarAuction", Version = "v1" });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = result.Where(x => x.Key == "Issuer").FirstOrDefault().Value,
                        ValidateAudience = false,
                        ValidAudience = result.Where(x => x.Key == "Audience").FirstOrDefault().Value,
                        ValidateLifetime = true,
                        IssuerSigningKey = GetSymmetricSecurityKey(result.Where(x => x.Key == "Key").FirstOrDefault().Value),
                        ValidateIssuerSigningKey = true
                    };
                });   
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarAuction v1"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { error = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("ApiCorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
