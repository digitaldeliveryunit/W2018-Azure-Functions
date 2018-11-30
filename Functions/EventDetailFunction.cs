using System;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Services.Interfaces;
using com.petronas.myevents.api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace com.petronas.myevents.api.Functions
{
    public class EventDetailFunction
    {
        [FunctionName("GetEventById")]
        public static IActionResult Run(
            [HttpTrigger(
                AuthorizationLevel.Anonymous,
                RequestMethods.Get,
                Route = "Event/{eventId}")]
            HttpRequest request,
            string eventId,
            ILogger log,
            [Inject] IEventService eventService)
        {
            try
            {
                switch (request.Method)
                {
                    case RequestMethods.Get:
                        var _event = eventService.GetById(eventId, DefaultValue.UserId);
                        return new OkObjectResult(_event);
                    default:
                        return new BadRequestResult();
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex);
                ErrorMessage error;
                error = new ErrorMessage
                {
                    Code = Convert.ToInt32(ErrorMessageCodes.GetEventsError),
                    Message = ErrorMessageCodes.GetEventsMessage
                };
                return new BadRequestObjectResult(error);
            }
        }
    }
}