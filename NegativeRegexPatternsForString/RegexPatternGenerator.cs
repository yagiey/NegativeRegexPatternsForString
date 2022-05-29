using System;

namespace NegativeRegexPatternsForString
{
	internal static class RegexPatternGenerator
	{
		/// <summary>
		/// generate a regular expression which does not contain specified word.
		/// </summary>
		/// <param name="bannedWord"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		/// <see cref="https://stackoverflow.com/questions/406230/regular-expression-to-match-a-line-that-doesnt-contain-a-word"/>
		/// <see cref="http://www.formauri.es/personal/pgimeno/misc/non-match-regex/"/>
		public static string GenerateNegativePattern(string bannedWord)
		{
			if (string.IsNullOrEmpty(bannedWord))
			{
				throw new Exception();
			}

			string head = bannedWord[0].ToString();
			string pattern;
			if (bannedWord.Length == 1)
			{
				pattern = string.Format("[^{0}]*", head);
			}
			else
			{
				string part1 = Part1(bannedWord);
				string part2 = Part2(bannedWord);
				string part3 = Part3(bannedWord);
				string part4 = Part4(bannedWord);
				pattern = string.Format("([^{0}]|{0}({1})*({2}))*(({3}){4})?", head, part1, part2, part3, part4);

			}
			return string.Format("^{0}$", pattern);
		}

		private static string Part1(string bannedWord)
		{
			if (bannedWord.Length < 2)
			{
				throw new Exception();
			}

			char head = bannedWord[0];
			string result = head.ToString();
			for (int i = bannedWord.Length - 1; 2 <= i; i--)
			{
				result = string.Format("{0}|{1}({2})", head, bannedWord[i - 1], result);
			}
			return result;
		}

		private static string Part2(string bannedWord)
		{
			if (bannedWord.Length < 2)
			{
				throw new Exception();
			}

			char head = bannedWord[0];
			string result = string.Format("[^{0}{1}]", head, bannedWord[^1]);
			for (int i = bannedWord.Length - 1; 2 <= i; i--)
			{
				result = string.Format("[^{0}{1}]|{1}({2})", head, bannedWord[i - 1], result);
			}
			return result;
		}

		private static string Part3(string bannedWord)
		{
			if (bannedWord.Length < 2)
			{
				throw new Exception();
			}
			else if (bannedWord.Length == 1)
			{
				return string.Empty;
			}

			char head = bannedWord[0];
			string part1 = Part1(bannedWord);
			if (bannedWord.Length == 2)
			{
				return string.Format("{0}*", part1);
			}
			else
			{
				return string.Format("{0}({1})*", head, part1);
			}
		}

		private static string Part4(string bannedWord)
		{
			if (bannedWord.Length < 2)
			{
				throw new Exception();
			}
			else if (bannedWord.Length == 2)
			{
				return string.Empty;
			}

			string result = string.Format("({0})?", bannedWord[^2]);
			for (int i = bannedWord.Length - 1; 3 <= i; i--)
			{
				result = string.Format("({0}{1})?", bannedWord[i - 2], result);
			}
			return result;
		}
	}
}
