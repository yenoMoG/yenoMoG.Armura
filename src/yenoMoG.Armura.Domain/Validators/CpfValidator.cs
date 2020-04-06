using System;

namespace yenoMoG.Armura.Domain.Validators
{
	public static class CpfValidator
	{
		public static bool IsCpfValid(this string cpf)
		{
			try
            {
                cpf = cpf.Replace(".", "").Replace("-", "").Trim();
                if (cpf.Length != 11)
                    return false;

                var tempCpf = cpf.Substring(0, 9);

                var mod = ModCalculate(tempCpf, new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 });

                var digit = mod;
                tempCpf = $"{tempCpf}{digit}";

                mod = ModCalculate(tempCpf, new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 });

                return cpf.EndsWith($"{digit}{mod}");
            }
			catch (Exception)
            {
                return false;
            }
		}

		private static int ModCalculate(string tempCpf, int[] multiplier)
		{
			var sum = 0;
			for (var i = 0; i < 9; i++)
				sum += int.Parse(tempCpf[i].ToString()) * multiplier[i];
			var mod = sum % 11;
			mod = mod < 2 ? 0 : 11 - mod;
			return mod;
		}
	}
}