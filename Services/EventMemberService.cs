using System;
using System.Linq;
using System.Threading.Tasks;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.Repositories.Interfaces;
using com.petronas.myevents.api.Services.Interfaces;

namespace com.petronas.myevents.api.Services
{
    public class EventMemberService : IEventMemberService
    {
        private readonly IEventMemberRepository _eventMemberRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        public EventMemberService(IEventMemberRepository eventMemberRepository, IEventRepository eventRepository, IUserRepository userRepository)
        {
            _eventMemberRepository = eventMemberRepository;
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }
        public EventMember CheckExistence(string eventid, string userId)
        {
            return _eventMemberRepository.GetAll().FirstOrDefault(x => !x.IsDeleted && x.EventId == eventid && x.UserId == userId);
        }

        public async Task<bool> Join(string eventId, string userId)
        {
            var joined = _eventMemberRepository.GetAll().FirstOrDefault(x => !x.IsDeleted && x.EventId == eventId && x.UserId == userId);
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
                var ev = _eventRepository.GetAll().FirstOrDefault(x => !x.IsDeleted && x.Id == eventId);
                ev.Members.Add(newMember);
                await _eventMemberRepository.Add(newMember);
                await  _eventRepository.Update(ev);
            }
            else
            {
                joined.EventMemberStatus = UserStatus.JOINED.ToString();
                await _eventMemberRepository.Update(joined);
            }
            return true;
        }

        public async Task<bool> UnJoin(string eventId, string userId)
        {
            var joined = _eventMemberRepository.GetAll().Where(x => !x.IsDeleted && x.EventId == eventId && x.UserId == userId).ToList();
            var ev = _eventRepository.GetAll().FirstOrDefault(x => !x.IsDeleted && x.Id == eventId);
            foreach (var item in joined)
            {
                item.IsDeleted = true;
                ev.Bookmarks.Remove(ev.Bookmarks.FirstOrDefault(x => x.Id == item.Id));
                await _eventMemberRepository.Update(item);
            }
            await _eventRepository.Update(ev);
            return true;
        }
    }
}
