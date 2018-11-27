  using System;
  using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.RequestContracts;
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
    public class EventFunctions
    {
        [FunctionName("GetEventAgenda")]
        public static IActionResult GetEventAgenda(
              [HttpTrigger(
                  AuthorizationLevel.Anonymous,
                  RequestMethods.Get,
                Route = "Agendas/{*eventId}")]HttpRequest request,
                  string eventId,
              ILogger log,
            [Inject]IEventService eventService, [Inject]IEventAgendaService agendaService)
        {
            try
            {
                switch (request.Method)
                {
                    case RequestMethods.Get:
                        var _event = eventService.GetById(eventId, DefaultValue.UserId);
                        if (_event == null)
                        {
                            var errorResponse = new ErrorsResponse();
                            var errorMessage = new ErrorMessage()
                            {
                                Code = Convert.ToInt32(ErrorMessageCodes.EventIdNotExistErrorCode),
                                Message = ErrorMessageCodes.EventIdNotExistErrorMessage
                            };

                            log.LogInformation(JsonConvert.SerializeObject(errorMessage));

                            return new NotFoundObjectResult(errorMessage);
                        }

                        var agenda = agendaService.GetAgendas(eventId);
                        return new OkObjectResult(agenda);
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
                    Code = Convert.ToInt32(ErrorMessageCodes.GetEventAgendaError),
                    Message = ErrorMessageCodes.GetEventAgendaMessage
                };
                return new BadRequestObjectResult(error);
            }
        }

        [FunctionName("GetSpotlights")]
        public static IActionResult GetSpotlights(
              [HttpTrigger(
                  AuthorizationLevel.Anonymous,
                  RequestMethods.Get,
                Route = "Spotlight/{*eventId}")]HttpRequest request,
                  string eventId,
              ILogger log,
            [Inject]IEventService eventService, [Inject]IEventSpotlightService spotlightService)
        {
            try
            {
                var query = new RouteRequest()
                {
                    Skip = !string.IsNullOrEmpty(request.Query["Skip"].ToString()) ? int.Parse(request.Query["Skip"]) : DefaultValue.Skip,
                    Take = !string.IsNullOrEmpty(request.Query["Take"].ToString()) ? int.Parse(request.Query["Take"]) : DefaultValue.Take
                };
                switch (request.Method)
                {
                    case RequestMethods.Get:
                        var _event = eventService.GetById(eventId, DefaultValue.UserId);
                        if (_event == null)
                        {
                            var errorMessage = new ErrorMessage()
                            {
                                Code = Convert.ToInt32(ErrorMessageCodes.EventIdNotExistErrorCode),
                                Message = ErrorMessageCodes.EventIdNotExistErrorMessage
                            };

                            log.LogInformation(JsonConvert.SerializeObject(errorMessage));

                            return new NotFoundObjectResult(errorMessage);
                        }
                        var spotlight = spotlightService.GetSpotlights(eventId, query.Skip, query.Take);

                        if (spotlight == null)
                        {
                            var errorMessage = new ErrorMessage()
                            {
                                Code = Convert.ToInt32(ErrorMessageCodes.NoSpotlightErrorCode),
                                Message = ErrorMessageCodes.NoSpotlightErrorMessage
                            };

                            log.LogInformation(JsonConvert.SerializeObject(errorMessage));

                            return new NotFoundObjectResult(errorMessage);
                        }

                        return new OkObjectResult(spotlight);
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
                    Code = Convert.ToInt32(ErrorMessageCodes.GetEventSpotlightError),
                    Message = ErrorMessageCodes.GetEventSpotlightMessage
                };
                return new BadRequestObjectResult(error);
            }
        }

        [FunctionName("GetEventById")]
        public static IActionResult GetEventById(
              [HttpTrigger(
                  AuthorizationLevel.Anonymous,
                  RequestMethods.Get,
                Route = "Event/{*eventId}")]HttpRequest request,
                  string eventId,
              ILogger log,
            [Inject]IEventService eventService, [Inject]IEventAgendaService agendaService, [Inject]IEventSpotlightService spotlightService)
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
                error = new ErrorMessage()
                {
                    Code = Convert.ToInt32(ErrorMessageCodes.GetEventsError),
                    Message = ErrorMessageCodes.GetEventsMessage
                };
                return new BadRequestObjectResult(error);
            }
        }

        //[FunctionName("Bookmark")]
        //public static IActionResult AAA(
        //      [HttpTrigger(
        //          AuthorizationLevel.Anonymous,
        //        RequestMethods.Post,
        //        Route = "Event/{*eventId}/Bookmark")]HttpRequest request,
        //          string eventId,
        //          string functionType,
        //      ILogger log,
        //    [Inject]IEventService eventService, [Inject]IEventAgendaService agendaService, [Inject]IEventSpotlightService spotlightService)
        //{
        //    try
        //    {
        //        switch (request.Method)
        //        {
        //            case RequestMethods.Post:

        //            default:
        //                return new BadRequestResult();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.LogError(ex.Message, ex);
        //        return new BadRequestObjectResult(null);
        //    }
        //}
    }
  }
