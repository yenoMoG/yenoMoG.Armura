using System;
using System.Collections.Generic;
using System.Linq;
using yenoMoG.Armura.Domain.Responses;
using yenoMoG.Armura.Domain.Validators;

namespace yenoMoG.Armura.Domain.Models
{
	public class User
	{
		public string Cpf { get; }
		public string Name { get; }
		public string Nickname { get; }
		public string Email { get; }
		public string Password { get; }
		public DateTime BirthDate { get; }
		public char Gender { get; }
		private User(string cpf, string name, string nickname, string email, string password, DateTime birthDate, char gender)
		{
			Cpf = cpf;
			Name = name;
			Nickname = nickname;
			Email = email;
			Password = password;
			BirthDate = birthDate;
			Gender = gender;
		}

		public static Response<User> Create(string cpf, string name, string nickname, string email, string password, DateTime birthDate, char gender)
		{
			var user = new User(cpf, name, nickname, email, password, birthDate, gender);

			var validator = new UserValidator().Validate(user);

			if (validator.IsValid)
				return Response<User>.Ok(user);

			var fails = new Dictionary<string, string>();

			validator.Errors.ToList().ForEach(e => fails.Add(e.ErrorCode, e.ErrorMessage));
			
			return Response<User>.Fail(fails);
		}
	}
}