namespace com.petronas.myevents.api.Models
{
    public class User : ModelBase
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }
        public string Username { get; set; }
        public string ThumbnailPhoto { get; set; }
    }
}
