using System.ComponentModel.DataAnnotations;

namespace com.petronas.myevents.api.Models
{
    public class Venue
    {
        [Key]
        public string Id { get; set; }
        public string VenueName { get; set; }
        public Location Location { get; set; }
        public bool IsDeleted { get; set; }
        public string Discriminator { get; set; }
    }
}
