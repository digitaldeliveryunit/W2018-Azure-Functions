using System;
using com.petronas.myevents.api.Models;

namespace com.petronas.myevents.api.Services.Interfaces
{
    public interface IEventMemberService
    {
        bool Join(string eventId, string userId);
        bool UnJoin(string eventId, string userId);
        EventMember CheckExistence(string eventid, string userId);
    }
}
