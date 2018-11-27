namespace com.petronas.myevents.api.Models
{
    public class SubSession : ModelBase
    {
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string AgendaName { get; set; }

        public string SessionId { get; set; }
        public string VenueId { get; set; }

        public virtual Venue Venue { get; set; }
    }
}