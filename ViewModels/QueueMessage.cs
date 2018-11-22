using System;
namespace com.petronas.myevents.api.Viewmodels
{
    public class QueueMessage
    {
        public string QueueType { get; set; }
        public string EventId { get; set; }
        public string SessionId { get; set; }
        public string UserId { get; set; }
    }
}
