using System;
namespace com.petronas.myevents.api.ViewModels
{
    public class EventSubAgendaResponse
    {
        public string SubAgendaId { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string Venue { get; set; }
        public string AgendaName { get; set; }
    }
}
