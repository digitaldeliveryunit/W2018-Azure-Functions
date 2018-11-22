using System.ComponentModel.DataAnnotations;

namespace com.petronas.myevents.api.Models
{
    public class Spotlight
    {
        [Key]
        public string Id { get; set; }
        public string EventId { get; set; }
        public string SpotlightTitle { get; set; }
        public string SpotlightDescription { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
        public string Discriminator { get; set; }

    }
}