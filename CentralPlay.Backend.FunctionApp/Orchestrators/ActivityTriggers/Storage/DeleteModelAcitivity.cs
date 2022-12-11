using CentralPlay.Backend.Service.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralPlay.Backend.FunctionApp.Orchestrators.ActivityTriggers.Storage
{
    public class DeleteModelAcitivity
    {
        #region Fields

        private readonly IStorageService _storageService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="storageService"></param>
        public DeleteModelAcitivity(IStorageService storageService)
        {
            _storageService = storageService;
        }

        #endregion


        [FunctionName(nameof(DeleteModelAcitivity))]
        public async Task Run([ActivityTrigger] string fileName,
            ILogger log)
        {
            try
            {
                await _storageService.DeleteAsync(fileName);
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Exception from '{ nameof(DeleteModelAcitivity) }'");
                throw new Exception("Failed to delete the file in the storage.");
            }
        }
    }
}
