using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult()
        {

        }
        public CommandResult(bool success, string message)
        {
            this.Success = success;
            this.Message = message;

        }
        public bool Success { get; set; }
        public string Message { get; private set; }

        public void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}