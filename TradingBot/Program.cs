using System;

namespace TradingBot
{
    class Program
    {
        static void Main(string[] args)
        {

            TradingBotContext ctx = new TradingBotContext();
            Console.WriteLine(string.Format("Trading bot started at {0}", DateTime.Now));

            //check cash status, if first time loaded give us 1000
            var cashman = new CashManager(ctx);

            //get today's top movers
            var topmovers = new TopMovers(ctx);

            //buy what we can from the top movers.
            var stockAgent = new StockAgent(ctx);
            var ownedstockAgent = new OwnedStockAgent(ctx);


            //Sell any left over stocks. - recalc to ensure we don't sell any false stock numbers
            ownedstockAgent.BuildPortfolioFromTransactions();
            stockAgent.SellOldStocks();

            stockAgent.PurchaseNewMovers(cashman.AvailableBalanace());


        }
    }
}
