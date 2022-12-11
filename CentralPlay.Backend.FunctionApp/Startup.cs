using CentralPlay.Backend.FunctionApp.AppSettings;
using CentralPlay.Backend.FunctionApp.Extensions;
using CentralPlay.Backend.Repository.Interfaces;
using CentralPlay.Backend.Repository.Repositories;
using CentralPlay.Backend.Service.Interfaces;
using CentralPlay.Backend.Service.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

[assembly: FunctionsStartup(typeof(CentralPlay.Backend.FunctionApp.Startup))]
namespace CentralPlay.Backend.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            ConfigureServices(builder.Services);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configurations
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Use a singleton Configuration throughout the application
            services.AddSingleton<IConfiguration>(configuration);

            #region AutoMapper

            // Configures dependency injection for AutoMapper using mapper profiles
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #endregion

            #region CosmosDB

            // Add CosmosDb. This verifies database and collections existence.
            CosmosDbSettings cosmosDbConfig = configuration.GetSection("Connections:CosmosDB").Get<CosmosDbSettings>();

            // Register CosmosDB client and data repositories
            services.AddCosmosDb(cosmosDbConfig.EndpointUrl,
                                 cosmosDbConfig.PrimaryKey,
                                 cosmosDbConfig.DatabaseName,
                                 cosmosDbConfig.Containers);

            #endregion

            // Register Blob Storage account
            services.AddBlobStorage(configuration);

            // Register ILogger
            services.AddLogging();

            //// Register repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();


            //// Register services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IModelService, ModelService>();
        }
    }
}
