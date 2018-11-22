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
        EventResponse GetById(string id);
        IEnumerable<EventResponse> GetUpcomingAllEvents(int skip, int take);
        IEnumerable<EventResponse> GetUpcomingEvents(int skip, int take);
        IEnumerable<EventResponse> GetFeaturedEvents(int skip, int take);
        IEnumerable<EventResponse> GetPastEvents(int skip, int take);
        Task<bool> UnBookmark(string eventId, string userId);
        Task<bool> Bookmark(string eventId, string userId);
        IEnumerable<EventResponse> Search(string searchKey, int skip, int take);
    }
}
