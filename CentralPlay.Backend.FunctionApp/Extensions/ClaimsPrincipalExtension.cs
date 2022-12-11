using System;
using System.Linq;
using System.Security.Claims;

namespace CentralPlay.Backend.FunctionApp.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        /// <summary>
        /// A way to get an user id in the local env., because authentication through Azure AD is only supported on azure.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            string userId = null;
            bool isLocal = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"));

            if (isLocal)
            {
                var testUserId = Environment.GetEnvironmentVariable("TestUserId");
                  
                userId = testUserId;
            }
            else
            {
                userId = principal.Identity.IsAuthenticated ?
                    principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value
                    : null;
            }

            return userId;
        }
    }
}
