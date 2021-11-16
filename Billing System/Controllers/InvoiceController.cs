using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IAppRepository _repository;

        public InvoiceController(IAppRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("/api/invoices/generateinvoices/{from}/{to}")]
        public IActionResult GenerateInvoices(DateTime from, DateTime to)
        {
            return Ok(_repository.GenerateInvoices(from, to));
        }
    }
}