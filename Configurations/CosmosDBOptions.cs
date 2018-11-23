using System;

namespace com.petronas.myevents.api.Configurations
{
    public class CosmosDBOptions
    {
        #region Properties
        public string EndpointUri { get; set; }

        public string PrivateKey { get; set; }

        public string DatabaseId { get; set; }
        #endregion
    }

    public class AzureQueueOptions
    {
        #region Properties
        public string StorageConnectionString { get; set; }
        public string QueueName { get; set; }

        #endregion
    }
}
