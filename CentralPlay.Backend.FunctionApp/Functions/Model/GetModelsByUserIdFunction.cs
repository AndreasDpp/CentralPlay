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

namespace CentralPlay.Backend.FunctionApp.Functions.Model
{
    public class GetModelsByUserIdFunction
    {
        #region Fields

        private readonly IModelService _modelService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="modelService"></param>
        public GetModelsByUserIdFunction(IModelService modelService)
        {
            _modelService = modelService;
        }

        #endregion

        /// <summary>
        /// Gets all model by userId
        /// </summary>
        /// <param name="req"></param>
        /// <param name="principal"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetModelsByUserIdFunction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/model/all")] HttpRequest req,
            ClaimsPrincipal principal,
            ILogger log)
        {
            try
            {
                var userId = principal.GetUserId();

                var models = await _modelService.GetByUserId(userId);

                return new OkObjectResult(models);
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Exception from '{ nameof(GetModelsByUserIdFunction) }'");
                return new BadRequestObjectResult("Was not able to retrieve any models");
            }
        }
    }
}
