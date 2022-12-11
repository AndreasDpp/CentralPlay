using CentralPlay.Backend.Service.DTO;
using CentralPlay.Backend.Service.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CentralPlay.Backend.FunctionApp.Orchestrators.ActivityTriggers.Model
{
    public class GetModelActivity
    {
        #region Fields

        private readonly IModelService _modelService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="modelService"></param>
        public GetModelActivity(IModelService modelService)
        {
            _modelService = modelService;
        }

        #endregion

        /// <summary>
        /// Updates the model in the database container.
        /// </summary>
        /// <param name="modelId">The id of the model</param>
        /// <param name="log">Logger</param>
        /// <returns>Nothing if everything goes well. An exception if anything went wrong</returns>
        [FunctionName(nameof(GetModelActivity))]
        public async Task<ModelDTO> Run([ActivityTrigger] string modelId,
            ILogger log)
        {
            try
            {
                var model = await _modelService.GetByIdAsync(modelId);
                return model;
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Exception from '{ nameof(GetModelActivity) }'");

                //throw Exception based on the error to handle it in the orchestrator.
                throw ex;
            }
        }
    }
}