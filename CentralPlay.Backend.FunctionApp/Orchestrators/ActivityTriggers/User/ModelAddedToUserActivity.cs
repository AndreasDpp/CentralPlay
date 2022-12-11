using CentralPlay.Backend.FunctionApp.Models;
using CentralPlay.Backend.Service.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralPlay.Backend.FunctionApp.Orchestrators.ActivityTriggers.User
{
    public class ModelAddedToUserActivity
    {
        #region Fields

        private readonly IUserService _userInformationService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userInformationService"></param>
        public ModelAddedToUserActivity(IUserService userInformationService)
        {
            _userInformationService = userInformationService;
        }

        #endregion

        /// <summary>
        /// Updates the model in the database container.
        /// </summary>
        /// <param name="blob">The content of the file</param>
        /// <param name="log">Logger</param>
        /// <returns>Nothing if everything goes well. An exception if anything went wrong</returns>
        [FunctionName(nameof(ModelAddedToUserActivity))]
        public async Task Run([ActivityTrigger] ModelAddedToUserModel model,
            ILogger log)
        {
            try
            {
                await _userInformationService.UpdateCountAsync(model.UserId, model.AmountAdded);
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Exception from '{ nameof(ModelAddedToUserActivity) }'");

                //throw Exception based on the error to handle it in the orchestrator.
                throw ex;
            }
        }
    }
}
