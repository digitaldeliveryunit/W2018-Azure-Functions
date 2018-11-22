using System;
using System.Collections.Generic;
using System.Linq;
using com.petronas.myevents.api.Repositories.Interfaces;
using com.petronas.myevents.api.Services.Interfaces;
using com.petronas.myevents.api.ViewModels;

namespace com.petronas.myevents.api.Services
{
    public class EventSpotlightService : IEventSpotlightService
    {
        private readonly ISpotlightRepository _spotlightRepository;

        public EventSpotlightService(ISpotlightRepository spotlightRepository)
        {
            _spotlightRepository = spotlightRepository;
        }
        public IEnumerable<EventSpotlightResponse> GetSpotlights(string eventId, int skip, int take)
        {
            var spotlightList = _spotlightRepository.GetAll(x => !x.IsDeleted && x.EventId == eventId, null).Skip(skip).Take(take);

            if (spotlightList.Any())
            {
                return spotlightList.Select(f => new EventSpotlightResponse
                {
                    SpotlightId = f.Id,
                    SpotlightName = f.SpotlightTitle,
                    Description = f.SpotlightDescription,
                    ImageUrl = f.ImageUrl
                });
            }
            return Enumerable.Empty<EventSpotlightResponse>();
        }
    }
}
