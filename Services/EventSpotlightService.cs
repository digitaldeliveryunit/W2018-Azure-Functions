using System.Collections.Generic;
using System.Linq;
using com.petronas.myevents.api.Extensions;
using com.petronas.myevents.api.Repositories.Interfaces;
using com.petronas.myevents.api.Services.Interfaces;
using com.petronas.myevents.api.ViewModels;
using Microsoft.Azure.Documents.Client;

namespace com.petronas.myevents.api.Services
{
    public class EventSpotlightService : IEventSpotlightService
    {
        private readonly IEventRepository _eventRepository;

        public EventSpotlightService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IEnumerable<EventSpotlightResponse> GetSpotlights(string eventId)
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = 1,
                EnableCrossPartitionQuery = true
            };
            var spotlightList = _eventRepository.GetAll(x => !x.IsDeleted && x.Id == eventId, feedOptions).ToList()
                .FirstOrDefault()
                ?.Spotlights.Where(x => !x.IsDeleted).ToList();

            if (spotlightList.Any())
                return spotlightList.Map<List<EventSpotlightResponse>>();
            return Enumerable.Empty<EventSpotlightResponse>();
        }
    }
}