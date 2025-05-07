using FabianoIO.Core.Messages;
using MediatR;

namespace FabianoIO.ManagementStudents.Application.Commands
{
    public class UserCommandHandler : IRequestHandler<AddUserCommand, bool>
    {
        public async Task<bool> Handle(AddUserCommand message, CancellationToken cancellationToken)
        {
            if(!ValidateCommand(message)) return false;

            return true;
        }

        private bool ValidateCommand(Command message)
        {
            if(message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                //lancar um evento de erro
            }

            return false;
        }
    }
}
