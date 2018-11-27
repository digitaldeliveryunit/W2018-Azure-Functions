using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<EventSpotlightResponse> GetSpotlights(string eventId, int skip, int take)
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = 1,
                EnableCrossPartitionQuery = true
            };
            var spotlightList = _eventRepository.GetAll(x => !x.IsDeleted && x.Id == eventId, feedOptions).ToList().FirstOrDefault().Spotlights.Where(x=>!x.IsDeleted).Skip(skip).Take(take).ToList();

            if (spotlightList.Any())
            {
                return spotlightList.Select(f => new EventSpotlightResponse
                {
                    SpotlightId = f.Id,
                    SpotlightName = f.SpotlightName,
                    SpotlightTitle = f.SpotlightTitle,
                    Description = f.SpotlightDescription,
                    ImageUrl = f.ImageUrl
                });
            }
            return Enumerable.Empty<EventSpotlightResponse>();
        }
    }
}
