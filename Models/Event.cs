using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace com.petronas.myevents.api.Models
{
    public class Event
    {
        [Key]
        public string Id { get; set; }
        [JsonProperty("eventName")]
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set; }
        public string EventType { get; set; }
        public Venue Venue { get; set; }
        public bool IsFeatured { get; set; }
        public string EventImageUrl { get; set; }
        public string EventStatus { get; set; }
        public string SurveyUrl { get; set; }
        public string SurveyResultUrl { get; set; }
        public Location Location { get; set; }
        public bool IsDeleted { get; set; }
        public string Discriminator { get; set; }
        public List<Session> Sessions { get; set; }
        public List<Bookmark> Bookmarks { get; set; }
        public List<EventMember> Members { get; set; }
    }
}
