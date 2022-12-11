using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CentralPlay.Backend.Service.Interfaces;
using System;
using System.Security.Claims;
using System.Linq;
using CentralPlay.Backend.FunctionApp.Extensions;

namespace CentralPlay.Backend.FunctionApp.Functions.User
{
    public class GetUserFunction
    {
        #region Fields

        private readonly IUserService _userInformationService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userInformationService"></param>
        public GetUserFunction(IUserService userInformationService)
        {
            _userInformationService = userInformationService;
        }

        #endregion


        [FunctionName(nameof(GetUserFunction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/user/get")] HttpRequest req,
            ClaimsPrincipal principal,
            ILogger log)
        {
            try
            {
                var userId = principal.GetUserId();

                var user = await _userInformationService.GetById(userId);

                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Exception from '{ nameof(GetUserFunction) }'");
                return new BadRequestObjectResult("User could not be found.");
            }
        }
    }
}
