using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingSystem.Models;
using BillingSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private IAppRepository context;
        public RegisterController(IAppRepository appDbContext)
        {
            context = appDbContext;
        }

        public IActionResult Create()
        {
            return Ok();
        }

        public IActionResult Read()
        {
            var res = context.GetAllTransactions();
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                context.AddTransaction(transaction);
                return RedirectToAction("Read");
            }
            else
                return Ok();
        }
    }
}