using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FabianoIO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<PostModel>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("add-lesson")]
        public IActionResult Add()
        {
            return Ok();
        }

    }
}
