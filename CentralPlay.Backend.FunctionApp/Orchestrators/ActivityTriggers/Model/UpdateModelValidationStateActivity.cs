using CentralPlay.Backend.FunctionApp.Models;
using CentralPlay.Backend.Service.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CentralPlay.Backend.FunctionApp.Orchestrators.ActivityTriggers.Model
{
    public class UpdateModelValidationStateActivity
    {
        #region Fields

        private readonly IModelService _modelService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="modelService"></param>
        public UpdateModelValidationStateActivity(IModelService modelService)
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
        [FunctionName(nameof(UpdateModelValidationStateActivity))]
        public async Task Run([ActivityTrigger] FileValidationModel fileValidation,
            ILogger log)
        {
            try
            {
                await _modelService.UpdateValidState(fileValidation.ModelId,
                 fileValidation.IsFileValid ?
                     Repository.Enums.ModelValidationEnum.Valid :
                     Repository.Enums.ModelValidationEnum.NotValid);
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Exception from '{ nameof(UpdateModelValidationStateActivity) }'");

                //throw Exception based on the error to handle it in the orchestrator.
                throw ex;
            }
        }
    }
}
