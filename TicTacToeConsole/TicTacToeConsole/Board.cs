using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace TicTacToeConsole
{
	abstract class Board
	{
		public const int Header_Y = 1;
		public const int PlayerNameKOLKO_X = 5;
		public const int PlayerNameKRZYZYK_X = 13;
		public const int PlayersNames_Y = Header_Y + 2;
		public int PlayerNameKOLKO_Height { get => WritePlayerName(PlayerKOLKO.Name, PlayerNameKOLKO_Place, false); }
		public int PlayerNameKRZYZYK_Height { get => WritePlayerName(PlayerKRZYZYK.Name, PlayerNameKRZYZYK_Place, false); }
		public int PlayersNames_Height { get => PlayerNameKOLKO_Height > PlayerNameKRZYZYK_Height ? PlayerNameKOLKO_Height : PlayerNameKRZYZYK_Height; }
		public int Board_Y { get => Header_Y + PlayersNames_Height + 3; }
		public Coordinates PlayerNameKOLKO_Place
		{
			get => new Coordinates { X = PlayerNameKOLKO_X, Y = PlayersNames_Y };
		}
		public Coordinates PlayerNameKRZYZYK_Place
		{
			get => new Coordinates { X = PlayerNameKRZYZYK_X, Y = PlayersNames_Y };
		}


		public Player PlayerKOLKO { get; private set; }
		public Player PlayerKRZYZYK { get; private set; }
		

		private int[,] _CharactersArray;
		public int[,] CharactersArray
		{
			get => _CharactersArray;
			set
			{
				_CharactersArray = value;
				DrawCharacters(new Coordinates { X = 0, Y = Board_Y });
			}
		}

		public Board(Player a_PlayerKOLKO, Player a_PlayerKRZYZYK)
		{
			PlayerKOLKO = a_PlayerKOLKO;
			PlayerKRZYZYK = a_PlayerKRZYZYK;
			CharactersArray = new int[3, 3];
		}

		public int WritePlayerName(string a_sName, Coordinates a_PlaceToWrite, ConsoleColor a_NameColor, bool a_bIsWriting = true)
		{
			Console.ForegroundColor = a_NameColor;

			int _iHeaderHight = 1;

			if (a_sName.Length > 7)
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
						_iHeaderHight++;
					}

					_sRestOfName = _sRestOfName.Remove(0, _sTempText.Length);

					Console.SetCursorPosition(a_PlaceToWrite.X - (_sTempText.Length / 2), a_PlaceToWrite.Y + _iHeaderHight - 1);

					if (a_bIsWriting)
					{
						Console.Write(_sTempText);
						if (!_bIsEnd)
							Console.Write("-");
					}

				} while (_sRestOfName.Length > 0);
			}
			else
			{
				Console.SetCursorPosition(a_PlaceToWrite.X - (a_sName.Length / 2), a_PlaceToWrite.Y);
				Console.Write(a_sName);
			}

			Console.ResetColor();
			return _iHeaderHight;
		}

		public  int WritePlayerName(string a_sName, Coordinates a_PlaceToWrite, bool a_bIsWriting = true)
		{
			int _iHeaderHight = 1;

			if (a_sName.Length > 7)
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
						_iHeaderHight++;
					}

					_sRestOfName = _sRestOfName.Remove(0, _sTempText.Length);

					Console.SetCursorPosition(a_PlaceToWrite.X - (_sTempText.Length / 2), a_PlaceToWrite.Y + _iHeaderHight - 1);

					if (a_bIsWriting)
					{
						Console.Write(_sTempText);
						if (!_bIsEnd)
							Console.Write("-");
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

		private void DrawCharacters(Coordinates a_BoardPosition)
        {
            for (int y = 0; y < CharactersArray.GetLength(1); y++)
            {
				for (int x = 0; x < CharactersArray.GetLength(0); x++)
				{

				}
			}
        }

		public void DrawHeader(Coordinates a_HeaderPosition, int a_iDistanceBetween, ConsoleColor a_Color)
        {
			Console.SetCursorPosition(a_HeaderPosition.X, a_HeaderPosition.Y);
			Console.ForegroundColor = a_Color;
            Console.Write("KOLKO");
            for (int i = 0; i < a_iDistanceBetween; i++)
            {
                Console.Write(" ");
            }
			Console.Write("KRZYZYK");
			Console.ResetColor();
		}

		public void DrawPlayBoard(Coordinates a_BoardPosition)
		{
			Console.SetCursorPosition(a_BoardPosition.X, a_BoardPosition.Y);
			for (int i = 0; i < 13; i++)
			{
				if (i == 0 || i == 4 || i == 8 || i == 12)
					Console.WriteLine("+-----+-----+-----+");
				else
					Console.WriteLine("|     |     |     |");
			}
		}
	}
}
