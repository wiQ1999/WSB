using System;

namespace TicTacToeConsole
{
	abstract class Board
	{
        #region Coordinates

        public const int Header_Y = 1;
		public const int PlayerNameKOLKO_X = 5;
		public const int PlayerNameKRZYZYK_X = 13;
		public const int PlayersNames_Y = Header_Y + 2;
		public Coordinates PlayerNameKOLKO_Place
		{
			get => new Coordinates { X = PlayerNameKOLKO_X, Y = PlayersNames_Y };
		}
		public Coordinates PlayerNameKRZYZYK_Place
		{
			get => new Coordinates { X = PlayerNameKRZYZYK_X, Y = PlayersNames_Y };
		}
		public int PlayerNameKOLKO_Height { get => WritePlayerName(PlayerKOLKO.Name, PlayerNameKOLKO_Place, ConsoleColor.White, false); }
		public int PlayerNameKRZYZYK_Height { get => WritePlayerName(PlayerKRZYZYK.Name, PlayerNameKRZYZYK_Place, ConsoleColor.White, false); }
		public int PlayersNames_Height { get => PlayerNameKOLKO_Height > PlayerNameKRZYZYK_Height ? PlayerNameKOLKO_Height : PlayerNameKRZYZYK_Height; }
		public int Board_Y { get => Header_Y + PlayersNames_Height + 3; }

        #endregion


        public Player PlayerKOLKO { get; private set; }
		public Player PlayerKRZYZYK { get; private set; }
		public int[,] CharactersArray { get; set; }


		public Board(Player a_PlayerKOLKO, Player a_PlayerKRZYZYK)
		{
			PlayerKOLKO = a_PlayerKOLKO;
			PlayerKRZYZYK = a_PlayerKRZYZYK;
			CharactersArray = new int[3, 3];
		}

		/// <summary>
		/// Get current player
		/// </summary>
		/// <param name="a_CurrentPlayer">Value of current player</param>
		/// <returns>Current player instance</returns>
		public Player GetCurrentPlayer(int a_CurrentPlayer)
        {
			return a_CurrentPlayer == 1 ? PlayerKOLKO : PlayerKRZYZYK;
		}

		/// <summary>
		/// Remove area of text
		/// </summary>
		/// <param name="a_StartPlace">Coordinates where cleaning have to start</param>
		/// <param name="a_EndPlace">Coordinates where cleaning have to end</param>
		public void RemoveTextArea(Coordinates a_StartPlace, Coordinates a_EndPlace)
        {
            for (int y = a_StartPlace.Y; y <= a_EndPlace.Y; y++)
            {
				for (int x = a_StartPlace.X; x <= a_EndPlace.X; x++)
				{
					Console.SetCursorPosition(x, y);
					Console.Write(" ");
				}
			}
        }

		/// <summary>
		/// Write player name on a console
		/// </summary>
		/// <param name="a_sName">Player name</param>
		/// <param name="a_PlaceToWrite">Coordinate pointing place to write</param>
		/// <param name="a_NameColor">Color of name</param>
		/// <param name="a_bIsWriting">Switch to write or not</param>
		/// <returns>An int value representing the number of lines of the name</returns>
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
			else if(a_bIsWriting)
			{
				Console.SetCursorPosition(a_PlaceToWrite.X - (a_sName.Length / 2), a_PlaceToWrite.Y);
				Console.Write(a_sName);
			}

			Console.ResetColor();
			return _iHeaderHight;
		}

		/// <summary>
		/// Droaw charackters array on a board
		/// </summary>
		/// <param name="a_BoardPosition">Coordinates of board</param>
		public void DrawCharacters(Coordinates a_BoardPosition)
        {
			int _iY = -2;
			int _iX = -3;

			for (int y = 0; y < CharactersArray.GetLength(1); y++)
            {
				_iY += 4;
				for (int x = 0; x < CharactersArray.GetLength(0); x++)
				{
					_iX += 6;
					Console.SetCursorPosition(a_BoardPosition.X + _iX, a_BoardPosition.Y +_iY);
                    switch (CharactersArray[x, y])
                    {
						case 1:
                            Console.Write("O");
							break;
						case -1:
							Console.Write("X");
							break;
						default:
							break;
					}
				}
				_iX = -3;
			}
        }

		/// <summary>
		/// Draw header
		/// </summary>
		/// <param name="a_HeaderPosition">Coordinates of header</param>
		/// <param name="a_iDistanceBetween">Distance beetwen "KOLKO" and "KRZYZYK"</param>
		/// <param name="a_Color">Color of text</param>
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

		/// <summary>
		/// Draw main play board
		/// </summary>
		/// <param name="a_BoardPosition">Coordinates of play board on a console</param>
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
