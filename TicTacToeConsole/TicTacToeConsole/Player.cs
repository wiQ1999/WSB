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
				_sName = string.Empty;
				Console.ResetColor();
				Console.Clear();

				Console.WriteLine("GRACZ " + a_Character);
				Console.Write("Podaj nazwę: ");
				Console.ForegroundColor = ConsoleColor.Green;
			} while ((_sName = Console.ReadLine()).Contains('.') || _sName == string.Empty);

			Console.ResetColor();
			Name = _sName;
			Character = a_Character;
		}

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
