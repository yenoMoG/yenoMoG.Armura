using System;
using FluentValidation;
using yenoMoG.Armura.Domain.Models;

namespace yenoMoG.Armura.Domain.Validators
{
    internal class UserValidator : AbstractValidator<User>
	{
		private const int MINIMUM_AGE = 18;
		internal UserValidator()
		{
            RuleFor(u => u.Cpf).Must(cpf => cpf.IsCpfValid()).WithMessage("O CPF é invalido");
			RuleFor(u => u.Name.Length).GreaterThanOrEqualTo(5).WithMessage("Nome invalido");
			RuleFor(u => u.Name).NotEmpty().Matches(@"^((\b[A-zÀ-ú']{2,40}\b)\s*){2,}$").WithMessage("Nome invalido");
			RuleFor(u => u.Nickname).NotEmpty().WithMessage("Apelido não pode ser vazio");
			RuleFor(u => u.Email).Must(email => email.IsEmailValid()).WithMessage("Precisa conter email válido");
			RuleFor(u => u.Password.Length).GreaterThanOrEqualTo(8).LessThanOrEqualTo(16).WithMessage("Senha precisa ser maior que 8 e menor que 16");
			RuleFor(u => u.Password).NotEmpty().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})").WithMessage("Senha precisa ser válida");
			RuleFor(u => u.BirthDate)
				.NotEmpty().WithMessage("Aniversário não pode ser vazio")
				.LessThan(DateTime.Today.AddYears(-MINIMUM_AGE)).WithMessage("Aniversário precisa ser valido");
			RuleFor(u => char.ToUpper(u.Gender))
				.NotEmpty().WithMessage("Gênero não pode ser vazio")
				.Must(g => g.Equals('F') || g.Equals('M') || g.Equals('O'));
		}
    }
}
