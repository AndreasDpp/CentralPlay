using CentralPlay.Backend.FunctionApp.Models;
using CentralPlay.Backend.FunctionApp.Orchestrators.ActivityTriggers.Storage;
using CentralPlay.Backend.FunctionApp.Orchestrators.ActivityTriggers.Model;
using CentralPlay.Backend.FunctionApp.Orchestrators.ActivityTriggers.User;
using CentralPlay.Backend.Service.DTO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CentralPlay.Backend.FunctionApp.Orchestrators
{
    public class ValidatingAddedModelToStorageOrchestrator
    {
        [FunctionName("ValidatingAddedModelToStorageOrchestrator")]
        public static async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var blob = context.GetInput<BlobFileTriggerModel>();

            // (0) Starts function for getting the model object.
            var model = await context.CallActivityAsync<ModelDTO>(nameof(GetModelActivity), blob.Name);

            if (model != null)
            {
                // (1) Starts function for validating the file.
                var isModeFileValid = await context.CallActivityAsync<bool>(nameof(ValidationOfModelFileActivity), blob.FilePath);

                var parallelTasks = new List<Task>();

                // (2) Starts function for updating the value in the Model entity.
                parallelTasks.Add(context.CallActivityAsync(nameof(UpdateModelValidationStateActivity),
                    new FileValidationModel { ModelId = blob.Name, IsFileValid = isModeFileValid }));

                if (isModeFileValid)
                {
                    // (3.A) Starts function for updating the amount of models in the user entity. - If the file is valid
                    parallelTasks.Add(context.CallActivityAsync(nameof(ModelAddedToUserActivity),
                        new ModelAddedToUserModel { UserId = model.UserId, AmountAdded = 1 }));
                }
                else
                {
                    // (3.B) Starts function for deleting the file. - If the file is invalid
                    parallelTasks.Add(context.CallActivityAsync(nameof(DeleteModelAcitivity), blob.Name));
                }

                // Waits on the started tasks to complete.
                await Task.WhenAll(parallelTasks);
            }
            else
            {
                // TODO : Add logic for when model doesn't exists in the database container based on the blob name
            }
        }

        [FunctionName("ValidatingAddedModelToStorageTriggerOrchestrator")]
        public static async Task StartOrchestratorBlobTrigger(
        [BlobTrigger("models/{name}", Connection = "Connections:ModelBlobStorage:ConnectionString")] Stream myBlob,
            string name,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            string blobStorageBasePath = Environment.GetEnvironmentVariable("ModelsEndpointUrl");

            var model = new BlobFileTriggerModel
            {
                Name = name.Split(".")[0],
                FileLength = myBlob.Length,
                FilePath = blobStorageBasePath + (blobStorageBasePath.EndsWith("/") ? "" : "/") + name
            };

            string instanceId = await starter.StartNewAsync("ValidatingAddedModelToStorageOrchestrator", model);
            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");
        }
    }
}