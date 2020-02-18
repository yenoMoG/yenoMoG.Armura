using System.Threading;
using System.Threading.Tasks;
using MediatR;
using yenoMoG.Armura.Application.Command.Commands;
using yenoMoG.Armura.Domain.Responses;
using yenoMoG.Armura.Domain.Validators;

namespace yenoMoG.Armura.Application.Command.Handlers
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response>
	{

		private readonly CreateUserCommandValidator _validator;

		public CreateUserCommandHandler
			(
			)
		{
			_validator = new CreateUserCommandValidator();
		}

		public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var validation = ValidateCommand(request);

			if (validation.IsFailure)
			{
				return Response.Fail("", "");
			}

			return Response.Ok();
		}

		private Response ValidateCommand(CreateUserCommand request)
		{
			var validationResponse = _validator.Validate(request);

			if (validationResponse.IsValid)
				return Response.Ok();
			return Response.Fail("","Dados inválidos");
		}
	}
}
