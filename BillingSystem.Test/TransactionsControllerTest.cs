using System;
using Xunit;
using BillingSystem.Controllers;
using BillingSystem.Models;
using BillingSystem.Repository;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BillingSystem.Test
{
    public class TransactionsControllerTest
    {
        private readonly IAppRepository _repository;
        IEnumerable<Transaction> aTransaction;

        [Fact]
        public async Task GetAllTransactions()
        {
            //Arrange   (Enfoque)
            var r = new Transaction()
            {
                Id= Guid.NewGuid(),
                TransactionAmount = 100,
                TransactionDescription = "test",
                Date = System.DateTime.Now,
                PaymentStatus = 1
            };
            var mockRepo = new Mock<IAppRepository>();
            mockRepo.Setup(repo => repo.GetAllTransactions()).Returns(GetTestTransactions());
            var controller = new RegisterController(mockRepo.Object);

            // Act (Accion)
            var result = controller.Read();

            // Assert (Hacer Valer)
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Transaction>>(viewResult.ViewData.Model);

            aTransaction = GetTestTransactions();
            Assert.Equal(aTransaction, model);
        }

        private static List<Transaction> GetTestTransactions()
        {
            var registrations = new List<Transaction>();
            registrations.Add(new Transaction()
            {
                Id = System.Guid.NewGuid(),
                Date = System.DateTime.Now,
                TransactionAmount = 127.75m,
                TransactionDescription = "Transaction #001264",
                PaymentStatus = 1
            });
            registrations.Add(new Transaction()
            {
                Id = System.Guid.NewGuid(),
                Date = System.DateTime.Now.AddDays(7),
                TransactionAmount = 289.93m,
                TransactionDescription = "Transaction #001265",
                PaymentStatus = 1
            });
            registrations.Add(new Transaction()
            {
                Id = System.Guid.NewGuid(),
                Date = System.DateTime.Now.AddMonths(1),
                TransactionAmount = 284.62m,
                TransactionDescription = "Transaction #001266",
                PaymentStatus = 1
            });
            return registrations;
        }
    }
}
