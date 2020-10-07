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
			CurrentPlayer = a_PlayerKRZYZYK.GetCharacterValue();
		}

		public void StartGame()
		{
			Console.Clear();

			//rysowanie nagłówka
			DrawHeader(new Coordinates { X = 3, Y = Header_Y }, 2, ConsoleColor.Blue);

			//rysowanie planszy do gry
			DrawPlayBoard(new Coordinates { X = 0, Y = Board_Y });

			while (!CheckResult())//sprawdzenie rezultatu
			{
				//wprowadzenie nazwy gracza oraz jego kolejności
				InsertCharactersInfo();

				//wprowadzenie pozycji wprowadzanego znaku przez użytkownika
				Coordinates _PlaceToInsert = InsertData(new Coordinates { X = 0, Y = Board_Y + 14 });
				if (_PlaceToInsert.X == -1)//wyjście
					break;

				//wstawienie znaku gracza do tablicy
				CharactersArray[_PlaceToInsert.X - 1, _PlaceToInsert.Y - 1] = CurrentPlayer;

				//zmiana gracza
				SwitchPlayers();
			}

			//zmiana gracza ze względu na zmianę na końcu pętli
			SwitchPlayers();


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

		private ConsoleKey CheckInsert(Coordinates a_InsertingPlace, string a_sErrorMessag, Coordinates a_MessagePlace)
        {
			ConsoleKey _Key;
			bool _bIsInserting = true;
			do
			{
				//usunięcie nie zatwierdzonego klawisza z konsoli
				Console.SetCursorPosition(a_InsertingPlace.X, a_InsertingPlace.Y);
                Console.Write("          ");
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

		private Coordinates InsertData(Coordinates a_ComunicationPlace)
		{
			Console.SetCursorPosition(a_ComunicationPlace.X, a_ComunicationPlace.Y);

			Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("(ESC) - wyjście");
			Console.ResetColor();

            Console.WriteLine();
			
			Console.Write("Podaj kolumnę: ");
			ConsoleKey _Col = CheckInsert(new Coordinates { X = 15, Y = a_ComunicationPlace.Y + 2 }, "Podaj wartość z przedziału liczbowego 1 - 3", new Coordinates { X = a_ComunicationPlace.X, Y = a_ComunicationPlace.Y + 3 });
			if (_Col == ConsoleKey.Escape) return new Coordinates { X = -1, Y = -1 };
			Console.Write("Podaj wiersz: ");
			ConsoleKey _Row = CheckInsert(new Coordinates { X = 14, Y = a_ComunicationPlace.Y + 4 }, "Podaj wartość z przedziału liczbowego 1 - 3", new Coordinates { X = a_ComunicationPlace.X, Y = a_ComunicationPlace.Y + 5 });
			if (_Row == ConsoleKey.Escape) return new Coordinates { X = -1, Y = -1 };

			return new Coordinates { X = ConvertKeyToInt(_Col), Y = ConvertKeyToInt(_Row) };
		}

		public void InsertCharactersInfo()
		{
			if (CurrentPlayer == 1)
			{
				WritePlayerName(PlayerKOLKO.Name, PlayerNameKOLKO_Place, ConsoleColor.Green);
				WritePlayerName(PlayerKRZYZYK.Name, PlayerNameKRZYZYK_Place);
			}
            else
            {
				WritePlayerName(PlayerKOLKO.Name, PlayerNameKOLKO_Place);
				WritePlayerName(PlayerKRZYZYK.Name, PlayerNameKRZYZYK_Place, ConsoleColor.Green);
			}
		}

		public void SwitchPlayers()
		{
			CurrentPlayer *= -1;
		}
	}
}
