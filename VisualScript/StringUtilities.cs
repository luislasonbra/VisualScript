using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VisualScript
{
	public class StringUtilities
	{
		private static Regex rex = new Regex(@"\$\{[^\}]*\}");

		public static List<string> SplitText(string text)
		{
			int lastindex = 0;
			List<string> parts = new List<string>();

			for (Match match = rex.Match(text); match.Success; match = match.NextMatch())
			{
				parts.Add(text.Substring(lastindex, match.Index - lastindex));
				parts.Add(match.ToString().Substring(2, match.Length - 3));
				lastindex = match.Index + match.Length;
			}

			if (lastindex < text.Length) parts.Add(text.Substring(lastindex, text.Length - lastindex));
			else if (parts.Count == 0) parts.Add(text);

			return parts;
		}

		public static List<object> Obj_SplitText(string str)
		{
			List<object> items = new List<object>();
			for (int i = 0; i < str.Length; i++) items.Add(str[i]);

			return items;
		}

		public static List<object> Obj_SplitText(string str, string split)
		{
			string[] SplitSrt = new string[] { split };
			string[] strArray = str.Split(SplitSrt, StringSplitOptions.None);

			List<object> items = new List<object>();
			for (int i = 0; i < strArray.Length; i++) items.Add(strArray[i]);

			return items;
		}

		public static string removeLastChar(string srt)
		{
			int srtLen = srt.Length;
			return srt.Substring(0, srtLen - 1);
		}
	}
}
