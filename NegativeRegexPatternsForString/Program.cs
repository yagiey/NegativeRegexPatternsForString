using System;
using System.Text.RegularExpressions;

namespace NegativeRegexPatternsForString
{
	internal class Program
	{
		public static void Main()
		{
			string text1 = @"I visited Scunthorpe, UK last month. It was a beautiful place.";
			Post(text1);

			string text2 = @"I visited Fukui, Japan last month. It was a beautiful place.";
			Post(text2);
		}

		public static void Post(string text)
		{
			// unbelievably stupid censorship
			string pattern = RegexPatternGenerator.GenerateNegativePattern("cunt");
			Regex regex = new(pattern);
			bool ok = regex.IsMatch(text);

			if(!ok)
			{
				Console.WriteLine(@"failed to post because of banned word/words.");
				return;
			}

			bool result = DoPost(text);
			if (result)
			{
				Console.WriteLine(@"posted successfully.");
			}
			else
			{
				Console.WriteLine(@"failed to post.");
			}
		}

		private static bool DoPost(string text)
		{
			Console.WriteLine(@"now posting... ""{0}""", text);
			return true;
		}
	}
}
