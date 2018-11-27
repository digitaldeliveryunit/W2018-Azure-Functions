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
                "myeventsworkshopqueue",
                Connection = AppSettings.StorageConnectionString)]string queueMessage,
            ILogger log,
            [Inject]IEventService eventService, [Inject]IEventMemberService memberService)
        {
            var message = JsonConvert.DeserializeObject<QueueMessage>(queueMessage);
            var queueType = Enum.Parse(typeof(QueueType), message.QueueType);
            if (message != null)
            {
                switch (queueType)
                {
                    case QueueType.BOOKMARK:
                        await eventService.Bookmark(message.EventId, message.UserId);
                        break;
                    case QueueType.UNBOOKMARK:
                        await eventService.UnBookmark(message.EventId, message.UserId);
                        break;
                    case QueueType.JOIN:
                        await memberService.Join(message.EventId, message.UserId);
                        break;
                    case QueueType.UN_JOIN:
                        await memberService.UnJoin(message.EventId, message.UserId);
                        break;
                }
            }
            else
            {
                log.LogError($"The message cannot be parsed");
            }
        }
    }
}
