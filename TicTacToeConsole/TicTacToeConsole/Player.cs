using System;
using TicTacToeConsole.Interfaces;

namespace TicTacToeConsole
{
	class Player : ICharacter
	{
		public string Name { get; }
		public Character Character { get; }

		public Player(Character a_Character)
		{
			string _sName;
			do
			{
				Console.ResetColor();
				Console.Clear();

				Console.WriteLine("GRACZ " + a_Character);
				Console.Write("Podaj nazwę: ");
				Console.ForegroundColor = ConsoleColor.Green;
			} while ((_sName = Console.ReadLine()).Contains('.') || _sName.Contains(' ') || _sName == string.Empty);

			Console.ResetColor();
			Name = _sName;
			Character = a_Character;
		}

		/// <summary>
		/// Get char of character
		/// </summary>
		/// <returns>A char</returns>
		public char GetCharacterChar()
		{
			switch (Character)
			{
				case Character.KOLKO:
					return 'O';
				case Character.KRZYZYK:
					return 'X';
				default:
					return ' ';
			}
		}

		/// <summary>
		/// Get value of character
		/// </summary>
		/// <returns>A value</returns>
		public int GetCharacterValue()
		{
			switch (Character)
			{
				case Character.KOLKO:
					return 1;
				case Character.KRZYZYK:
					return -1;
				default:
					return 0;
			}
		}
	}
}
