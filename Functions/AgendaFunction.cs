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
    public class AgendaFunction
    {
        [FunctionName("GetEventAgenda")]
        public static IActionResult Run(
            [HttpTrigger(
                AuthorizationLevel.Anonymous,
                RequestMethods.Get,
                Route = "Agendas/{eventId}")]
            HttpRequest request,
            string eventId,
            ILogger log,
            [Inject] IEventService eventService, [Inject] IEventAgendaService agendaService)
        {
            try
            {
                switch (request.Method)
                {
                    case RequestMethods.Get:
                        var _event = eventService.GetById(eventId, DefaultValue.UserId);
                        if (_event == null)
                        {
                            var errorMessage = new ErrorMessage
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
                error = new ErrorMessage
                {
                    Code = Convert.ToInt32(ErrorMessageCodes.GetEventAgendaError),
                    Message = ErrorMessageCodes.GetEventAgendaMessage
                };
                return new BadRequestObjectResult(error);
            }
        }
    }
}