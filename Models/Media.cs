using System.ComponentModel.DataAnnotations;

namespace com.petronas.myevents.api.Models
{
    public class Media
    {
        [Key]
        public string Id { get; set; }
        public string EventId { get; set; }
        public string MediaType { get; set; }
        public string Url { get; set; }
        public string ThumbUrl { get; set; }
        public bool IsDeleted { get; set; }
        public string Discriminator { get; set; }
    }
}
