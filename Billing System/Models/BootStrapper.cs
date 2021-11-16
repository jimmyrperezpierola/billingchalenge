using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BillingSystem.Models
{
    /// <summary>
    /// This Class will create mock data at starting the solution
    /// </summary>
    public class BootStrapper
    {
        public static void Initialize()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>().UseInMemoryDatabase("BillingDB").Options;
            using var context = new AppDBContext(options);

            // Check already existing any data
            if (context.Transactions.Any())
                return;   // DB already filled

            //It is filling and saving transactions at dbContext
            context.Transactions.AddRange(GetMockTransactionsList());
            context.SaveChanges();
        }

        //Method will Mock Transactions to dbContext
        public static List<Transaction> GetMockTransactionsList()
        {
            return new List<Transaction>
            {
                new Transaction
                 {
                     Id = System.Guid.NewGuid(),
                     Date = System.DateTime.Now,
                     TransactionAmount = 127.75m,
                     TransactionDescription = "Transaction #001264",
                     PaymentStatus = 1
                 },
                 new Transaction
                 {
                     Id = System.Guid.NewGuid(),
                     Date = System.DateTime.Now.AddDays(7),
                     TransactionAmount = 289.93m,
                     TransactionDescription = "Transaction #001265",
                     PaymentStatus = 1
                 },
                 new Transaction
                 {
                     Id = System.Guid.NewGuid(),
                     Date = System.DateTime.Now.AddMonths(1),
                     TransactionAmount = 284.62m,
                     TransactionDescription = "Transaction #001266",
                     PaymentStatus = 1
                 },
                 new Transaction
                 {
                     Id = System.Guid.NewGuid(),
                     Date = System.DateTime.Now.AddMonths(2),
                     TransactionAmount = 154.82m,
                     TransactionDescription = "Transaction #001267",
                     PaymentStatus = 1
                 },
                 new Transaction
                 {
                     Id = System.Guid.NewGuid(),
                     Date = System.DateTime.Now.AddDays(3),
                     TransactionAmount = 584.92m,
                     TransactionDescription = "Transaction #001268",
                     PaymentStatus = 1
                 }
            };
        }
    }
}
