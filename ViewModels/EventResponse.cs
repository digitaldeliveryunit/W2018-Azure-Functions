using System;
using System.Collections.Generic;
using com.petronas.myevents.api.Models;

namespace com.petronas.myevents.api.ViewModels
{
    public class EventResponse
    {
        public string EventId { get; set; }
        public string ImageUrl { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Venue { get; set; }
        public Location EventLocation { get; set; }
        public string EventStatus { get; set; }
        public bool IsFeatured { get; set; }
        public string EventType { get; set; }
        public string SurveyUrl { get; set; }
        public string SurveyResultUrl { get; set; }
        public bool IsBookmark { get; set; }
        public string UserStatus { get; set; }
    }

    public class EventListViewModel
    {
        public List<EventResponse> Events { get; set; }
        public bool HasNextPage { get; set; }
        public string ContinuationKey { get; set; }
        public int Take { get; set; }
    }
}