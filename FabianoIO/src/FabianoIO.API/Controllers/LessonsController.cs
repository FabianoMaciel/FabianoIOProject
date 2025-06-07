using FabianoIO.Core.Interfaces.Services;
using FabianoIO.ManagementCourses.Application.Commands;
using FabianoIO.ManagementCourses.Application.Queries;
using FabianoIO.ManagementCourses.Application.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

//TO DO
//Fazer aula deve checar se a aula esta dentro de um curso ja pago pelo student
//se nao foi pago ainda retornar a exception com a mensagem
//ai o aluno pode fazer a aula caso ja tenha o pagamento para um curso que tem a aula dentro dela
//Concluir aula, novamente fazer o check, e ver se o aluno estava ja fazendo essa aula, se ja estava
//entao concluir aula vai ser valido


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
        public async Task<IActionResult> StartClass(Guid lessonId)
        {
            var command = new StartLessonCommand(lessonId, UserId);
            await _mediator.Send(command);

            return CustomResponse(HttpStatusCode.Created); ;
        }

        [Authorize(Roles = "STUDENT")]
        [HttpPost("{lessonId:guid}/finish-class")]
        [ProducesResponseType(typeof(CourseViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> FinishClass(Guid lessonId)
        {
            var command = new FinishLessonCommand(lessonId, UserId);
            await _mediator.Send(command);

            return CustomResponse(HttpStatusCode.Created); ;
        }
    }
}
