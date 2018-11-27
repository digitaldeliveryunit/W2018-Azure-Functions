
using com.petronas.myevents.api;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;
using System;
using com.petronas.myevents.api.Repositories.Interfaces;
using com.petronas.myevents.api.Repositories;
using com.petronas.myevents.api.Services.Interfaces;
using com.petronas.myevents.api.Services;
using com.petronas.myevents.api.Migrations;
using com.petronas.myevents.api.Helpers;

[assembly: WebJobsStartup(typeof(Startup))]
namespace com.petronas.myevents.api
{
    internal class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddDependencyInjection<ServiceProviderBuilder>();
            
            
        }

        //public void Configure(IWebJobsBuilder builder) =>
        //builder.AddDependencyInjection<ServiceProviderBuilder>();
        internal class ServiceProviderBuilder : IServiceProviderBuilder
        {
            // private readonly ILoggerFactory _loggerFactory;

            // public ServiceProviderBuilder(ILoggerFactory loggerFactory) =>
            //     _loggerFactory = loggerFactory;

            public IServiceProvider Build()
            {
                //IConfigurationRoot config = new ConfigurationBuilder()
                    //// .SetBasePath(Environment.CurrentDirectory)
                    //.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    ////.AddEnvironmentVariables()
                    //.Build();


                var services = new ServiceCollection();

                services.AddScoped<IEventRepository, EventRepository>();
                services.AddScoped<ILocationRepository, LocationRepository>();
                services.AddScoped<ISessionRepository, SessionRepository>();
                services.AddScoped<ISubSessionRepository, SubSessionRepository>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IVenueRepository, VenueRepository>();

                services.AddScoped<IEventService, EventService>();
                services.AddScoped<IEventSpotlightService, EventSpotlightService>();
                services.AddScoped<IEventAgendaService, EventAgendaService>();
                services.AddScoped<IEventMediaService, EventMediaService>();
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IEventMemberService, EventMemberService>();
                services.AddScoped<QueueService,QueueService>();
                new InitDbAndData().InitDbAndDataSeending();
                return services.BuildServiceProvider(true);
                
            }
        }
    }
}