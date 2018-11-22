using System.ComponentModel.DataAnnotations;

namespace com.petronas.myevents.api.Models
{
    public class EventMember
    {
        [Key]
        public string Id { get; set; }
        public string EventMemberStatus { get; set; }
        public Session Session { get; set; }
        public Event Event { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; }
        public string Discriminator { get; set; }
    }
}
