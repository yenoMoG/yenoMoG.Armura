using FluentValidation;
using yenoMoG.Armura.Application.Command.Commands;

namespace yenoMoG.Armura.Domain.Validators
{
	class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
	{
		public CreateUserCommandValidator()
		{
			RuleFor(u => u.CPF).NotEmpty().Matches(@"/^\d{3}\.\d{3}\.\d{3}\-\d{2}$/").WithMessage("Cpf precisar ser válido");
			RuleFor(u => u.Name.Length).GreaterThanOrEqualTo(5).WithMessage("Nome precisa conter mais de 5 caracteres");
			RuleFor(u => u.Name).NotEmpty().Matches(@"^((\b[A-zÀ-ú']{2,40}\b)\s*){2,}$").WithMessage("Nome precisa estar completo");
			RuleFor(u => u.Nickname).NotEmpty().WithMessage("Apelido não pode ser vazio");
			RuleFor(u => u.Email).NotEmpty().Matches(@"/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/").WithMessage("Precisa conter email válido");
			RuleFor(u => u.Password.Length).GreaterThanOrEqualTo(8).LessThanOrEqualTo(16).WithMessage("Senha precisa ser maior que 8 e menor que 16");
			RuleFor(u => u.Password).NotEmpty().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})").WithMessage("Senha precisa ser válida");
			RuleFor(u => u.BirthDate).NotEmpty().WithMessage("Aniversário não pode ser vázio");
			RuleFor(u => u.Gender).NotEmpty().WithMessage("Gênero não pode ser vázio");
		}
	}
}
