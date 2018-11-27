using System.Collections.Generic;

namespace com.petronas.myevents.api.Models
{
    public class Session : ModelBase
    {
        public string AgendaName { get; set; }
        public int? Day { get; set; }

        public string EventId { get; set; }
        public string VenueId { get; set; }

        public virtual Venue Venue { get; set; }
        public virtual List<SubSession> SubSessions { get; set; }
        public virtual List<EventMember> Members { get; set; }
    }
}