using System;
using MediatR;
using yenoMoG.Armura.Domain.Responses;

namespace yenoMoG.Armura.Application.Command.Commands
{
	public class CreateUserCommand : IRequest<Response>
	{
		public CreateUserCommand
			(
				string cpf, 
				string name, 
				string nickname, 
				string email, 
				string password, 
				DateTime birthDate, 
				char gender
			)
		{
			CPF = cpf;
			Name = name;
			Nickname = nickname;
			Email = email;
			Password = password;
			BirthDate = birthDate;
			Gender = gender;
		}

		public string CPF { get; set; }
		public string Name { get; set; }
		public string Nickname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime BirthDate { get; set; }
		public char Gender { get; set; }
	}
}
