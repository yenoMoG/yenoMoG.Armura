using FluentValidation;
using System;
using yenoMoG.Armura.Domain.Models;

namespace yenoMoG.Armura.Domain.Validators
{
    internal class UserValidator : AbstractValidator<User>
    {
        private const int MINIMUM_AGE = 18;
        internal UserValidator()
        {
            RuleFor(u => u.Cpf).Must(cpf => cpf.IsCpfValid()).WithMessage("O CPF é invalido");
            RuleFor(u => u.Name)
                .NotEmpty().WithErrorCode("NameIsNotNullOrEmpty").WithMessage("O nome deve ser preenchido")
                .Matches(@"^((\b[A-zÀ-ú']{2,40}\b)\s*){2,}$").WithErrorCode("NameRegexInvalid").WithMessage("Nome deve ser completo");
            RuleFor(u => u.Nickname).NotEmpty().WithMessage("Apelido não pode ser vazio");
            RuleFor(u => u.Email).Must(email => email.IsEmailValid()).WithMessage("Precisa conter email válido");
            RuleFor(u => u.Password).NotEmpty().WithErrorCode("PasswordIsNotNullOrEmpty").WithMessage("A senha deve ser preenchida")
                .Matches(@"(?=^.{8,}$)((?=.*\d)(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$").WithErrorCode("PasswordInvalid").WithMessage("Senha deverá conter no mínimo uma letra minúscula, uma maiúscula, um número, um caractere especial e com o comprimento mínimo de oito caracteres.");
            RuleFor(u => u.BirthDate).LessThan(DateTime.Today.AddYears(-MINIMUM_AGE)).WithMessage("Você deve ser maior de idade");
            RuleFor(u => char.ToUpper(u.Gender))
                .Must(g => g.Equals('F') || g.Equals('M') || g.Equals('O')).WithMessage("Você deve informar um genêro que está na lista");
        }
    }
}
