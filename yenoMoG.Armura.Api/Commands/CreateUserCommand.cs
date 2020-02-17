using MediatR;
using System;

namespace yenoMoG.Armura.Api.Commands
{
    public class CreateUserCommand : IRequest
    {
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string BirthDate { get; set; }
        public char Gender { get; set; }
    }
}
