using TicTacToeConsole.Interfaces;

namespace TicTacToeConsole
{
	class Player : ICharacter
	{
		public string Name { get; }
		public Character Character { get; }

		public Player(string a_Name, Character a_Character)
		{
			Name = a_Name;
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
