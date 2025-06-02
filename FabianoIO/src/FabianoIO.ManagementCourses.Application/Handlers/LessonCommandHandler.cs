using FabianoIO.Core.Interfaces.Repositories;
using FabianoIO.Core.Messages;
using FabianoIO.Core.Messages.Notifications;
using FabianoIO.ManagementCourses.Application.Commands;
using FabianoIO.ManagementCourses.Domain;
using MediatR;

namespace FabianoIO.ManagementCourses.Application.Handlers
{
    public class LessonCommandHandler(ILessonRepository lessonRepository,
                                IMediator mediator) : IRequestHandler<AddLessonCommand, bool>
    {
        public async Task<bool> Handle(AddLessonCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateComand(request)) return false;

            var lesson = new Lesson(request.Name, request.Subject, request.TotalHours, request.CourseId);

            lessonRepository.Add(lesson);

            return await lessonRepository.UnitOfWork.Commit();
        }

        private bool ValidateComand(Command command)
        {
            if (command.IsValid()) return true;

            foreach (var erro in command.ValidationResult.Errors)
            {
                mediator.Publish(new DomainNotification(command.MessageType, erro.ErrorMessage));
            }

            return false;
        }
    }
}
