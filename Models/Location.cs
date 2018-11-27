namespace com.petronas.myevents.api.Models
{
    public class Location : ModelBase
    {
        public string LocationName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}