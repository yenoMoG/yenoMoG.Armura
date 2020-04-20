using FluentAssertions;
using System;
using System.Collections;
using System.Collections.Generic;
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
        private readonly DateTime BIRTH_DATE = new DateTime(2000, 01, 01);
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_ReturnFailWhenPasswordNotEmpty(string password)
        {
            var user = User.Create(CPF, NAME, NICKNAME, EMAIL, password, BIRTH_DATE, GENDER);
            user.Should().NotBeNull();
            user.IsFailure.Should().BeTrue();
            user.Messages.Keys.Should().Contain("PasswordIsNotNullOrEmpty");
            user.Messages.Should().ContainValues("A senha deve ser preenchida");
        }

        [Theory]
        [InlineData("!QAZXSW@")]
        [InlineData("123456")]
        [InlineData("abcdef")]
        [InlineData("@WSXvfr$")]
        [InlineData("1qazxsw2")]
        [InlineData("1QAZXSW2")]
        public void Should_ReturnFailWhenPasswordInvalid(string password)
        {
            var user = User.Create(CPF, NAME, NICKNAME, EMAIL, password, BIRTH_DATE, GENDER);
            user.Should().NotBeNull();
            user.IsFailure.Should().BeTrue();
            user.Messages.Keys.Should().Contain("PasswordInvalid");
            user.Messages.Should().ContainValues("Senha deverá conter no mínimo uma letra minúscula, uma maiúscula, um número, um caractere especial e com o comprimento mínimo de oito caracteres.");
        }

        [Theory]
        [ClassData(typeof(BirthDateFake))]
        public void Should_ReturnFailWhenBirthDateInvalid(DateTime birthDate)
        {
            var user = User.Create(CPF, NAME, NICKNAME, EMAIL, PASSWORD, birthDate, GENDER);
            user.Should().NotBeNull();
            user.IsFailure.Should().BeTrue();
            user.Messages.Should().ContainValues("Você deve ser maior de idade");
        }

        [Theory]
        [InlineData(' ')]
        [InlineData('k')]
        public void Should_ReturnFailWhenGenderNotEmpty(char gender)
        {
            var user = User.Create(CPF, NAME, NICKNAME, EMAIL, PASSWORD, BIRTH_DATE, gender);
            user.Should().NotBeNull();
            user.IsFailure.Should().BeTrue();
            user.Messages.Should().ContainValues("Você deve informar um genêro que está na lista");
        }

        private class BirthDateFake : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { DateTime.Now };
                yield return new object[] { DateTime.Today.AddYears(-17).AddDays(-364) };
                yield return new object[] { DateTime.Today.AddDays(1) };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
