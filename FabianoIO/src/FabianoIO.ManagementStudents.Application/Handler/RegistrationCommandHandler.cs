﻿using FabianoIO.Core.Messages;
using FabianoIO.Core.Messages.Notifications;
using FabianoIO.ManagementStudents.Aplication.Commands;
using FabianoIO.ManagementStudents.Domain;
using MediatR;

namespace FabianoIO.ManagementStudents.Application.Handler;

public class RegistrationCommandHandler(IMediator _mediator,
                                        IRegistrationRepository _registrationRepository,
                                        IUserRepository _userRepository) : IRequestHandler<AddRegistrationCommand, bool>
{
    public async Task<bool> Handle(AddRegistrationCommand request, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(request))
            return false;

        var student = await _userRepository.GetById(request.StudentId);
        if (student == null)
        {
            await _mediator.Publish(new DomainNotification(request.MessageType, "Aluno não encontrado."), cancellationToken);
            return false;
        }

        _registrationRepository.AddRegistration(request.StudentId, request.CourseId);

        return await _registrationRepository.UnitOfWork.Commit();
    }

    private bool ValidateCommand(Command command)
    {
        if (command.IsValid())
            return true;
        foreach (var erro in command.ValidationResult.Errors)
        {
            _mediator.Publish(new DomainNotification(command.MessageType, erro.ErrorMessage));
        }
        return false;
    }
}