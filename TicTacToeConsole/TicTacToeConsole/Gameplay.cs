using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeConsole.Interfaces;

namespace TicTacToeConsole
{
	class Gameplay : Board, IRules
	{
		public int CurrentPlayer { get; set; }

		public Gameplay(Player a_PlayerKOLKO, Player a_PlayerKRZYZYK) : base(a_PlayerKOLKO, a_PlayerKRZYZYK)
		{
			CurrentPlayer = a_PlayerKOLKO.GetCharacterValue();
		}

		public void StartGame()
		{
			bool _bQuitGame = false;
			while (!_bQuitGame)
			{
				Console.Clear();
				DrawPlayBoard();
				while (!CheckResult())
				{

					InsertCharactersInfo();


					//insert graczy wraz z mozliwoscia wyjscia
				}
			}
		}

		public bool CheckResult()
		{
			for (int y = 0; y < 3; y++)
			{
				int _iSum1 = 0;
				int _iSum2 = 0;

				for (int x = 0; x < 3; x++)
				{
					_iSum1 += CharactersArray[x, y];
					_iSum2 += CharactersArray[y, x];
				}

				if (_iSum1 == CurrentPlayer * 3)
					return true;

				if (_iSum2 == CurrentPlayer * 3)
					return true;
			}

			if (CharactersArray[0, 0] + CharactersArray[1, 1] + CharactersArray[2, 2] == CurrentPlayer * 3 ||
				CharactersArray[2, 0] + CharactersArray[1, 1] + CharactersArray[0, 2] == CurrentPlayer * 3)
				return true;

			return false;
		}

		private BoardPlace SelectPlaceForText(string a_sText)
		{
			//napisać obliczanie położenia tekstu po jego długości

			return new BoardPlace { };
		}

		public void InsertCharactersInfo()
		{
			Console.SetCursorPosition(0, 0);
			Console.WriteLine("KOLKO KRZYZYK");
			BoardPlace _Place = SelectPlaceForText(PlayerKOLKO.Name);
			Console.SetCursorPosition(_Place.X, _Place.Y);
			Console.Write(PlayerKOLKO.Name);
			//ilosc punktów

			//drugi gracz to samo

			//znak - kogo ruch
		}

		public void SwitchPlayers()
		{
			CurrentPlayer *= -1;
		}
	}
}
