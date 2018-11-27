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
  using com.petronas.myevents.api.Helpers;

  namespace com.petronas.myevents.api.Functions
  {
    public class EventMediaFunction
    {
        [FunctionName("EventMediaFunction")]
        public static IActionResult Run(
            [HttpTrigger(
                AuthorizationLevel.Anonymous,
                RequestMethods.Get,
                Route = "Media/{id}/Type/{type}")]HttpRequest request,
                string id,
                string type,
            ILogger log,
            [Inject]IEventService eventService,[Inject]IEventMediaService eventMediaService)
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
                        var events = eventService.GetById(id, DefaultValue.UserId);
                        if (events == null)
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
                        var media = eventMediaService.GetMedias(id, "Video", query.Skip, query.Take);
                        return new OkObjectResult(media);
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
                    Code = Convert.ToInt32(ErrorMessageCodes.GetEventMediaError),
                    Message = ErrorMessageCodes.GetEventMediaMessage
                };
                return new BadRequestObjectResult(error);
            }
        }
    }
  }
