using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeConsole.Interfaces;

namespace TicTacToeConsole
{
	class Gameplay : Board, IRules
	{
		private const int PlayerKOLKO_X = 5;
		private const int PlayerKRZYZYK_X = 13;
		private const int Players_Y = Header_Y + 2;

		public Coordinates PlayerKOLKOInfoPlace
        {
			get => new Coordinates { X = PlayerKOLKO_X, Y = Players_Y };
        }

		public Coordinates PlayerKRZYZYKInfoPlace
		{
			get => new Coordinates { X = PlayerKRZYZYK_X, Y = Players_Y };
		}

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
				DrawHeader(new Coordinates { X = 3, Y = Header_Y }, 2, ConsoleColor.Blue);

				//obliczenie wysokości nagłówka
				int _iKOLKOInfoHeight = WritePlayerName(PlayerKOLKO.Name, PlayerKOLKOInfoPlace);
				int _iKRZYZYKInfoHeight = WritePlayerName(PlayerKRZYZYK.Name, PlayerKRZYZYKInfoPlace);

				DrawPlayBoard(new Coordinates { X = 0, Y = Header_Y + 3 + (_iKOLKOInfoHeight > _iKRZYZYKInfoHeight ? _iKOLKOInfoHeight : _iKRZYZYKInfoHeight)});//dziki if?
				while (!CheckResult())
				{

                    InsertCharactersInfo();


					//insert graczy wraz z mozliwoscia wyjscia


					SwitchPlayers();

					Console.ReadKey();//usun
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

		private int WritePlayerName(string a_sName, Coordinates a_PlaceToWrite)
		{
			int _iHeaderHight = 1;

			if(a_sName.Length > 7)//7 - maksymalna szerokość nazwy gracza w hederze
            {
				string _sRestOfName = a_sName;
				do
				{
					bool _bIsEnd = false;
                    string _sTempText;
                    if (_sRestOfName.Length > 7)
						_sTempText = _sRestOfName.Substring(0, 6);
					else
					{
						_sTempText = _sRestOfName;
						_bIsEnd = true;
					}

					_sRestOfName = _sRestOfName.Remove(0, _sTempText.Length);

					Console.SetCursorPosition(a_PlaceToWrite.X - (_sTempText.Length / 2), a_PlaceToWrite.Y + _iHeaderHight - 1);

					Console.Write(_sTempText);
					if (!_bIsEnd)
                    {
						Console.Write("-");
						_iHeaderHight++;
					}
				} while (_sRestOfName.Length > 0);
            }
            else
            {
				Console.SetCursorPosition(a_PlaceToWrite.X - (a_sName.Length / 2), a_PlaceToWrite.Y);
				Console.Write(a_sName);
			}

			return _iHeaderHight;
		}

		public void InsertCharactersInfo()
		{
			if (CurrentPlayer == 1)
				Console.ForegroundColor = ConsoleColor.Green;
			WritePlayerName(PlayerKOLKO.Name, PlayerKOLKOInfoPlace);
			Console.ResetColor();
			if (CurrentPlayer == -1)
				Console.ForegroundColor = ConsoleColor.Green;
			WritePlayerName(PlayerKRZYZYK.Name, PlayerKRZYZYKInfoPlace);
			Console.ResetColor();
		}

		public void SwitchPlayers()
		{
			CurrentPlayer *= -1;
		}
	}
}
