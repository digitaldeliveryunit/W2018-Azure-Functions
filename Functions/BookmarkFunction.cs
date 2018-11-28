using System;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Helpers;
using com.petronas.myevents.api.Services.Interfaces;
using com.petronas.myevents.api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace com.petronas.myevents.api.Functions
{
    public class BookmarkFunction
    {
        [FunctionName("Bookmark")]
        public static IActionResult Run(
            [HttpTrigger(
                AuthorizationLevel.Anonymous,
                RequestMethods.Post,
                Route = "Bookmark/{*eventId}")]
            HttpRequest request,
            string eventId,
            ILogger log,
            [Inject] IEventService eventService, [Inject] QueueService queueService)
        {
            try
            {
                switch (request.Method)
                {
                    case RequestMethods.Post:
                        var message = new QueueMessage
                        {
                            QueueType = QueueType.BOOKMARK.ToString(),
                            EventId = eventId,
                            UserId = DefaultValue.UserId
                        };
                        queueService.Insert(message);
                        return new OkObjectResult(true);
                    default:
                        return new BadRequestObjectResult(false);
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex);
                return new BadRequestObjectResult(false);
            }
        }
    }
}