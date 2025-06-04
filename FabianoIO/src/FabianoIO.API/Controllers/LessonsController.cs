using FabianoIO.Core.Interfaces.Services;
using FabianoIO.ManagementCourses.Application.Commands;
using FabianoIO.ManagementCourses.Application.Queries;
using FabianoIO.ManagementCourses.Application.Queries.ViewModels;
using FabianoIO.ManagementCourses.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FabianoIO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController(IMediator _mediator,
                                ILessonQuery lessonQuery,
                                INotifier notifier) : MainController(notifier)
    {
        [AllowAnonymous]
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(IEnumerable<LessonViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LessonViewModel>>> GetAll()
        {
            var lessons = await lessonQuery.GetAll();
            return CustomResponse(lessons);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("add-lesson")]
        [ProducesResponseType(typeof(CourseViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Add(LessonViewModel course)
        {
            var command = new AddLessonCommand(course.Name, course.Subject, course.CourseId, course.TotalHours);
            await _mediator.Send(command);

            return CustomResponse(HttpStatusCode.Created); ;
        }

        [AllowAnonymous]
        [HttpGet("get-by-courseId")]
        [ProducesResponseType(typeof(IEnumerable<LessonViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LessonViewModel>>> GetByCourseId([FromQuery] Guid courseId)
        {
            var lessons = await lessonQuery.GetByCourseId(courseId);
            return CustomResponse(lessons);
        }

    }
}
