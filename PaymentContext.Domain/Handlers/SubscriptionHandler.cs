using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, 
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePayPalSubscriptionCommand>,
    IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail fast validations
            command.Validate();
            if(command.Invalid)
            {   
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura!");
            }

            //Verificar se Documento já está cadastrado
            if(_studentRepository.DocumentExists(command.Document))
               AddNotification("Document", "Este CPF já está cadastrado!");

            //Verificar se Email já está cadastrado
            if(_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está em usi");

            //Gerar VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode, 
                command.BoletoNumber, 
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer, 
                new Document(command.PayerDocument, command.PayerDocumentType), 
                address, 
                email
            );

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Aplicar as Validações
            AddNotifications(document, email, address, student, subscription, payment);

            //Chegar as informações
            if(Invalid)
                return new CommandResult(false, "Não foi possível assinar sua assinatura");

            //Salvar as Informações
            _studentRepository.CreateSubscription(student);

            //Enviar E-mail de boas Vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao balta.io!", "Sua assinatura foi criada!");

            //Retornar as informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            //Fail fast validations
            // command.Validate();
            // if(command.Invalid)
            // {   
            //     AddNotifications(command);
            //     return new CommandResult(false, "Não foi possível realizar sua assinatura!");
            // }

            //Verificar se Documento já está cadastrado
            if(_studentRepository.DocumentExists(command.Document))
               AddNotification("Document", "Este CPF já está cadastrado!");

            //Verificar se Email já está cadastrado
            if(_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está em usi");

            //Gerar VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode, 
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer, 
                new Document(command.PayerDocument, command.PayerDocumentType), 
                address, 
                email
            );

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Aplicar as Validações
            AddNotifications(document, email, address, student, subscription, payment);

            //Salvar as Informações
            _studentRepository.CreateSubscription(student);

            //Enviar E-mail de boas Vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao balta.io!", "Sua assinatura foi criada!");

            //Retornar as informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            throw new NotImplementedException();
        }
    }
}