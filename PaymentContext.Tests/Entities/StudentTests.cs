using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;

        private readonly Email _email;
        private readonly Address _address;
        private readonly Student _student;

        public StudentTests()
        {
            _name = new Name("Bruce", "Wayne");
            _document = new Document("81853845094", EDocumentType.CPF);
            _email = new Email("batman@dc.com");
            _address = new Address("Rua 1", "231", "Santa Luzia", "Franca", "SP", "BR", "14406574");
             _student = new Student(_name, _document, _email);
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, _address, _email);

            //_subscription.AddPayment(payment);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, _address, _email);

            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);
            _student.AddSubscription(subscription);

            Assert.IsTrue(_student.Invalid);
        }

    
        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            var subscription = new Subscription(null);
            _student.AddSubscription(subscription);
            Assert.IsTrue(_student.Invalid);
        }
        
       [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var subscription = new Subscription(null);

            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, _address, _email);

            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);

            Assert.IsTrue(_student.Valid);
        }        
    }
}
