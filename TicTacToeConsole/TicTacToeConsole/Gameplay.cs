using System;
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

		/// <summary>
		/// Main game method
		/// </summary>
		public void StartGame()
		{
			Console.Clear();

			//rysowanie nagłówka
			DrawHeader(new Coordinates { X = 3, Y = Header_Y }, 2, ConsoleColor.Red);

			//rysowanie planszy do gry
			DrawPlayBoard(new Coordinates { X = 0, Y = Board_Y });

			bool _bIsExit = false;
			int _i = 1;
			while (!CheckResult() && _i <= 9)//sprawdzenie rezultatu
			{
				//zmiana gracza
				SwitchPlayers();

				//wprowadzenie nazwy gracza oraz jego kolejności
				InsertCharactersInfo();

				//wprowadzenie pozycji wprowadzanego znaku przez użytkownika
				Coordinates _PlaceToInsert;
				do
				{
					_PlaceToInsert = InsertData(new Coordinates { X = 0, Y = Board_Y + 14 });
					if (_PlaceToInsert.X == -1)//wyjście
					{
                        Console.WriteLine("Wyjście z rozgrywki!");
						_bIsExit = true;
						break;
					}
				} while (CharactersArray[_PlaceToInsert.X - 1, _PlaceToInsert.Y - 1] != 0);
				//wyjście
				if (_bIsExit)
					break;

				//wstawienie znaku gracza do tablicy
				CharactersArray[_PlaceToInsert.X - 1, _PlaceToInsert.Y - 1] = CurrentPlayer;
				DrawCharacters(new Coordinates { X = 0, Y = Board_Y });

				//zwiększenie iteratora gry
				_i++;
			}

			//komunikat kończący rozgrywkę
			if (!_bIsExit)
			{
				Console.Clear();
				if (_i == 10)
				{
					Console.WriteLine("REMIS!");

					//zapisanie danych do pliku
					Statistics statistics = new Statistics();
					statistics.SaveGame(PlayerKOLKO.Name, PlayerKRZYZYK.Name, Character.EMPTY);
				}
				else
				{
					Player _Winner = GetCurrentPlayer(CurrentPlayer);
					Console.WriteLine($"WYGRAL GRACZ {_Winner.Name} - {_Winner.Character}");

					//zapisanie danych do pliku
					Statistics statistics = new Statistics();
					statistics.SaveGame(PlayerKOLKO.Name, PlayerKRZYZYK.Name, _Winner.Character);
				}

				Console.WriteLine("(Wcisnij dowolny klawisz, aby wrocic do menu)");
				Console.ReadKey();
			}
		}

		/// <summary>
		/// Checks player win
		/// </summary>
		/// <returns>Information about win</returns>
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

		/// <summary>
		/// Convert valid keys to integer values
		/// </summary>
		/// <param name="a_Key">Key to parse</param>
		/// <returns>An integer value</returns>
		private int ConvertKeyToInt(ConsoleKey a_Key)
        {
            switch (a_Key)
            {
				case ConsoleKey.D1:
					return 1;
				case ConsoleKey.NumPad1:
					return 1;
				case ConsoleKey.D2:
					return 2;
				case ConsoleKey.NumPad2:
					return 2;
				case ConsoleKey.D3:
					return 3;
				case ConsoleKey.NumPad3:
					return 3;
				default:
					return 0;
			}
        }

		/// <summary>
		/// Check propriety of player input
		/// </summary>
		/// <param name="a_InsertingPlace">Coordinates to insert</param>
		/// <param name="a_sErrorMessag">Message shows when input is incorrect</param>
		/// <param name="a_MessagePlace">Coordinates to show error message</param>
		/// <returns>A valid console key entered by the player</returns>
		private ConsoleKey CheckInsert(Coordinates a_InsertingPlace, string a_sErrorMessag, Coordinates a_MessagePlace)
        {
			ConsoleKey _Key;
			bool _bIsInserting = true;
			do
			{
				//usunięcie nie zatwierdzonego klawisza z konsoli
				RemoveTextArea(a_InsertingPlace, new Coordinates { X = a_InsertingPlace.X + 10, Y = a_InsertingPlace.Y });
				Console.SetCursorPosition(a_InsertingPlace.X, a_InsertingPlace.Y);

				//wczytanie klawisza od uzytkownika oraz sprawdzenie go
				_Key = Console.ReadKey().Key;
				if (_Key == ConsoleKey.Escape || _Key == ConsoleKey.D1 || _Key == ConsoleKey.NumPad1 || _Key == ConsoleKey.D2 || _Key == ConsoleKey.NumPad2 || _Key == ConsoleKey.D3 || _Key == ConsoleKey.NumPad3)
					_bIsInserting = false;
                else
                {
					Console.SetCursorPosition(a_MessagePlace.X, a_MessagePlace.Y);
					Console.WriteLine(a_sErrorMessag);
                }
				
			} while (_bIsInserting);

			return _Key;
        }

		/// <summary>
		/// Player input
		/// </summary>
		/// <param name="a_ComunicationPlace">Coordinates pointing where the error message</param>
		/// <returns>Error output - escape key returns -1 and -1</returns>
		private Coordinates InsertData(Coordinates a_ComunicationPlace)
		{
			Console.SetCursorPosition(a_ComunicationPlace.X, a_ComunicationPlace.Y);

			Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("(ESC) - wyjście");
			Console.ResetColor();

			//usuwanie starych logów
			RemoveTextArea(new Coordinates { X = a_ComunicationPlace.X, Y = a_ComunicationPlace.Y + 2 }, new Coordinates { X = a_ComunicationPlace.X + 43, Y = a_ComunicationPlace.Y + 5 });

			Console.SetCursorPosition(a_ComunicationPlace.X, a_ComunicationPlace.Y + 2);
			Console.Write("Podaj kolumnę: ");
			ConsoleKey _Col = CheckInsert(new Coordinates { X = 15, Y = a_ComunicationPlace.Y + 2 }, "Podaj wartość z przedziału liczbowego 1 - 3", new Coordinates { X = a_ComunicationPlace.X, Y = a_ComunicationPlace.Y + 3 });
			if (_Col == ConsoleKey.Escape) return new Coordinates { X = -1, Y = -1 };
			Console.SetCursorPosition(a_ComunicationPlace.X, a_ComunicationPlace.Y + 4);
			Console.Write("Podaj wiersz: ");
			ConsoleKey _Row = CheckInsert(new Coordinates { X = 14, Y = a_ComunicationPlace.Y + 4 }, "Podaj wartość z przedziału liczbowego 1 - 3", new Coordinates { X = a_ComunicationPlace.X, Y = a_ComunicationPlace.Y + 5 });
			if (_Row == ConsoleKey.Escape) return new Coordinates { X = -1, Y = -1 };

			return new Coordinates { X = ConvertKeyToInt(_Col), Y = ConvertKeyToInt(_Row) };
		}

		/// <summary>
		/// Start inserting information for the player about where to place his character
		/// </summary>
		public void InsertCharactersInfo()
		{
			if (CurrentPlayer == 1)
			{
				WritePlayerName(PlayerKOLKO.Name, PlayerNameKOLKO_Place, ConsoleColor.Green);
				WritePlayerName(PlayerKRZYZYK.Name, PlayerNameKRZYZYK_Place, ConsoleColor.White);
			}
            else
            {
				WritePlayerName(PlayerKOLKO.Name, PlayerNameKOLKO_Place, ConsoleColor.White);
				WritePlayerName(PlayerKRZYZYK.Name, PlayerNameKRZYZYK_Place, ConsoleColor.Green);
			}
		}

		/// <summary>
		/// Switch player
		/// </summary>
		public void SwitchPlayers()
		{
			CurrentPlayer *= -1;
		}
	}
}
