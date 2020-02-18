using yenoMoG.Armura.Application.Command.Commands;
using yenoMoG.Armura.Domain.Validators;

namespace yenoMoG.Armura.Application.Command.Handlers
{
	class CreateUserCommandHandler
	{

		private readonly CreateUserCommandValidator _validator;

		public CreateUserCommandHandler
			(

			)
		{
			_validator = new CreateUserCommandValidator();
		}

		private bool ValidateCommand(CreateUserCommand request)
		{
			var validationResponse = _validator.Validate(request);

			if (validationResponse.IsValid) 
				return true;
			return false;
		} 
	}
}
