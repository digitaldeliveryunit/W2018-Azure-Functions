using System;
using System.Collections.Generic;

namespace com.petronas.myevents.api.ViewModels
{
    /// <summary>
    /// Viewmodel for GetAgendas API
    /// </summary>
    public class EventAgendaResponse
    {
        public string AgendaId { get; set; }
        public string AgendaName { get; set; }
        public string Venue { get; set; }
        public int? Day { get; set; }
        public List<EventSubAgendaResponse> SubAgendas { get; set; }
        public string UserStatus { get; set; }
    }
}
