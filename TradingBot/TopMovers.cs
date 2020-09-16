using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradingBot
{
    class TopMovers
    {
        private TradingBotContext _ctx;
        private string _url;
        private List<TopMoverRootObject> _movers;

        public TopMovers(TradingBotContext ctx)
        {
            _ctx = ctx;
            _url = "https://finance.yahoo.com//screener/predefined/";
            FetchMovers();
        }
    
        public List<TopMoverRootObject> Top5
        {
            get
            {
                return _movers;
            }
        }

        public void FetchMovers()
        {
            string jsonstring;
            using(var wc = new System.Net.WebClient())
            {
                jsonstring = wc.DownloadString(_url);
            }
            /* the data is a json blob embedded into the html */
            jsonstring = jsonstring.Split(new string[] { "\"results\":{\"rows\":" }, StringSplitOptions.None)[1];
            jsonstring = jsonstring.Split(new String[] { "]" }, StringSplitOptions.None)[0] + "]";

            List<TopMoverRootObject> results = JsonConvert.DeserializeObject<List<TopMoverRootObject>>(jsonstring);
            _movers = new List<TopMoverRootObject>();
            _movers.AddRange(results.Take(5));

            Console.WriteLine("Top 5 movers discovered:");
            foreach(var m in _movers)
            {
                Console.WriteLine(string.Format("{0} - \t{1} \t{2}", m.symbol, m.regularMarketChangePercent.fmt));
            }

            CommitToDb();
        }
         
        private void CommitToDb()
        {
            var lastentry = _ctx.Top5Stocks.OrderByDescending(i => i.Id).ThenByFirstOrDefault();
            if(lastentry != null && lastentry.Symbol1 == _movers[0].symbol && lastentry.Symbol2 == _mover2)
        }

    }
}
