namespace StockMarketDashboard
{
    // Classes to match the response from TwelveData API
    public class StockDataResponse
    {
        public MetaData Meta { get; set; }
        public StockDataItem[] Values { get; set; }
    }
}
