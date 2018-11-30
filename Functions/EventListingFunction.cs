using System;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Helpers;
using com.petronas.myevents.api.RequestContracts;
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
    public static class EventListingFunction
    {
        [FunctionName("EventListingFunction")]
        public static IActionResult Run(
            [HttpTrigger(
                AuthorizationLevel.Anonymous,
                RequestMethods.Get,
                Route = "Events/Listing/{listType}")]
            HttpRequest request,
            string listType,
            ILogger log,
            [Inject] IEventService eventService)
        {
            try
            {
                switch (request.Method)
                {
                    case RequestMethods.Get:
                        var query = new RouteRequest
                        {
                            SearchKey = request.Query["searchKey"],
                            ContinuationKey = !string.IsNullOrEmpty(request.Query["continuationKey"])
                                ? EncryptionService.Decrypt(request.Query["continuationKey"])
                                : string.Empty,
                            Take = !string.IsNullOrEmpty(request.Query["Take"].ToString())
                                ? int.Parse(request.Query["Take"])
                                : DefaultValue.Take
                        };
                        switch (listType)
                        {
                            case QueryConstants.EVENT_FEATURED:
                                return new OkObjectResult(eventService.GetFeaturedEvents(query.ContinuationKey,
                                    query.Take, DefaultValue.UserId));
                            case QueryConstants.EVENT_UPCOMING:
                                return new OkObjectResult(eventService.GetUpcomingEvents(query.ContinuationKey,
                                    query.Take, DefaultValue.UserId));
                            case QueryConstants.EVENT_UPCOMING_ALL:
                                return new OkObjectResult(eventService.GetUpcomingAllEvents(query.ContinuationKey,
                                    query.Take, DefaultValue.UserId));
                            case QueryConstants.EVENT_PAST:
                                return new OkObjectResult(eventService.GetPastEvents(query.ContinuationKey, query.Take,
                                    DefaultValue.UserId));
                            case QueryConstants.EVENT_SEARCH:
                                return new OkObjectResult(eventService.Search(query.SearchKey, query.ContinuationKey,
                                    query.Take, DefaultValue.UserId));
                            default: return new BadRequestResult();
                        }
                    default:
                        return new BadRequestResult();
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex);
                ErrorMessage error;
                switch (listType)
                {
                    case QueryConstants.EVENT_FEATURED:
                        error = new ErrorMessage
                        {
                            Code = Convert.ToInt32(ErrorMessageCodes.GetFeaturedEventsError),
                            Message = ErrorMessageCodes.GetFeaturedEventsMessage
                        };
                        break;
                    case QueryConstants.EVENT_UPCOMING:
                        error = new ErrorMessage
                        {
                            Code = Convert.ToInt32(ErrorMessageCodes.GetUpcomingEventsError),
                            Message = ErrorMessageCodes.GetUpcomingEventsMessage
                        };
                        break;
                    case QueryConstants.EVENT_UPCOMING_ALL:
                        error = new ErrorMessage
                        {
                            Code = Convert.ToInt32(ErrorMessageCodes.GetAllUpcomingEventsError),
                            Message = ErrorMessageCodes.GetAllUpcomingEventsMessage
                        };
                        break;
                    case QueryConstants.EVENT_PAST:
                        error = new ErrorMessage
                        {
                            Code = Convert.ToInt32(ErrorMessageCodes.GetPastEventsError),
                            Message = ErrorMessageCodes.GetPastEventsMessage
                        };
                        break;
                    case QueryConstants.EVENT_SEARCH:
                        error = new ErrorMessage
                        {
                            Code = Convert.ToInt32(ErrorMessageCodes.SearchEventError),
                            Message = ErrorMessageCodes.SearchEventMessage
                        };
                        break;
                    default:
                        error = new ErrorMessage
                        {
                            Code = Convert.ToInt32(ErrorMessageCodes.GetEventsError),
                            Message = ErrorMessageCodes.GetEventsMessage
                        };
                        break;
                }

                return new BadRequestObjectResult(error);
            }
        }
    }
}