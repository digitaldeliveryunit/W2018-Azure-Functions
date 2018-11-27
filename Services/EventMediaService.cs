using System;
using System.Collections.Generic;
using System.Linq;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.Repositories.Interfaces;
using com.petronas.myevents.api.Services.Interfaces;
using Microsoft.Azure.Documents.Client;

namespace com.petronas.myevents.api.Services
{
    public class EventMediaService : IEventMediaService
    {
        private readonly IEventRepository _eventRepository;
        public EventMediaService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IEnumerable<Media> GetMedias(string eventId, string mediaType, int skip, int take)
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = 1,
                EnableCrossPartitionQuery = true
            };
            return _eventRepository.GetAll(x => !x.IsDeleted && x.Id == eventId, feedOptions).ToList().FirstOrDefault().Medias.Where(x=>!x.IsDeleted && x.MediaType == mediaType).Skip(skip).Take(take).ToList();
        }
    }
}
