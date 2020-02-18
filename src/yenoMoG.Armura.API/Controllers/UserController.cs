using Microsoft.AspNetCore.Mvc;
using yenoMoG.Armura.API.Requests;

namespace yenoMoG.Armura.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : Controller
	{

		[HttpPost("/create")]
		public IActionResult Create([FromBody] UserRequest request)
		{

			return Ok();
		}

	}
}