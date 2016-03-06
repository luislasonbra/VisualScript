using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript
{
	public static class Utils
	{
		public static bool isWhitespace(char ch) { return (ch == ' ') || (ch == '\t') || (ch == '\n') || (ch == '\r'); }

		public static bool isNumeric(char ch) { return (ch >= '0') && (ch <= '9'); }

		public static bool isNumber(string str)
		{
			for (int i = 0; i < str.Length; i++)
				if (!isNumeric(str[i])) return false;

			//
			return true;
		}

		public static bool isHexadecimal(char ch)
		{
			return ((ch >= '0') && (ch <= '9')) ||
					((ch >= 'a') && (ch <= 'f')) ||
					((ch >= 'A') && (ch <= 'F'));
		}

		public static bool isAlpha(char ch)
		{
			return ((ch >= 'a') && (ch <= 'z')) || ((ch >= 'A') && (ch <= 'Z')) || ch == '_';
		}

		public static bool isIDString(char s)
		{
			if (!isAlpha(s)) return false;
			while (s != 0)
			{
				if (!(isAlpha(s) || isNumeric(s))) return false;
				s++;
			}
			//
			return true;
		}

		/// <summary>
		/// Is the string alphanumeric
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool isAlphaNum(string str)
		{
			if (str.Length == 0) return true;
			if (!isAlpha(str[0])) return false;
			for (int i = 0; i < str.Length; i++)
				if (!(isAlpha(str[i]) || isNumeric(str[i])))
					return false;
			//
			return true;
		}

		public static string replace(string str, char textFrom, char textTo)
		{
			return str.Replace(textFrom, textTo);
		}

		public static string replace(string str, string textFrom, string textTo)
		{
			return str.Replace(textFrom, textTo);
		}
	}
}
