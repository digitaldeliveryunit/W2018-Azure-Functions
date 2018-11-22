using System.ComponentModel.DataAnnotations;

namespace com.petronas.myevents.api.Models
{
    public class Location
    {
        [Key]
        public string Id { get; set; }
        public string LocationName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsDeleted { get; set; }
        public string Discriminator { get; set; }

    }
}
