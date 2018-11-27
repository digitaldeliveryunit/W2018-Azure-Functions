namespace com.petronas.myevents.api.RequestContracts
{
    public class RouteRequest {
        public string SearchKey {get;set;}
        public int Skip { get; set; }
        public int Take {get;set;}
        public string ContinuationKey {get;set;}
    }
}