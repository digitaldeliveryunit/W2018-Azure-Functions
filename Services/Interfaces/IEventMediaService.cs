using System.Collections.Generic;
using com.petronas.myevents.api.Models;

namespace com.petronas.myevents.api.Services.Interfaces
{
    public interface IEventMediaService
    {
        IEnumerable<Media> GetMedias(string eventId, string mediaType);
    }
}