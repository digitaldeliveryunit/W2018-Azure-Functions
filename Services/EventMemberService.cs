using System;
using System.Linq;
using System.Threading.Tasks;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.Repositories.Interfaces;
using com.petronas.myevents.api.Services.Interfaces;
using Microsoft.Azure.Documents.Client;

namespace com.petronas.myevents.api.Services
{
    public class EventMemberService : IEventMemberService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        public EventMemberService(IEventRepository eventRepository, IUserRepository userRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }
        public EventMember CheckExistence(string eventid, string userId)
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = 1,
                EnableCrossPartitionQuery = true
            };
            return _eventRepository.GetAll(x => !x.IsDeleted && x.Id == eventid, feedOptions).FirstOrDefault().Members.Where(x => !x.IsDeleted && x.UserId == userId).FirstOrDefault();
        }

        public async Task<bool> Join(string eventId, string userId)
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = 1,
                EnableCrossPartitionQuery = true
            };
            var joined = _eventRepository.GetAll(x=>!x.IsDeleted && x.Id == eventId, feedOptions).FirstOrDefault().Members.Where(x => !x.IsDeleted && x.UserId == userId).FirstOrDefault();
            if (joined == null)
            {
                var newMember = new EventMember()
                {
                    Id = Guid.NewGuid().ToString(),
                    Discriminator = CollectionNameConstant.MODEL_EVENT_MEMBER,
                    EventMemberStatus = UserStatus.JOINED.ToString(),
                    UserId = userId,
                    EventId = eventId
                };
                var ev = _eventRepository.GetAll(x => !x.IsDeleted && x.Id == eventId, feedOptions).FirstOrDefault();
                ev.Members.Add(newMember);
                await _eventRepository.Update(ev);
            }
            else
            {
                var ev = _eventRepository.GetAll(x => !x.IsDeleted && x.Id == eventId, feedOptions).FirstOrDefault();
                var oldMember = ev.Members.Where(x => !x.IsDeleted && x.Id == joined.Id).FirstOrDefault();
                oldMember.EventMemberStatus = UserStatus.JOINED.ToString();
                await _eventRepository.Update(ev);
            }
            return true;
        }

        public async Task<bool> UnJoin(string eventId, string userId)
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = 1,
                EnableCrossPartitionQuery = true
            };

            var joined = _eventRepository.GetAll(x => !x.IsDeleted && x.Id == eventId, null).FirstOrDefault().Members.Where(x=>!x.IsDeleted).ToList();
            var ev = _eventRepository.GetAll(x => !x.IsDeleted && x.Id == eventId, feedOptions).FirstOrDefault();
            foreach (var item in joined)
            {
                ev.Bookmarks.Remove(ev.Bookmarks.FirstOrDefault(x => x.Id == item.Id));
            }
            await _eventRepository.Update(ev);
            return true;
        }
    }
}
