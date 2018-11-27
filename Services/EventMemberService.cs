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
            return _eventRepository.GetAll(x => !x.IsDeleted && x.Id == eventid, feedOptions).FirstOrDefault().Members
                .Where(x => !x.IsDeleted && x.UserId == userId).FirstOrDefault();
        }

        public async Task<bool> Join(string eventId, string userId)
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = 1,
                EnableCrossPartitionQuery = true
            };
            var _event = _eventRepository.GetAll(x => !x.IsDeleted && x.Id == eventId, feedOptions).ToList()
                .FirstOrDefault();
            if (!_event.Members.Any(x => !x.IsDeleted && x.UserId == userId))
            {
                var newMember = new EventMember
                {
                    Id = Guid.NewGuid().ToString(),
                    //Discriminator = CollectionNameConstant.MODEL_EVENT_MEMBER,
                    EventMemberStatus = UserStatus.JOINED.ToString(),
                    UserId = userId,
                    EventId = eventId
                };
                _event.Members.Add(newMember);
                await _eventRepository.Update(_event);
            }
            else
            {
                var oldMember = _event.Members.Where(x => !x.IsDeleted && x.UserId == userId).FirstOrDefault();
                oldMember.EventMemberStatus = UserStatus.JOINED.ToString();
                await _eventRepository.Update(_event);
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

            var _event = _eventRepository.GetAll(x => !x.IsDeleted && x.Id == eventId, null).ToList().FirstOrDefault();
            for (var i = 0; i < _event.Members.Count; i++)
                _event.Members.Remove(_event.Members.FirstOrDefault(x => x.Id == _event.Members[i].Id));
            await _eventRepository.Update(_event);
            return true;
        }
    }
}