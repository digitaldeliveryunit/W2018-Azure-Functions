using System;
using com.petronas.myevents.api.Configurations;
using com.petronas.myevents.api.Viewmodels;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace com.petronas.myevents.api.Helpers
{
    public class AzureQueueHelpers
    {
        private readonly CloudQueue _queue;

        public AzureQueueHelpers(IOptions<AzureQueueOptions> optionsAccessor)
        {
            var _options = optionsAccessor.Value;
            var _storageAccount = CloudStorageAccount.Parse(_options.StorageConnectionString);
            var _queueClient = _storageAccount.CreateCloudQueueClient();
            _queue = _queueClient.GetQueueReference(_options.QueueName);
        }

        public void Insert(QueueMessage queueMessage){
            CloudQueueMessage message = new CloudQueueMessage(JsonConvert.SerializeObject(queueMessage));
            _queue.AddMessageAsync(message);
        }

    }
}
