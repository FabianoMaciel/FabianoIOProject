using FabianoIO.API.ViewModel;
using FabianoIO.Core.Interfaces.Services;
using FabianoIO.ManagementCourses.Application.Commands;
using FabianoIO.ManagementCourses.Application.Queries;
using FabianoIO.ManagementCourses.Application.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("getall")]
        [ProducesResponseType(typeof(IEnumerable<CourseViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CourseViewModel>>> GetAll()
        {
            var courses = await courseQuery.GetAll();
            return CustomResponse(courses);
        }

        [AllowAnonymous]
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
    }
}