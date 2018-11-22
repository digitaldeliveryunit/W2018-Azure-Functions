using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.Services.Interfaces;
using com.petronas.myevents.api.ViewModels;
using com.petronas.myevents.api.Repositories.Interfaces;

namespace com.petronas.myevents.api.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IEnumerable<EventResponse> GetUpcomingAllEvents(int skip, int take)
        {
            var ev = GetAll().Where(x => !x.IsDeleted && x.EventDateTo > DateTime.Now).ToList();
            foreach (var e in ev)
            {
                yield return GetEventResponse(e);
            }
        }

        // public Event AddNewEvent()
        // {

        // }

        private IQueryable<Event> GetAll()
        {
            // return _eventRepository.GetAll().Include(x => x.Venue).Include(x => x.Location).Include(x=>x.Sessions).Include(x=>x.Members).Include(x=>x.Bookmarks);
            return _eventRepository.GetAll();
        }

        private EventResponse GetEventResponse(Event _event)
        {
            var userId = "Mr. Hung"; // Just testing
            return new EventResponse()
            {
                EventId = _event.Id,
                ImageUrl = _event.EventImageUrl,
                EventDescription = _event.EventDescription,
                EventStatus = _event.EventStatus,
                EventName = _event.EventName,
                EventLocation = _event.Location,
                EventType = _event.EventType,
                DateTo = _event.EventDateTo,
                DateFrom = _event.EventDateFrom,
                Venue = _event.Venue.VenueName,
                IsFeatured = _event.IsFeatured,
                SurveyUrl = _event.SurveyUrl,
                SurveyResultUrl = _event.SurveyResultUrl//,
                // IsBookmark = _event.Bookmarks.Any(x => !x.IsDeleted && x.User.Id == userId),
                // UserStatus = _event.Members.Any(x => !x.IsDeleted && x.User.Id == userId) ?
                //                    _event.Members.FirstOrDefault(x => !x.IsDeleted && x.User.Id == userId).EventMemberStatus
                //                    : UserStatus.NEW.ToString()
            };
        }


    }
}
