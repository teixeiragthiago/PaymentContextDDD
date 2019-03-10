using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "99999999999";
            command.Email = "email@email.com2";
            command.BarCode = "";
            command.BoletoNumber = "51161655166";
            command.PaymentNumber = "2232";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 59;
            command.TotalPaid = 59;
            command.Payer = "Wayne Corp";
            command.PayerDocument = "115155176";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "batman@dc.com";
            command.Street = "dsdada";
            command.Number = "2121";
            command.Neighborhood = "vila tião";
            command.City = "Franca";
            command.State = "SP";
            command.Country = "BR";
            command.ZipCode = "14406578";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}