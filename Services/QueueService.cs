using System;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.ViewModels;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
namespace com.petronas.myevents.api.Helpers
{
    public class QueueService
    {
        private readonly CloudQueue _queue;

        public QueueService()
        {
            var _storageAccount = CloudStorageAccount.Parse(
                Environment.GetEnvironmentVariable(AppSettings.StorageConnectionString));
            var _queueClient = _storageAccount.CreateCloudQueueClient();
            _queue = _queueClient.GetQueueReference(QueueNames.MyEventsQueue);
            _queue.CreateIfNotExistsAsync();
        }

        public void Insert(QueueMessage queueMessage)
        {
            var message = new CloudQueueMessage(JsonConvert.SerializeObject(queueMessage));
            _queue.AddMessageAsync(message);
        }
    }
}