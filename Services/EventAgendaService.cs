using System.Collections.Generic;
using System.Linq;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Repositories.Interfaces;
using com.petronas.myevents.api.Services.Interfaces;
using com.petronas.myevents.api.ViewModels;

namespace com.petronas.myevents.api.Services
{
    public class EventAgendaService : IEventAgendaService
    {
        private readonly ISessionRepository _sessionRepossitory;
        private readonly IUserService _userService;

        public EventAgendaService(ISessionRepository sessionRepossitory,
            IUserService userService)
        {
            _sessionRepossitory = sessionRepossitory;
            _userService = userService;
        }

        public IEnumerable<EventAgendaResponse> GetAgendas(string eventId)
        {
            var user = _userService.GetCurrentUser();
            var result = _sessionRepossitory.GetAll(x => !x.IsDeleted && x.EventId == eventId, null).ToList().Select(
                s => new EventAgendaResponse
                {
                    AgendaId = s.Id,
                    AgendaName = s.AgendaName,
                    Venue = s.Venue.VenueName,
                    Day = s.Day,
                    UserStatus = s.Members.Any(x => !x.IsDeleted && x.UserId == user.Id)
                        ? s.Members.FirstOrDefault(x => !x.IsDeleted && x.UserId == user.Id)?.EventMemberStatus
                        : UserStatus.NEW.ToString(),
                    SubAgendas = s.SubSessions.Select(ss => new EventSubAgendaResponse
                    {
                        SubAgendaId = ss.Id,
                        AgendaName = ss.AgendaName,
                        TimeFrom = ss.TimeFrom,
                        TimeTo = ss.TimeTo,
                        Venue = ss.Venue.VenueName
                    }).ToList()
                });
            return result;
        }
    }
}