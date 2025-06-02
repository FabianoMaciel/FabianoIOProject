using FabianoIO.Core.Interfaces.Repositories;
using FabianoIO.Core.Messages;
using FabianoIO.Core.Messages.Notifications;
using FabianoIO.ManagementCourses.Application.Commands;
using FabianoIO.ManagementCourses.Domain;
using MediatR;

namespace FabianoIO.ManagementCourses.Application.Handlers
{
    public class CourseCommandHandler(ICourseRepository courseRepository,
                                IMediator mediator) : IRequestHandler<AddCourseCommand, bool>
    {
        public async Task<bool> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            if (!ValidatComand(request)) return false;

            var course = new Course
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            courseRepository.Add(course);

            return await courseRepository.UnitOfWork.Commit();
        }

        private bool ValidatComand(Command command)
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
