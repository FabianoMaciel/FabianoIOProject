using FabianoIO.Core.Enums;
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
    public class LessonsController(IMediator _mediator,
                                ILessonQuery lessonQuery,
                                INotifier notifier) : MainController(notifier)
    {
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LessonViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LessonViewModel>>> GetAll()
        {
            var lessons = await lessonQuery.GetAll();
            return CustomResponse(lessons);
        }

        [AllowAnonymous]
        [HttpGet("get-by-courseId")]
        [ProducesResponseType(typeof(IEnumerable<LessonViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LessonViewModel>>> GetByCourseId([FromQuery] Guid courseId)
        {
            var lessons = await lessonQuery.GetByCourseId(courseId);
            return CustomResponse(lessons);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
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

        [Authorize(Roles = "STUDENT")]
        [HttpPost("{lessonId:guid}/start-class")]
        [ProducesResponseType(typeof(CourseViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> StartClass(Guid lessonId)
        {
            if (!lessonQuery.ExistsProgress(lessonId, UserId))
                return NotFound("Você ainda não está matriculado a essa aula.");

            var status = lessonQuery.GetProgressStatusLesson(lessonId, UserId);

            if (status == EProgressLesson.Completed)
                return NotFound("Você já concluiu essa aula.");

            var command = new StartLessonCommand(lessonId, UserId);
            await _mediator.Send(command);

            return CustomResponse(HttpStatusCode.NoContent); ;
        }

        [Authorize(Roles = "STUDENT")]
        [HttpPost("{lessonId:guid}/finish-class")]
        [ProducesResponseType(typeof(CourseViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FinishClass(Guid lessonId)
        {
            if (!lessonQuery.ExistsProgress(lessonId, UserId))
                return NotFound("Você ainda não está matriculado a essa aula.");

            var status = lessonQuery.GetProgressStatusLesson(lessonId, UserId);

            if (status == EProgressLesson.NotStarted)
                return NotFound("Você ainda não teve progresso nesta aula.");

            var command = new FinishLessonCommand(lessonId, UserId);
            await _mediator.Send(command);

            return CustomResponse(HttpStatusCode.NoContent); ;
        }
    }
}
