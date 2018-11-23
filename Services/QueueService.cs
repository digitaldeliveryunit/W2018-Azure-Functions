using System;
using com.petronas.myevents.api.Configurations;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace com.petronas.myevents.api.Helpers
{
    public class QueueService
    {
        private readonly CloudQueue _queue;

        public QueueService(string queueName)
        {
            var _storageAccount = CloudStorageAccount.Parse(
                Environment.GetEnvironmentVariable(AppSettings.StorageConnectionString));
            var _queueClient = _storageAccount.CreateCloudQueueClient();
            _queue = _queueClient.GetQueueReference(queueName);
        }

        public void Insert(QueueMessage queueMessage){
            CloudQueueMessage message = new CloudQueueMessage(JsonConvert.SerializeObject(queueMessage));
            _queue.AddMessageAsync(message);
        }

    }
}
