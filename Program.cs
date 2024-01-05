using System;

class Program
{
    static void Main()
    {
        
        string apiKey = "QYOD9Z3W3OAGBGU9";
        
        string stockSymbol = "AAPL";

        
        StockApiService stockApiService = new StockApiService(apiKey);

        
        var (yesterdayPrice, todayPrice) = stockApiService.GetLastTwoDaysPrices(stockSymbol);
        Console.WriteLine($"Dünden Bugüne Kapanış Fiyatları: Dün: {yesterdayPrice}, Bugün: {todayPrice}");

        Console.ReadLine();
    }
}
