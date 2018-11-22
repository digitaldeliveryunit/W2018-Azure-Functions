using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.ViewModels;

namespace com.petronas.myevents.api.Services.Interfaces
{
    public interface IEventService
    {
        IEnumerable<EventResponse> GetUpcomingAllEvents(int skip, int take);
    }
}
