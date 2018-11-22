using System.ComponentModel.DataAnnotations;

namespace com.petronas.myevents.api.Models
{
    public class SubSession
    {
        [Key]
        public string Id { get; set; }
        public string SessionId { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string AgendaName { get; set; }
        public Venue Venue { get; set; }
        public bool IsDeleted { get; set; }
        public string Discriminator { get; set; }
    }
}
