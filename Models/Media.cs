using System;

namespace com.petronas.myevents.api.Models
{
    public class Media : ModelBase
    {
        public string EventId { get; set; }
        public string MediaType { get; set; }
        public string Url { get; set; }
        public string ThumbUrl { get; set; }
        public string FileName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}