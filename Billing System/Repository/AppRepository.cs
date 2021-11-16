using BillingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillingSystem.Repository
{
    /// <summary>
    /// This an intermediate layer called Repository Patern used for decouple code, and keep controller as clean as posible
    /// </summary>
    public class AppRepository : IAppRepository
    {
        private readonly AppDBContext _dbContext;

        public AppRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        ///Getting AllTransactions as Collection
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Transaction> GetAllTransactions() => _dbContext.Transactions.ToList();

        /// <summary>
        ///Getting TransactionById as Transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Transaction GetTransactionById(Guid id) => _dbContext.Transactions.FirstOrDefault(x=> x.Id == id);

        /// <summary>
        ///Adding Transaction to contextDB
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Transaction AddTransaction(Transaction transaction)
        {
            transaction.Id = Guid.NewGuid();
            _dbContext.Transactions.Add(transaction);
            try
            {
                _dbContext.SaveChanges();
            }
            catch
            {
                throw new Exception("There is an expecion saving data");
            }
            return transaction;
        }

        /// <summary>
        ///It is a service for update transaction as Billed. It will call a private method UpdateBillingStatus
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public BillingStatus SetTransactionAsBilled(Transaction transaction)
        {
            return UpdateBillingStatus(transaction, (int)BillingStatus.Billed);
        }

        /// <summary>
        ///It is a service for update transaction as Paid. It will call a private method UpdateBillingStatus
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public BillingStatus SetTransactionAsPaid(Transaction transaction)
        {
            return UpdateBillingStatus(transaction, (int)BillingStatus.Paid);
        }

        private BillingStatus UpdateBillingStatus(Transaction transaction, int billingStatus)
        {
            var existingTransaction = _dbContext.Transactions.Find(transaction.Id);
            if (existingTransaction != null)
            {
                existingTransaction.PaymentStatus = billingStatus;

                _dbContext.Transactions.Update(existingTransaction);
                _dbContext.SaveChanges();
            }
            return (BillingStatus)billingStatus;
        }

        #region Invoices
        /// <summary>
        ///Getting AllTransactions as Collection
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Invoice> GetAllInvoices() => _dbContext.Invoices.ToList();

        /// <summary>
        ///Getting GetInvoiceById as Invoice
        /// </summary>
        /// <returns></returns>
        public Invoice GetInvoiceById(Guid id) => _dbContext.Invoices.FirstOrDefault(x => x.Id == id);

        /// <summary>
        ///Adding Invoice to contextDB
        /// </summary>
        /// <returns></returns>
        public Invoice AddInvoice(Invoice invoice)
        {
            invoice.Id = Guid.NewGuid();
            _dbContext.Invoices.Add(invoice);
            try
            { 
                _dbContext.SaveChanges();
            }
            catch
            {
                throw new Exception("There is an expecion saving data");
            }
            return invoice;
        }

        /// <summary>
        /// Service for Generate Billed Transactions
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IEnumerable<Invoice> GenerateInvoices(DateTime from, DateTime to)
        {
            DateTime dateTo = to.AddHours(23).AddMinutes(59).AddSeconds(59);
            //Here Im taking a collection from Transactions with date from and to, and also payment is Unbilled
            var existingTransaction = _dbContext.Transactions.Where(x => (x.Date >= from && x.Date <= dateTo) && x.PaymentStatus == (int)BillingStatus.UnBilled).ToList();
            if (existingTransaction != null)
            {
                foreach(var transaction in existingTransaction)
                {
                    //Here the model is being created and the model is added to the data set that represents a table in the database
                    Invoice invoice = new Invoice();
                    invoice.BillNumber = _dbContext.Invoices.Count() + 1;
                    invoice.Date = System.DateTime.Now;
                    invoice.InvoiceAmount = transaction.TransactionAmount;
                    invoice.InvoiceDescription = "Bill Id:" + transaction.Id;
                    invoice.PaymentStatus = (int)BillingStatus.Billed;

                    //Saving just Invoices for Billed transactions
                    _dbContext.Invoices.Add(invoice);
                    _dbContext.SaveChanges();

                    var updateTransaction = _dbContext.Transactions.Find(transaction.Id);
                    if (updateTransaction != null)
                    {
                        updateTransaction.PaymentStatus = (int)BillingStatus.Billed;
                        updateTransaction.BillNumber = invoice.BillNumber;

                        _dbContext.Transactions.Update(updateTransaction);
                        _dbContext.SaveChanges();
                    }
                }
            }
            return _dbContext.Invoices;
        }
        #endregion
    }
}
