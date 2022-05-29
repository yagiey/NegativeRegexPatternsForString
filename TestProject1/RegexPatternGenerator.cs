using NegativeRegexPatternsForString;
using System.Text;
using System.Text.RegularExpressions;

namespace TestProject1
{
	public class RegexPatternGenerator
	{
		private static void TestNeg(Regex regex, string bannedWord)
		{
			Console.WriteLine("==================== NG-word: {0} ====================", bannedWord);

			var alphabets = Enumerable.Range(0, bannedWord.Length + 1).Select(it => Convert.ToChar('a' + it));
			StringBuilder sb = new();
			int nCases = 0;

			foreach (var item in DirectProductEnumerator.DP(alphabets, alphabets))
			{
				sb.Clear();
				sb.Append(item.Item1);
				sb.Append(item.Item2);

				string input = sb.ToString();
				TestNeg(input, regex, bannedWord);
				nCases++;
			}
			Console.WriteLine("---------- any 2-length word: {0} cases", nCases);

			nCases = 0;
			foreach (var item in DirectProductEnumerator.DP(alphabets, alphabets, alphabets))
			{
				sb.Clear();
				sb.Append(item.Item1);
				sb.Append(item.Item2);
				sb.Append(item.Item3);

				string input = sb.ToString();
				TestNeg(input, regex, bannedWord);
				nCases++;
			}
			Console.WriteLine("---------- any 3-length word: {0} cases", nCases);

			nCases = 0;
			foreach (var item in DirectProductEnumerator.DP(alphabets, alphabets, alphabets, alphabets))
			{
				sb.Clear();
				sb.Append(item.Item1);
				sb.Append(item.Item2);
				sb.Append(item.Item3);
				sb.Append(item.Item4);

				string input = sb.ToString();
				TestNeg(input, regex, bannedWord);
				nCases++;
			}
			Console.WriteLine("---------- any 4-length word: {0} cases", nCases);

			nCases = 0;
			foreach (var item in DirectProductEnumerator.DP(alphabets, alphabets, alphabets, alphabets, alphabets))
			{
				sb.Clear();
				sb.Append(item.Item1);
				sb.Append(item.Item2);
				sb.Append(item.Item3);
				sb.Append(item.Item4);
				sb.Append(item.Item5);

				string input = sb.ToString();
				TestNeg(input, regex, bannedWord);
				nCases++;
			}
			Console.WriteLine("---------- any 5-length word: {0} cases", nCases);

			nCases = 0;
			foreach (var item in DirectProductEnumerator.DP(alphabets, alphabets, alphabets, alphabets, alphabets, alphabets))
			{
				sb.Clear();
				sb.Append(item.Item1);
				sb.Append(item.Item2);
				sb.Append(item.Item3);
				sb.Append(item.Item4);
				sb.Append(item.Item5);
				sb.Append(item.Item6);

				string input = sb.ToString();
				TestNeg(input, regex, bannedWord);
				nCases++;
			}
			Console.WriteLine("---------- any 6-length word: {0} cases", nCases);
		}

		private static void TestNeg(string input, Regex regex, string bannedWord)
		{
			bool actual = regex.IsMatch(input);
			bool expected = !input.Contains(bannedWord);

			if (actual != expected)
			{
				string strActual = "was " + (actual ? "accepted" : "not accepted");
				string strExpected = expected ? "does not contain" : "contains";
				string errMsg = string.Format("Assertion failure! '{0}' {1} but it {2} '{3}'. input: {4}, regex: {5}, banned word: {6}", input, strActual, strExpected, bannedWord, input, regex.ToString(), bannedWord);
				throw new Exception(errMsg);
			}
		}

		private static string CreateBannedWord(int length)
		{
			char[] array =
				Enumerable.Range(0, length)
				.Select(it => Convert.ToChar('a' + it))
				.ToArray();
			return new string(array);
		}

		[Fact]
		public void GenerateNegativePattern()
		{
			const int MaxLengthBannedWord = 5;
			foreach (var length in Enumerable.Range(1, MaxLengthBannedWord))
			{
				string bannedWord = CreateBannedWord(length);
				string pattern = NegativeRegexPatternsForString.RegexPatternGenerator.GenerateNegativePattern(bannedWord);
				Regex regex = new(pattern);
				TestNeg(regex, bannedWord);
			}
		}
	}
}