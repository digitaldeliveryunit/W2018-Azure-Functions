using System.Collections.Generic;
using com.petronas.myevents.api.ViewModels;

namespace com.petronas.myevents.api.Services.Interfaces
{
    public interface IEventAgendaService
    {
        IEnumerable<EventAgendaResponse> GetAgendas(string eventId);
    }
}