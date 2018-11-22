using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.Repositories.Interfaces;

namespace com.petronas.myevents.api.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository() : base(CollectionNames.COLLECTION_EVENT) { }
    }
}
