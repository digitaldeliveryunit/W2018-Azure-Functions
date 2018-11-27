using System.Threading.Tasks;
using com.petronas.myevents.api.Models;

namespace com.petronas.myevents.api.Services.Interfaces
{
    public interface IEventMemberService
    {
        Task<bool> Join(string eventId, string userId);
        Task<bool> UnJoin(string eventId, string userId);
        EventMember CheckExistence(string eventId, string userId);
    }
}