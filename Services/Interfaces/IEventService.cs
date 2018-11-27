using System.Threading.Tasks;
using com.petronas.myevents.api.ViewModels;

namespace com.petronas.myevents.api.Services.Interfaces
{
    public interface IEventService
    {
        EventResponse GetById(string id, string userId);
        EventListViewModel GetUpcomingAllEvents(string continuationKey, int take, string userId);
        EventListViewModel GetUpcomingEvents(string continuationKey, int take, string userId);
        EventListViewModel GetFeaturedEvents(string continuationKey, int take, string userId);
        EventListViewModel GetPastEvents(string continuationKey, int take, string userId);
        Task<bool> UnBookmark(string eventId, string userId);
        Task<bool> Bookmark(string eventId, string userId);
        EventListViewModel Search(string searchKey, string continuationKey, int take, string userId);
    }
}