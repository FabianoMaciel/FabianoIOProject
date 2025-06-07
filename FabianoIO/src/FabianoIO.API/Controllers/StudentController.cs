using FabianoIO.Core.Interfaces.Services;
using FabianoIO.ManagementCourses.Application.Commands;
using FabianoIO.ManagementCourses.Application.Queries;
using FabianoIO.ManagementPayments.Application.Query;
using FabianoIO.ManagementStudents.Aplication.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FabianoIO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(IMediator _mediator,
                                ICourseQuery courseQuery,
                                IPaymentQuery paymentQuery,
                                INotifier notifier) : MainController(notifier)
    {
        [Authorize(Roles = "STUDENT")]
        [HttpPost("register-to-course/{courseId:guid}")]
        public async Task<IActionResult> RegisterToCourse(Guid courseId)
        {
            var course = await courseQuery.GetById(courseId);
            if (course == null)
                return NotFound("Curso não encontrado.");

            var paymentExists = await paymentQuery.PaymentExists(UserId, courseId);
            if (!paymentExists)
                return UnprocessableEntity("Você não possui acesso a esse curso.");

            var commandRegistration = new AddRegistrationCommand(UserId, courseId);
            await _mediator.Send(commandRegistration);

            var commandCreationProgress = new CreateProgressByCourseCommand(courseId, UserId);
            await _mediator.Send(commandCreationProgress);

            return CustomResponse(HttpStatusCode.Created);
        }
    }
}
