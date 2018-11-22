using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace com.petronas.myevents.api.Services
{
    internal static class ServiceCollectionExtensions
    {
        private const string COSMOSDB_CONFIGURATION_SECTION = "CosmosDB";
        private const string AZURE_QUEUE_CONFIGURATION_SECTION = "AzureQueue";

        public static IServiceCollection AddCosmosDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CosmosDBOptions>(configuration.GetSection(COSMOSDB_CONFIGURATION_SECTION));
            services.Configure<AzureQueueOptions>(configuration.GetSection(AZURE_QUEUE_CONFIGURATION_SECTION));

            return services;
        }
    }
}
