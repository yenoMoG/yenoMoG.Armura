using System;

namespace yenoMoG.Armura.API.Requests
{
	public class UserRequest
	{
		public string CPF { get; set; }
		public string Name { get; set; }
		public string Nickname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime BirthDate { get; set; }
		public char Gender { get; set; }
	}
}
