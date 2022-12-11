using CentralPlay.Backend.FunctionApp.Extensions;
using CentralPlay.Backend.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CentralPlay.Backend.FunctionApp.Functions
{
    public class RegisterUserFunction
    {
        #region Fields

        private readonly IUserService _userInformationService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userInformationService"></param>
        public RegisterUserFunction(IUserService userInformationService)
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
        [FunctionName(nameof(RegisterUserFunction))]
        public async Task<IActionResult> AddedModelTOUserFunctionMethod([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/user/register")] HttpRequest req,
            ClaimsPrincipal principal,
            ILogger log)
        {
            try
            {
                var userId = principal.GetUserId();

                await _userInformationService.AddAsync(new Service.DTO.UserDTO
                {
                    Id = userId,
                    AmountOfModels = 0
                });

                return new OkResult();
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Exception from '{ nameof(RegisterUserFunction) }'");

                //TODO : throw HTTP Error  instead of exception
                return new BadRequestObjectResult("Couldn't register the user.");
            }
        }
    }
}
