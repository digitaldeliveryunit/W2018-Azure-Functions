using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Services.Interfaces;
using Microsoft.Extensions.Options;
using com.petronas.myevents.api.Configurations;
using com.petronas.myevents.api.RequestContracts;
using com.petronas.myevents.api.ViewModels;
using com.petronas.myevents.api.Helpers;

namespace com.petronas.myevents.api.Functions
{
    public static class UserFunction
    {
        [FunctionName("UserFunction")]
        public static IActionResult Run(
            [HttpTrigger(
                AuthorizationLevel.Anonymous,
                RequestMethods.Get,
                Route = "User")]HttpRequest request,
            ILogger log,
            [Inject]IUserService userService)
        {
            try
            {
                switch (request.Method)
                {
                    case RequestMethods.Get:
                         var user = userService.GetCurrentUser();
                        return new OkObjectResult(user);
                    default:
                        return new BadRequestResult();
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex);
                ErrorMessage error;
                error = new ErrorMessage()
                {
                    Code = Convert.ToInt32(ErrorMessageCodes.GetUserProfileError),
                    Message = ErrorMessageCodes.GetUserProfileMessage
                };
                return new BadRequestObjectResult(error);
            }
        }
    }
}
