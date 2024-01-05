using System;

class Program
{
    static void Main()
    {
        // Alpha Vantage API Key'i
        string apiKey = "QYOD9Z3W3OAGBGU9";
        // İlgili hisse senedi simgesi (örneğin: AAPL)
        string stockSymbol = "AAPL";

        // StockApiService sınıfını oluşturun
        StockApiService stockApiService = new StockApiService(apiKey);

        // Dün ve bugünün fiyatlarını al
        var (yesterdayPrice, todayPrice) = stockApiService.GetLastTwoDaysPrices(stockSymbol);
        Console.WriteLine($"Dünden Bugüne Kapanış Fiyatları: Dün: {yesterdayPrice}, Bugün: {todayPrice}");

        Console.ReadLine();
    }
}
