using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using yenoMoG.Armura.API.Requests;
using yenoMoG.Armura.Application.Command.Commands;

namespace yenoMoG.Armura.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : Controller
	{
		private readonly IMediator _mediator;

		public UserController
			(
			IMediator mediator
			)
		{
			_mediator = mediator;
		}

		[HttpPost("/create")]
		public async Task<IActionResult> Create([FromBody] UserRequest request)
		{
			var command = new CreateUserCommand
				(
					request.CPF,
					request.Name,
					request.Nickname,
					request.Email,
					request.Password,
					request.BirthDate,
					request.Gender
				);

			var response = await _mediator.Send(command);

			if (response.IsFailure)
			{
				return BadRequest(response);
			}

			return Ok();
		}
	}
}