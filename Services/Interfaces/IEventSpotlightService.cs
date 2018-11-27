using System.Collections.Generic;
using com.petronas.myevents.api.ViewModels;

namespace com.petronas.myevents.api.Services.Interfaces
{
    public interface IEventSpotlightService
    {
        IEnumerable<EventSpotlightResponse> GetSpotlights(string eventId);
    }
}