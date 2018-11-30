using System.Threading.Tasks;
using com.petronas.myevents.api.ViewModels;

namespace com.petronas.myevents.api.Services.Interfaces
{
    public interface IEventService
    {
        EventResponse GetById(string id, string userId);
        PagedResults<EventResponse> GetUpcomingAllEvents(string continuationKey, int take, string userId);
        PagedResults<EventResponse> GetUpcomingEvents(string continuationKey, int take, string userId);
        PagedResults<EventResponse> GetFeaturedEvents(string continuationKey, int take, string userId);
        PagedResults<EventResponse> GetPastEvents(string continuationKey, int take, string userId);
        Task<bool> UnBookmark(string eventId, string userId);
        Task<bool> Bookmark(string eventId, string userId);
        PagedResults<EventResponse> Search(string searchKey, string continuationKey, int take, string userId);
    }
}