using System.ComponentModel.DataAnnotations;

namespace com.petronas.myevents.api.Models
{
    public class Bookmark
    {
        [Key]
        public string Id { get; set; }
        public User User { get; set; }
        public string EventId { get; set; }
        public Event Event { get; set; }
        public bool IsDeleted { get; set; }
        public string Discriminator { get; set; }
    }
}
