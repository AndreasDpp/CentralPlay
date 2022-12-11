using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CentralPlay.Backend.Service.Interfaces;
using System;
using System.Security.Claims;

namespace CentralPlay.Backend.FunctionApp.Functions.Storage
{
    public class UploadModelFunction
    {
        #region Fields

        private readonly IStorageService _storageService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="storageService"></param>
        public UploadModelFunction(IStorageService storageService)
        {
            _storageService = storageService;
        }

        #endregion


        [FunctionName(nameof(UploadModelFunction))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/storage/model")] HttpRequest req,
            ClaimsPrincipal principal,
            ILogger log)
        {
            try
            {
                var file = req.Form.Files["File"];

                await _storageService.UploadAsync(file);

                return new OkObjectResult("file uploaded successfully");
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Exception from '{ nameof(UploadModelFunction) }'");
                return new BadRequestObjectResult("Failed to upload the file.");
            }
        }
    }
}
