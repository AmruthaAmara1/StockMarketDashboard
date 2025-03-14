using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockMarketDashboard
{
    public class StockDataService
    {
        private const string ApiKey = "<your api key>"; // Twelvedata API Key
        private const string BaseUrl = "https://api.twelvedata.com/time_series";

        // Method to fetch stock data for the top 8 companies
        public async Task<List<StockData>> GetTop8StockDataAsync()
        {
            var symbols = new List<string> { "AAPL", "MSFT", "GOOGL", "AMZN", "TSLA", "META", "NFLX", "NVDA" }; 
            var stockDataList = new List<StockData>();

            using (var client = new HttpClient())
            {
                foreach (var symbol in symbols)
                {
                    var url = $"{BaseUrl}?symbol={symbol}&interval=1day&apikey={ApiKey}";
                    var response = await client.GetStringAsync(url);

                    var stockDataResponse = JsonConvert.DeserializeObject<StockDataResponse>(response);

                    if (stockDataResponse?.Values != null && stockDataResponse.Values.Length > 0)
                    {
                        var stockData = stockDataResponse.Values[0];
                        stockDataList.Add(new StockData
                        {
                            Symbol = symbol,
                            Open = stockData.Open,
                            High = stockData.High,
                            Low = stockData.Low,
                            Close = stockData.Close,
                            Volume = stockData.Volume
                        });
                    }
                }
            }

            return stockDataList;
        }
    }
}