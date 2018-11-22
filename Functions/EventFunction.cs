using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Services.Interfaces;

namespace Petronas.Services.Social.Functions
{
    public static class EventFunction
    {
        [FunctionName("EventFunction")]
        public static IActionResult Run(
            [HttpTrigger(
                AuthorizationLevel.Anonymous,
                RequestMethods.Get,
                RequestMethods.Post,
                RequestMethods.Put,
                RequestMethods.Delete,
                Route = "event/{*eventType}")]HttpRequest request,
            string eventType,
            ILogger log,
            [Inject]IEventService eventService)
        {
            try
            {
                switch (request.Method)
                {
                    case RequestMethods.Get:
                        return new OkObjectResult(eventService.GetUpcomingAllEvents(0, 10));
                    case RequestMethods.Post:
                        return new OkResult();
                    case RequestMethods.Put:
                        return new OkResult();
                    case RequestMethods.Delete:
                        return new OkResult();
                    default:
                        return new BadRequestResult();
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex);
                throw ex;
            }
        }
    }
}
