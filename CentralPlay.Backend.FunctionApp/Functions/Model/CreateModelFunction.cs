using CentralPlay.Backend.FunctionApp.Extensions;
using CentralPlay.Backend.FunctionApp.Models;
using CentralPlay.Backend.Service.DTO;
using CentralPlay.Backend.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CentralPlay.Backend.FunctionApp.Functions.Model
{
    public class CreateModelFunction
    {
        #region Fields

        private readonly IModelService _modelService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="modelService"></param>
        public CreateModelFunction(IModelService modelService)
        {
            _modelService = modelService;
        }

        #endregion

        /// <summary>
        /// Updates the model in the database container.
        /// </summary>
        /// <param name="blob">The content of the file</param>
        /// <param name="log">Logger</param>
        /// <returns>Nothing if everything goes well. An exception if anything went wrong</returns>
        [FunctionName(nameof(CreateModelFunction))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/model/add")] HttpRequest req,
            ClaimsPrincipal principal,
            ILogger log)
        {
            try
            {
                var userId = principal.GetUserId();

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var createModel = JsonConvert.DeserializeObject<CreateModelModel>(requestBody);

                var model = new ModelDTO
                {
                    UserId = userId,
                    Name = createModel.Name,
                    FileType = createModel.FileType,
                    FileSize = createModel.FileSize
                };

                await _modelService.AddAsync(model);

                return new OkObjectResult(new { Id = model.Id });
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Exception from '{ nameof(CreateModelFunction) }'");
                return new BadRequestObjectResult("Failed to create model");
            }
        }
    }
}