using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.Repositories.Interfaces;

namespace com.petronas.myevents.api.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository() : base(CollectionNames.COLLECTION_EVENT) { }
    }
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository() : base(CollectionNames.COLLECTION_LOCATION) { }
    }
    public class SessionRepository : BaseRepository<Session>, ISessionRepository
    {
        public SessionRepository() : base(CollectionNames.COLLECTION_SESSION) { }
    }
    public class SubSessionRepository : BaseRepository<SubSession>, ISubSessionRepository
    {
        public SubSessionRepository() : base(CollectionNames.COLLECTION_SUBSESSION) { }
    }
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository() : base(CollectionNames.COLLECTION_USER) { }
    }
    public class VenueRepository : BaseRepository<Venue>, IVenueRepository
    {
        public VenueRepository() : base(CollectionNames.COLLECTION_VENUE) { }
    }
}
