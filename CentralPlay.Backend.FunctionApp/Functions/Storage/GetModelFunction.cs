using CentralPlay.Backend.FunctionApp.Extensions;
using CentralPlay.Backend.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CentralPlay.Backend.FunctionApp.Functions.Storage
{
    public class GetModelFunction
    {
        #region Fields

        private readonly IModelService _modelService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="modelService"></param>
        public GetModelFunction(IModelService modelService)
        {
            _modelService = modelService;
        }

        #endregion


        [FunctionName(nameof(GetModelFunction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/storage/get")] HttpRequest req,
            ClaimsPrincipal principal,
            ILogger log)
        {
            try
            {
                var userId = principal.GetUserId();
                string fileName = req.Query["fileName"];

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return new BadRequestObjectResult("Missing file name");
                }

                var fileResult = await _modelService.GetModelFileAsync(fileName, userId);

                if (!fileResult.Error)
                {
                    return new FileStreamResult(fileResult.Blob, "application/octet-stream");
                }
                else
                {
                    if(fileResult.Status == "Forbidden access")
                    {
                        return new ForbidResult();
                    } 

                    return new BadRequestObjectResult(fileResult.Status);
                }

                return new ForbidResult();
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Exception from '{ nameof(GetModelFunction) }'");
                return new BadRequestObjectResult("Invalid request");
            }
        }
    }
}
