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

        [AllowAnonymous]
        [HttpPost]
        //[ProducesResponseType(typeof(AuthorModel), StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Post()
        {
            return Ok();
        }

    }
}
