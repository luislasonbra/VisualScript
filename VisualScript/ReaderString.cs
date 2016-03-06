/*
 * Date:
 * dd/mm/yyyy
 * 06/03/2015
 * 
 * ReaderString v2.0
 * Developed by luislasonbra
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualScript
{
	public class ReaderString
	{
		private int _pos;
		private string _s;
		private int _length;

		private int _col;
		private int _line = 1;

		/// <summary>
		/// Inicializa una nueva instancia de la clase 'ReaderString' para la
		/// cadena especificada.
		/// </summary>
		/// <param name="s">Secuencia que se va a leer.</param>
		public ReaderString(string s)
		{
			if (s == null) throw new ArgumentNullException("s");

			_s = s;
			_length = s == null ? 0 : s.Length;
		}

		/// <summary>
		/// Devuelve el siguiente carácter disponible pero no lo consume.
		/// </summary>
		/// <returns></returns>
		public int Peek()
		{
			if (_pos == _length) return -1;

			//
			return _s[_pos];
		}

		/// <summary>
		/// Lee el siguiente carácter del flujo de entrada y hace avanzar la posición
		/// de los caracteres en un carácter.
		/// </summary>
		/// <returns></returns>
		public int Read()
		{
			_col++;
			if (_pos == _length) return -1;
			if (_s[_pos] == '\n')
			{
				_line++;
				_col = 0;
			}

			//
			return _s[_pos++];
		}

		/// <summary>
		/// Lee una línea de caracteres de la secuencia actual y devuelve los datos como
		/// una cadena.
		/// </summary>
		/// <returns></returns>
		public string ReadLine()
		{
			int i = _pos;
			while (i < _length)
			{
				char ch = _s[i];
				if (ch == '\r' || ch == '\n')
				{
					string result = _s.Substring(_pos, i - _pos);
					_pos = i + 1;
                    if (ch == '\r' && _pos < _length && _s[_pos] == '\n') _pos++;

					//
					return result;
				}
				i++;
			}

			if (i > _pos)
			{
				string result = _s.Substring(_pos, i - _pos);
				_pos = i;
				return result;
			}
			return null;
		}

		/*
		 * Propiedades
		*/
		public int Col { get { return _col; } }
		public int Line { get { return _line; } }
	}
}