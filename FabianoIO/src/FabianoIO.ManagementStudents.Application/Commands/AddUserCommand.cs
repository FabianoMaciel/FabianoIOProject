using FabianoIO.Core.Messages;
using FluentValidation;

namespace FabianoIO.ManagementStudents.Application.Commands
{
    public class AddUserCommand : Command
    {
        public string UserName { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = string.Empty;

        public AddUserCommand(string userName, bool isAdmin, string name, string lastName, DateTime dateOfBirth, string email)
        {
            UserName = userName;
            IsAdmin = isAdmin;
            Name = name;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddUserCommandValidation : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidation()
        {
            RuleFor(c => c.UserName)
                .NotEmpty()
                .WithMessage("User Name is required");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name is needed");

            RuleFor(c => c.LastName)
                .NotEmpty()
                .WithMessage("Last Name is needed");

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("Email is required");
        }
    }
}
