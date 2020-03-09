using System.Threading;
using System.Threading.Tasks;
using MediatR;
using yenoMoG.Armura.Application.Command.Commands;
using yenoMoG.Armura.Domain.Models;
using yenoMoG.Armura.Domain.Responses;

namespace yenoMoG.Armura.Application.Command.Handlers
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response>
	{

		public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var user = User.Create(request.CPF, request.Name, request.Nickname, request.Email, request.Password, request.BirthDate, request.Gender);

			if (user.IsFailure)
			{
				return Response.Fail("", "");
			}

			return Response.Ok();
		}
	}
}
