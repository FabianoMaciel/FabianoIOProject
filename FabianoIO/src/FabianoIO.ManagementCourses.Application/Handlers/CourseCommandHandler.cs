﻿using FabianoIO.Core.DomainObjects.DTOs;
using FabianoIO.Core.Interfaces.Repositories;
using FabianoIO.Core.Messages;
using FabianoIO.Core.Messages.IntegrationCommands;
using FabianoIO.Core.Messages.Notifications;
using FabianoIO.ManagementCourses.Aplication.Commands;
using FabianoIO.ManagementCourses.Application.Commands;
using FabianoIO.ManagementCourses.Domain;
using MediatR;

namespace FabianoIO.ManagementCourses.Application.Handlers
{
    public class CourseCommandHandler(ICourseRepository courseRepository,
                                ILessonRepository lessonRepository,
                                IMediator mediator) : IRequestHandler<AddCourseCommand, bool>,
                                                      IRequestHandler<ValidatePaymentCourseCommand, bool>,
                                                      IRequestHandler<CreateProgressByCourseCommand, bool>
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

        public async Task<bool> Handle(ValidatePaymentCourseCommand command, CancellationToken cancellationToken)
        {
            if (!ValidatComand(command))
                return false;

            //TO DO, move to controller e passar o price no command

            var course = await courseRepository.GetById(command.CourseId);
            if (course == null)
            {
                await mediator.Publish(new DomainNotification(command.MessageType, "Curso não encontrado."), cancellationToken);
                return false;
            }

            var paymentCourse = new PaymentCourse
            {
                StudentId = command.StudentId,
                CourseId = course.Id,
                CardCVV = command.CardCVV,
                CardExpirationDate = command.CardExpirationDate,
                CardName = command.CardName,
                CardNumber = command.CardNumber,
                Total = course.Price
            };

            return await mediator.Send(new MakePaymentCourseCommand(paymentCourse), cancellationToken);
        }

        public async Task<bool> Handle(CreateProgressByCourseCommand request, CancellationToken cancellationToken)
        {
            if (!ValidatComand(request)) return false;

            await lessonRepository.CreateProgressLessonByCourse(request.CourseId, request.StudentId);

            return await lessonRepository.UnitOfWork.Commit();
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
