using com.petronas.myevents.api.Models;

namespace com.petronas.myevents.api.Repositories.Interfaces
{
    public interface IEventRepository : IBaseRepository<Event>
    {
    }

    public interface ISessionRepository : IBaseRepository<Session>
    {
    }

    public interface ISubSessionRepository : IBaseRepository<SubSession>
    {
    }

    public interface IVenueRepository : IBaseRepository<Venue>
    {
    }

    public interface ILocationRepository : IBaseRepository<Location>
    {
    }

    public interface IUserRepository : IBaseRepository<User>
    {
    }
}