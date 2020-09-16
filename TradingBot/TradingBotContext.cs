using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradingBot
{
    class TradingBotContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }
        public DbSet<Top5Stopcks> Top5Stocks { get; set; }
        public DbSet<CashTransaction> CashTransaction { get; set; }
        public DbSet<Models.StockTransaction> StockTransactions { get; set; }
        public DbSet<Models.StockPrice> StockPrices { get; set; }
        public DbSet<Models.OwnedStock> OwnedStocks { get; set; }
    }
}
