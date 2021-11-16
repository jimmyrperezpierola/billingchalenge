using System;
using BillingSystem.Models;
using BillingSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BillingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IAppRepository _repository;

        public TransactionController(IAppRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("/api/transactions")]
        public IActionResult GetAllTransactions()
        {
            return Ok(_repository.GetAllTransactions());
        }

        [HttpGet("/api/transactions/{id}")]
        public IActionResult GetTransactionById(Guid id)
        {
            Transaction transaction = _repository.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }


        [HttpPost("/api/transactions")]
        public IActionResult AddTransaction([FromBody] Transaction transaction)
        {
            var result = _repository.AddTransaction(transaction);
            return Ok(result);
        }

        [HttpPut("/api/transactions/settransactionasbilled/{id}")]
        public IActionResult SetTransactionAsBilled(Guid id)
        {
            var existingTransaction = _repository.GetTransactionById(id);
            if (existingTransaction != null)
            {
                existingTransaction.Id = existingTransaction.Id;
                _repository.SetTransactionAsBilled(existingTransaction);
            }

            return Ok(existingTransaction);
        }

        [HttpPut("/api/transactions/settransactionaspaid/{id}")]
        public IActionResult SetTransactionAsPaid(Guid id)
        {
            var existingTransaction = _repository.GetTransactionById(id);
            if (existingTransaction != null)
            {
                existingTransaction.Id = existingTransaction.Id;
                _repository.SetTransactionAsPaid(existingTransaction);
            }

            return Ok(existingTransaction);
        }

        [HttpPost("/api/invoices")]
        public IActionResult AddInvoice([FromBody] Invoice invoice)
        {
            var result = _repository.AddInvoice(invoice);
            return Ok(result);
        }


        #region Invoices

        [HttpGet("/api/invoices")]
        public IActionResult GetAllInvoices()
        {
            return Ok(_repository.GetAllInvoices());
        }

        [HttpGet("/api/invoices/{id}")]
        public IActionResult GetInvoiceById(Guid id)
        {
            Invoice invoice = _repository.GetInvoiceById(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }
        #endregion

    }
}