using System;
using System.Net.Mail;

namespace yenoMoG.Armura.Domain.Validators
{
	public static class EmailValidator
	{
		public static bool IsEmailValid(this string emailAddress)
		{
			try
			{
				var email = new MailAddress(emailAddress);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
