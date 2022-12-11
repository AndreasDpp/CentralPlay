using Azure.Storage.Blobs;
using CentralPlay.Backend.Repository.Interfaces;
using CentralPlay.Backend.Repository.Repositories;
using CentralPlay.Backend.Service.Interfaces;
using CentralPlay.Backend.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storage.Net;
using System;

namespace CentralPlay.Backend.FunctionApp.Extensions
{
    public static class BlobStorageServiceCollectionExtension
    {
        /// <summary>
        /// Setup Azure Blob storage
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddBlobStorage(this IServiceCollection services, IConfiguration configuration)
        {
            StorageFactory.Modules.UseAzureBlobStorage();

            var storageConnectionString = configuration.GetValue<string>("Connections:ModelBlobStorage:ConnectionString");
            var storageContainerName = configuration.GetValue<string>("Connections:ModelBlobStorage:ContainerName");

            if (string.IsNullOrWhiteSpace(storageConnectionString))
            {
                throw new Exception("Connection String for the Blob storage account is null or empty");
            }

            if (string.IsNullOrWhiteSpace(storageContainerName))
            {
                throw new Exception("Container Name for the Blob storage account is null or empty");
            }

            services.AddTransient<IAzureBlobStorageRepository, AzureBlobStorageRepository>(t =>
            {
                var client = new BlobServiceClient(storageConnectionString);
                var container = client.GetBlobContainerClient(storageContainerName);

                return new AzureBlobStorageRepository(container);
            });

            services.AddTransient<IStorageService, AzureBlobStorageService>();
        }
    }
}
