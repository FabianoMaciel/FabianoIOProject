using FluentValidation;
using FabianoIO.Core.Messages;

namespace FabianoIO.ManagementStudents.Aplication.Commands;

public class AddRegistrationCommand(Guid studentId, Guid courseId) : Command
{
    public Guid StundentId { get; set; } = studentId;
    public Guid CourseId { get; set; } = courseId;

    public override bool IsValid()
    {
        ValidationResult = new AddRegistrationCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
public class AddRegistrationCommandValidation : AbstractValidator<AddRegistrationCommand>
{
    public static string StudentIdError = "O campo AlunoId não pode ser vazio.";
    public static string CourseIdError = "O campo CursoId não pode ser vazio.";
    public AddRegistrationCommandValidation()
    {
        RuleFor(c => c.StundentId)
            .NotEqual(Guid.Empty)
            .WithMessage(StudentIdError);
        RuleFor(c => c.CourseId)
            .NotEqual(Guid.Empty)
            .WithMessage(CourseIdError);
    }
}