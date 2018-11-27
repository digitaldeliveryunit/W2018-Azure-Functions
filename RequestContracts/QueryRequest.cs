namespace com.petronas.myevents.api.RequestContracts
{
    public class ListingRequest
    {
        public string SearchKey { get; set; }
        public int Take { get; set; }
        public string SkipKey { get; set; }
    }
}