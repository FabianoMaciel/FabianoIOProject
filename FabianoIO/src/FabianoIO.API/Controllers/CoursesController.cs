using FabianoIO.API.ViewModel;
using FabianoIO.Core.Interfaces.Services;
using FabianoIO.ManagementCourses.Aplication.Commands;
using FabianoIO.ManagementCourses.Application.Commands;
using FabianoIO.ManagementCourses.Application.Queries;
using FabianoIO.ManagementCourses.Application.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducacao.Api.DTOs;
using System.Net;

namespace FabianoIO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController(IMediator _mediator,
                                ICourseQuery courseQuery,
                                INotifier notifier) : MainController(notifier)
    {

        [AllowAnonymous]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<CourseViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CourseViewModel>>> GetAll()
        {
            var courses = await courseQuery.GetAll();
            return CustomResponse(courses);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CourseViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CourseViewModel>>> GetById(Guid id)
        {
            var course = await courseQuery.GetById(id);
            return CustomResponse(course);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("create")]
        [ProducesResponseType(typeof(CourseViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create(CourseViewModel course)
        {
            var command = new AddCourseCommand(course.Name, course.Description, UserId, course.Price);
            await _mediator.Send(command);

            return CustomResponse(HttpStatusCode.Created);
        }

        [Authorize(Roles = "STUDENT")]
        [HttpPost("{courseId:guid}/make-payment")]
        public async Task<IActionResult> MakePayment(Guid courseId, [FromBody]PaymentViewModel paymentViewModel)
        {
            var command = new ValidatePaymentCourseCommand(courseId, UserId, paymentViewModel.CardName,
                                                        paymentViewModel.CardNumber, paymentViewModel.CardExpirationDate,
                                                        paymentViewModel.CardCVV);
            await _mediator.Send(command);

            return CustomResponse(HttpStatusCode.Created);
        }
    }
}