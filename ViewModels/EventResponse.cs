using System;
using com.petronas.myevents.api.Models;

namespace com.petronas.myevents.api.ViewModels
{
    public class EventResponse
    {
        public string EventId { get; set; }
        public String ImageUrl { get; set; }
        public String EventName { get; set; }
        public String EventDescription { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Venue { get; set; }
        public Location EventLocation { get; set; }
        public string EventStatus { get; set; }
        public Boolean IsFeatured { get; set; }
        public String EventType { get; set; }
        public String SurveyUrl { get; set; }
        public String SurveyResultUrl { get; set; }
        public Boolean IsBookmark { get; set; }
        public string UserStatus { get; set; }
    }
}
