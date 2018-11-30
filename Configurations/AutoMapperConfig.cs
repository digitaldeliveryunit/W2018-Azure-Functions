using AutoMapper;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.ViewModels;

namespace com.petronas.myevents.api.Configurations
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Spotlight, EventSpotlightResponse>()
                .ForMember(t => t.SpotlightId, f => f.MapFrom(x => x.Id))
                .ForMember(t => t.Description, f => f.MapFrom(x => x.SpotlightDescription));

                config.CreateMap<SubSession, EventSubAgendaResponse>()
                .ForMember(t => t.SubAgendaId, f => f.MapFrom(x => x.Id))
                .ForMember(t => t.TimeFrom, f => f.MapFrom(x => x.TimeFrom))
                .ForMember(t => t.TimeTo, f => f.MapFrom(x => x.TimeTo))
                .ForMember(t => t.Venue, f => f.MapFrom(x => x.Venue.VenueName))
                .ForMember(t => t.AgendaName, f => f.MapFrom(x => x.AgendaName));
            });
        }
    }
}
