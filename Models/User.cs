using System.ComponentModel.DataAnnotations;

namespace com.petronas.myevents.api.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }
        public string Username { get; set; }
        public string ThumbnailPhoto { get; set; }
        public bool IsDeleted { get; set; }
        public string Discriminator { get; set; }
    }
}
