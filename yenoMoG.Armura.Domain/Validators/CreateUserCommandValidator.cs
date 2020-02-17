using FluentValidation;
using yenoMoG.Armura.Domain.Models;

namespace yenoMoG.Armura.Domain.Validators
{
	class CreateUserCommandValidator : AbstractValidator<User>
	{
		public CreateUserCommandValidator()
		{
			RuleFor(u => u.CPF).NotEmpty();
			RuleFor(u => u.Name.Length).GreaterThanOrEqualTo(5);
			RuleFor(u => u.Name).NotEmpty().Matches(@"^((\b[A-zÀ-ú']{2,40}\b)\s*){2,}$");
			RuleFor(u => u.Nickname).NotEmpty();
		}
	}
}
