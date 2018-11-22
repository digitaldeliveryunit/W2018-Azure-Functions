using System;
namespace com.petronas.myevents.api.ViewModels
{
    /// <summary>
    /// Viewmodel for GetSpotlight API
    /// </summary>
    public class EventSpotlightResponse
    {
        public string SpotlightId { get; set; }
        public string SpotlightName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
