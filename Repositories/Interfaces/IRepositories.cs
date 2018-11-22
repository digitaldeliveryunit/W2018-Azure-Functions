using System;
using com.petronas.myevents.api.Models;

namespace com.petronas.myevents.api.Repositories.Interfaces
{
    public interface IEventRepository : IBaseRepository<Event> { }
    public interface IBookmarkRepository : IBaseRepository<Bookmark> { }
    public interface IEventMemberRepository : IBaseRepository<EventMember> { }
    public interface ISessionRepository : IBaseRepository<Session> { }
    public interface ISubSessionRepository : IBaseRepository<SubSession> { }
    public interface IVenueRepository : IBaseRepository<Venue> { }
    public interface ILocationRepository : IBaseRepository<Location> { }
    public interface IUserRepository : IBaseRepository<User> { }
    public interface ISpotlightRepository : IBaseRepository<Spotlight> { }
    public interface IMediaRepository : IBaseRepository<Media> { }
}
