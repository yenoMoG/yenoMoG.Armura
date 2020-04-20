using System;
using FluentAssertions;
using Xunit;
using yenoMoG.Armura.Domain.Models;

namespace yenoMoG.Armura.Test.Domain.Models
{
	public class UserTest
	{
		private const string CPF = "123.456.789-09";
		private const string NAME = "João Pedro Alves";
		private const string NICKNAME = "JP zAlvs";
		private const string EMAIL = "joaopedro@gmail.com";
		private const string PASSWORD = "Homolog@145";
		private readonly DateTime BIRTH_DATE = new DateTime(2000,01,01);
		private const char GENDER = 'M';

		[Fact]
		public void Should_ReturnValidUser()
		{
			var user = User.Create(CPF, NAME, NICKNAME, EMAIL, PASSWORD, BIRTH_DATE, GENDER);

			user.Should().NotBeNull();
			user.IsSuccess.Should().BeTrue();
			user.Value.Should().NotBeNull();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("12345")]
		[InlineData("76567656787")]
        [InlineData("788736qf856")]
        [InlineData("788736578564359")]
		[InlineData("456.545.675-64")]
		public void Should_ReturnFailWhenCpfInvalid(string cpf)
		{
			var user = User.Create(cpf, NAME, NICKNAME, EMAIL, PASSWORD, BIRTH_DATE, GENDER);
			user.Should().NotBeNull();
			user.IsFailure.Should().BeTrue();
			user.Messages.Should().ContainValues("O CPF é invalido");
		}

		[Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
		public void Should_ReturnFailWhenNameNotEmpty(string name)
        {
            var user = User.Create(CPF, name, NICKNAME, EMAIL, PASSWORD, BIRTH_DATE, GENDER);
            user.Should().NotBeNull();
            user.IsFailure.Should().BeTrue();
            user.Messages.Keys.Should().Contain("NameIsNotNullOrEmpty");
            user.Messages.Should().ContainValues("O nome deve ser preenchido");
        }

        [Theory]
        [InlineData("José")]
		[InlineData("José S")]
		[InlineData("José Da Siva Pereira R")]
		[InlineData("Mariana")]
        public void Should_ReturnFailWhenNameRegexInvalid(string name)
        {
            var user = User.Create(CPF, name, NICKNAME, EMAIL, PASSWORD, BIRTH_DATE, GENDER);
            user.Should().NotBeNull();
            user.IsFailure.Should().BeTrue();
            user.Messages.Keys.Should().Contain("NameRegexInvalid");
            user.Messages.Should().ContainValues("Nome deve ser completo");
        }

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Should_ReturnFailWhenNicknameInvalid(string nickname)
        {
            var user = User.Create(CPF, NAME, nickname, EMAIL, PASSWORD, BIRTH_DATE, GENDER);
            user.Should().NotBeNull();
            user.IsFailure.Should().BeTrue();
            user.Messages.Should().ContainValues("Apelido não pode ser vazio");
        }

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("r.c.e@.e#.com")]
		[InlineData("-@a@glob.com")]
		[InlineData("( ͡° ͜ʖ ͡°)@mailinator.com.br")]
		public void Should_ReturnFailWhenEmailInvalid(string email)
        {
            var user = User.Create(CPF, NAME, NICKNAME, email, PASSWORD, BIRTH_DATE, GENDER);
            user.Should().NotBeNull();
            user.IsFailure.Should().BeTrue();
            user.Messages.Should().ContainValues("Precisa conter email válido");
        }
    }
}
