using System;
using System.Collections.Generic;
using System.Text;

namespace TradingBot
{
    class CashManager
    {
        // Dependency Injection
        private TradingBotContext _ctx;
        public CashManager(TradingBotContext ctx)
        {
            _ctx = ctx;
            CheckStatus();
        }

        public void CheckStatus()
        {
            if(_ctx.CashTransactions.ToList().Count == 0)
            {
                Console.WriteLine("No transactions made yet, generating $1000 to begin.");
                _ctx.CashTransactions.Add(new CashTransaction()
                {
                    Val = 1000,
                    Comments = "Initial Load"

                });
            }
            else
            {
                Console.WriteLine(string.Format("Available Balance: {0}", _ctx.CashTransactions.Sum(i => i.Val)));

            }
        }

        public float AvailableBalance()
        {
            return _ctx.CashTransactions.Sum(i => i.Val);
        }
    }
}
