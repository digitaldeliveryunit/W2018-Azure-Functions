using System;
using System.Threading.Tasks;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Services.Interfaces;
using com.petronas.myevents.api.ViewModels;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace com.petronas.myevents.api.Functions.Queues
{
    public static class EventQueueFunction
    {
        [FunctionName("EventQueueFunction")]
        public static async Task Run(
            [QueueTrigger(
                QueueNames.Events,
                Connection = AppSettings.StorageConnectionString)]string queueMessage,
            ILogger log,
            [Inject]IEventService eventService)
        {
            var message = JsonConvert.DeserializeObject<QueueMessage>(queueMessage);
            if (message != null)
            {
                switch (message.QueueType)
                {
                    case QueueType.BOOKMARK:
                        await EventServices.Bookmark(message.EventId, message.UserId);
                        break;
                    case QueueType.UNBOOKMARK:
                        await EventServices.UnBookmark(message.EventId, message.UserId);
                        break;
                    case QueueType.JOIN:
                        await EventServices.Join(message.EventId, message.UserId);
                        break;
                    case QueueType.UN_JOIN:
                        await EventServices.UnJoin(message.EventId, message.UserId);
                        break;
                }
            }
            else
            {
                log.LogError($"The message cannot be parsed");
            }

            switch (queueContract.Action)
            {
                case QueueActions.Create:
                    var addResult = await applicationService.AddApplication(applicationContract, queueContract.UserId);
                    log.LogInformation($"New application added: {JsonConvert.SerializeObject(addResult)}");
                    break;
                case QueueActions.Update:
                    var updateResult = await applicationService.UpdateApplication(queueContract.Id, applicationContract, queueContract.UserId);
                    log.LogInformation($"Application updated: {JsonConvert.SerializeObject(updateResult)}");
                    break;
                case QueueActions.Delete:
                    var deleteResult = await applicationService.DeleteApplication(queueContract.Id, queueContract.UserId, queueContract.Environment);
                    log.LogInformation($"Application deleted: {JsonConvert.SerializeObject(deleteResult)}");
                    break;
                default:
                    break;
            }

            log.LogInformation($"Application Queue trigger function processed: {queueMessage}");
        }


    }
}
