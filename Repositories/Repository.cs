using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.Repositories.Interfaces;

namespace com.petronas.myevents.api.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository() : base(CollectionNameConstant.COLLECTION_EVENT) { }
    }
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository() : base(CollectionNameConstant.COLLECTION_LOCATION) { }
    }
    public class SessionRepository : BaseRepository<Session>, ISessionRepository
    {
        public SessionRepository() : base(CollectionNameConstant.COLLECTION_SESSION) { }
    }
    public class SubSessionRepository : BaseRepository<SubSession>, ISubSessionRepository
    {
        public SubSessionRepository() : base(CollectionNameConstant.COLLECTION_SUBSESSION) { }
    }
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository() : base(CollectionNameConstant.COLLECTION_USER) { }
    }
    public class VenueRepository : BaseRepository<Venue>, IVenueRepository
    {
        public VenueRepository() : base(CollectionNameConstant.COLLECTION_VENUE) { }
    }
}
