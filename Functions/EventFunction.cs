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

namespace com.petronas.myevents.api.Functions
{
    public static class EventFunction
    {
        [FunctionName("EventListingFunction")]
        public static IActionResult Run(
            [HttpTrigger(
                AuthorizationLevel.Anonymous,
                RequestMethods.Get,
                Route = "event/listing/{*listType}")]HttpRequest request,
                string listType,
            ILogger log,
            [Inject]IEventService eventService)
        {
            try
            {
                switch (request.Method)
                {
                    case RequestMethods.Get:
                        var query = new RouteRequest(){
                            SearchKey = request.Query["searchKey"],
                            ContinuationKey = request.Query["continuationKey"],
                            Take = !string.IsNullOrEmpty(request.Query["Take"].ToString()) ? int.Parse(request.Query["Take"]) : DefaultValue.Take
                        };
                        switch(listType){
                            case QueryConstants.EVENT_FEATURED: 
                                return new OkObjectResult(eventService.GetFeaturedEvents(string.Empty, query.Take, query.SearchKey, DefaultValue.UserId));
                            case QueryConstants.EVENT_UPCOMING: 
                                return new OkObjectResult(eventService.GetUpcomingEvents(0, 10));
                            case QueryConstants.EVENT_UPCOMING_ALL: 
                                return new OkObjectResult(eventService.GetUpcomingAllEvents(0, 10));
                            case QueryConstants.EVENT_PAST: 
                                return new OkObjectResult(eventService.GetPastEvents(0, 10));
                            default: return new BadRequestResult();
                        }
                        //return new OkObjectResult(eventService.GetUpcomingAllEvents(0, 10));
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
