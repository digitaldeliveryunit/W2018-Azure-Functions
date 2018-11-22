namespace com.petronas.myevents.api.Models
{
    public class EventMember : ModelBase
    {
        public string EventMemberStatus { get; set; }

        public string SessionId { get; set; }
        public string EventId { get; set; }
        public string UserId { get; set; }
    }
}
