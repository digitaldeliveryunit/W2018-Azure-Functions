using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace com.petronas.myevents.api.Models
{
    public class Session
    {
        [Key]
        public string Id { get; set; }
        public Event Event { get; set; }
        public string AgendaName { get; set; }
        public Venue Venue { get; set; }
        public bool IsDeleted { get; set; }
        public int? Day { get; set; }
        public List<SubSession> SubSessions { get; set; }
        public List<EventMember> Members { get; set; }
        public string Discriminator { get; set; }
    }
}
