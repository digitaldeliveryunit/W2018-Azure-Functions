using System;
using com.petronas.myevents.api.Viewmodels;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace com.petronas.myevents.api.Services.Helpers
{
    public class AzureQueueHelpers
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudQueueClient _queueClient;
        private readonly AzureQueueOptions _options;
        private readonly CloudQueue _queue;
        public AzureQueueHelpers(IOptions<AzureQueueOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
            _storageAccount = CloudStorageAccount.Parse(_options.StorageConnectionString);
             _queueClient = _storageAccount.CreateCloudQueueClient();
            _queue = _queueClient.GetQueueReference(_options.QueueName);
        }

        public void Insert(QueueMessage queueMessage){
            CloudQueueMessage message = new CloudQueueMessage(JsonConvert.SerializeObject(queueMessage));
            _queue.AddMessageAsync(message);
        }

    }
}
