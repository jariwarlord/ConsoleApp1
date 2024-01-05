using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

public class StockApiService
{
    private readonly string apiKey;
    private readonly string apiUrl;

    public StockApiService(string apiKey)
    {
        this.apiKey = apiKey;
        this.apiUrl = "https://www.alphavantage.co/query";
    }

    public (double yesterdayPrice, double todayPrice) GetLastTwoDaysPrices(string symbol)
    {
        string queryString = $"{apiUrl}?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={apiKey}";
        string json_data = DownloadJsonData(queryString);

        return ParsePrices(json_data);
    }

    private string DownloadJsonData(string queryString)
    {
        using (WebClient client = new WebClient())
        {
            try
            {
                return client.DownloadString(queryString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata oluştu: " + ex.Message);
                return null;
            }
        }
    }

    private (double, double) ParsePrices(string jsonData)
    {
        var stockData = JsonConvert.DeserializeObject<StockData>(jsonData);

        if (stockData?.TimeSeries != null && stockData.TimeSeries.Count >= 2)
        {
            var lastTwoDays = stockData.TimeSeries.Take(2).ToList();
            var yesterdayPrice = lastTwoDays[1].Value.Close;
            var todayPrice = lastTwoDays[0].Value.Close;

            return (yesterdayPrice, todayPrice);
        }

        return (0.0, 0.0);
    }

    private class StockData
    {
        [JsonProperty("Time Series (Daily)")]
        public Dictionary<string, StockPrice> TimeSeries { get; set; }
    }

    private class StockPrice
    {
        [JsonProperty("4. close")]
        public double Close { get; set; }
    }
}
