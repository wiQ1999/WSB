namespace TicTacToeConsole.Interfaces
{
	public interface ICharacter
	{
		Character Character { get; }

		char GetCharacterChar();

		int GetCharacterValue();
	}
}
