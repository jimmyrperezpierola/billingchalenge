using BillingSystem.Models;
using System;
using System.Collections.Generic;

namespace BillingSystem.Repository
{
    public interface IAppRepository
    {
        public IEnumerable<Transaction> GetAllTransactions();
        public Transaction GetTransactionById(Guid id);
        public Transaction AddTransaction(Transaction transaction);
        public BillingStatus SetTransactionAsBilled(Transaction transaction);
        public BillingStatus SetTransactionAsPaid(Transaction transaction);


        #region Invoices
        public IEnumerable<Invoice> GetAllInvoices();
        public Invoice GetInvoiceById(Guid id);
        public Invoice AddInvoice(Invoice invoice);

        public IEnumerable<Invoice> GenerateInvoices(DateTime from, DateTime to);
        #endregion
    }
}
