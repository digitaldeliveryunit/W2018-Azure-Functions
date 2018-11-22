using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.Repositories.Interfaces;
using com.petronas.myevents.api.Services.Interfaces;
using com.petronas.myevents.api.ViewModels;

namespace com.petronas.myevents.api.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserService _userService;
        private readonly IBookmarkRepository _bookmarkRepository;
        public EventService(IEventRepository eventRepository, IUserService userService, IBookmarkRepository bookmarkRepository)
        {
            _eventRepository = eventRepository;
            _userService = userService;
            _bookmarkRepository = bookmarkRepository;
        }

        public async Task<bool> Bookmark(string eventId, string userId)
        {
            var isBookmark = _bookmarkRepository.GetAll().Any(x => !x.IsDeleted && x.EventId == eventId && x.UserId == userId);
            if (!isBookmark)
            {
                var bookmark = new Bookmark()
                {
                    EventId = eventId,
                    UserId = userId,
                    Id = Guid.NewGuid().ToString(),
                    Discriminator = CollectionNameConstant.MODEL_BOOKMARK
                };
                var ev = _eventRepository.GetAll().FirstOrDefault(x => !x.IsDeleted && x.Id == eventId);
                ev.Bookmarks.Add(bookmark);
                await _bookmarkRepository.Add(bookmark);
                await _eventRepository.Update(ev);
            }
            return true;
        }

        public async Task<bool> UnBookmark(string eventId, string userId)
        {
            var bookmarks = _bookmarkRepository.GetAll().Where(x => !x.IsDeleted && x.EventId == eventId && x.UserId == userId).ToList();
            var ev = _eventRepository.GetAll().FirstOrDefault(x => !x.IsDeleted && x.Id == eventId);
            foreach (var item in bookmarks)
            {
                item.IsDeleted = true;
                ev.Bookmarks.Remove(ev.Bookmarks.FirstOrDefault(x => x.Id == item.Id));
                await _bookmarkRepository.Update(item);
            }
            await _eventRepository.Update(ev);
            return true;
        }

        public EventResponse GetById(string id)
        {
            var currentUser = _userService.GetCurrentUser();
            var ev = _eventRepository.GetAll().Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();
            return ev == null ? null : GetEventResponse(ev);
        }

        public IEnumerable<EventResponse> GetFeaturedEvents(int skip, int take)
        {
            var ev = _eventRepository.GetAll().Where(x => !x.IsDeleted && x.IsFeatured).Skip(skip).Take(take).OrderBy(x => x.EventDateTo).ToList();
            foreach (var e in ev)
            {
                yield return GetEventResponse(e);
            }
        }

        public IEnumerable<EventResponse> GetPastEvents(int skip, int take)
        {
            var user = _userService.GetCurrentUser();
            var ev = _eventRepository.GetAll().Where(x => !x.IsDeleted && x.EventDateTo < DateTime.UtcNow && x.Members.Any(m => m.UserId == user.Id && !m.IsDeleted)).Skip(skip).Take(take).OrderBy(x => x.EventDateTo).ToList();
            foreach (var e in ev)
            {
                yield return GetEventResponse(e);
            }
        }

        public IEnumerable<EventResponse> GetUpcomingAllEvents(int skip, int take)
        {
            var ev = _eventRepository.GetAll().Where(x => !x.IsDeleted && x.EventDateTo > DateTime.UtcNow).Skip(skip).Take(take).OrderBy(x => x.EventDateTo).ToList();
            foreach (var e in ev)
            {
                yield return GetEventResponse(e);
            }
        }

        public IEnumerable<EventResponse> GetUpcomingEvents(int skip, int take)
        {
            var user = _userService.GetCurrentUser();
            var ev = _eventRepository.GetAll().Where(x => !x.IsDeleted && x.EventDateTo > DateTime.UtcNow
                                    && (x.Members.Any(m => !m.IsDeleted && m.UserId == user.Id)
                                        || x.Bookmarks.Any(b => !b.IsDeleted && b.UserId == user.Id)))
                             .Skip(skip).Take(take).OrderBy(x => x.EventDateTo).ToList();
            foreach (var e in ev)
            {
                yield return GetEventResponse(e);
            }
        }


        private EventResponse GetEventResponse(Event _event)
        {
            var user = _userService.GetCurrentUser();
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
                SurveyResultUrl = _event.SurveyResultUrl,
                IsBookmark = _event.Bookmarks.Any(x => !x.IsDeleted && x.UserId == user.Id),
                UserStatus = _event.Members.Any(x => !x.IsDeleted && x.UserId == user.Id) ?
                                   _event.Members.FirstOrDefault(x => !x.IsDeleted && x.UserId == user.Id).EventMemberStatus
                                   : UserStatus.NEW.ToString()
            };
        }

        public IEnumerable<EventResponse> Search(string searchKey, int skip, int take)
        {
            var user = _userService.GetCurrentUser();
            var ev = _eventRepository.GetAll().Where(x => !x.IsDeleted && x.EventName.ToLower().Contains(searchKey.ToLower()))
                             .Skip(skip).Take(take).OrderBy(x => x.EventDateTo).ToList();
            foreach (var e in ev)
            {
                yield return GetEventResponse(e);
            }
        }
    }
}
