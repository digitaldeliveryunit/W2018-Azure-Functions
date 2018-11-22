using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.ViewModels;

namespace com.petronas.myevents.api.Services.Interfaces
{
    public interface IEventAgendaService
    {
        IEnumerable<EventAgendaResponse> GetAgendas(string eventId);
    }
}
