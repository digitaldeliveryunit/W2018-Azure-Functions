using System;
using System.Collections.Generic;

namespace com.petronas.myevents.api.Models
{
    public class Event:ModelBase
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set; }
        public string EventType { get; set; }
        public bool IsFeatured { get; set; }
        public string EventImageUrl { get; set; }
        public string EventStatus { get; set; }
        public string SurveyUrl { get; set; }
        public string SurveyResultUrl { get; set; }

        public string VenueId { get; set; }
        public string LocationId { get; set; }

        public Venue Venue { get; set; }
        public Location Location { get; set; }
        public List<Bookmark> Bookmarks { get; set; }
        public List<EventMember> Members { get; set; }
    }
}
