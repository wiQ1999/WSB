using System;

namespace TicTacToeConsole
{
	class Program
	{
		public const int KOLKO = 1;
		public const int KZYRZYK = -1;

		public static Player player1;
		public static Player player2;

		public static int[,] Board = new int[3, 3];

		public static int BoardFill = 0;

		public static int CurrentPlayer = KOLKO;

		static void Main()
		{
			//menu
			Menu();

			//rysowanie tablicy
			DrawBoard();

			//rozpoczęczęcie gry
			StartGane();
		}

		public static void Menu()
		{
			Console.Clear();
			Console.WriteLine("1. Nowa gra");
			Console.WriteLine("2. Statystyki dla gracza");
			Console.WriteLine("3. Wyjście");

			switch (Console.ReadLine())
			{
				case "1":
					SetPlayers();
					break;
				case "2":
					Console.WriteLine("DODAĆ STATYSTYKI TRZEBA!");
					break;
				case "3":
					Environment.Exit(0);
					break;
			}
		}

		public static void SetPlayers()
		{
			Console.Clear();

			string s_Name;
			int i_Value;

			Console.Write("Podaj nazwę gracza nr. 1:	");
			s_Name = Console.ReadLine();
			Console.Write("Podaj symbol gracza nr. 1 (1 - KÓŁKO, -1 - KŻYRZYK):	");
			i_Value = int.Parse(Console.ReadLine());//ZABEZPIECZYĆ TRY CATCHEM ORAZ PRZEDZIAŁEM!!!!!!@@@@@
			player1 = new Player(s_Name, i_Value);

			Console.Write("Podaj nazwę gracza nr. 2:	");
			s_Name = Console.ReadLine();
			Console.Write("Podaj symbol gracza nr. 2 (1 - KÓŁKO, -1 - KŻYRZYK):	");
			i_Value = int.Parse(Console.ReadLine());//ZABEZPIECZYĆ TRY CATCHEM ORAZ PRZEDZIAŁEM!!!!!!@@@@@
			player2 = new Player(s_Name, i_Value);
		}

		public static void DrawBoard()
		{
			Console.Clear();

			//gracze
			Console.WriteLine($"Gracz {player1.Name} - {player1.GetCharName()}");
			Console.WriteLine($"Gracz {player2.Name} - {player2.GetCharName()}");

			//plansza
			for (int i = 0; i < 13; i++)
			{
				if (i == 0 || i == 4 || i == 8 || i == 12)
					Console.WriteLine("+---+---+---+");
				else
					Console.WriteLine("|   |   |   |");
			}

		}

		public static void StartGane()
		{
			string Comunicate = string.Empty;
			bool IsGameRunning = true;
			while (IsGameRunning)
			{
				//zmienna bool sprawdzająca czy podane dane są porpawne
				bool IsInputCorrect;
				Player player;

				//pętla do wprowadzania danych dopóki nie są poprawne
				do
				{
					//ustalenie który gracz jest aktualny
					if (CurrentPlayer == 1)
						player = player1;
					else
						player = player2;

					//pobranie miejsca postawienia znaku przez gracza
					Console.SetCursorPosition(0, 15);
					BoardPlace boardPlace;
					boardPlace = PlayerInput(player);

					//próba wporwadzenia danych podanych przez gracza
					IsInputCorrect = InsertIntoBoard(player, boardPlace);

					if(IsInputCorrect == false)
						Console.WriteLine("Niepoprawne dane!");
				}
				while (!IsInputCorrect);

				//wyświetlenie tablicy oraz znaków
				DrawBoard();
				DrowChars();

				//sprawdzenie czy ktoś nie wygrał
				IsGameRunning = CheckBoard();
				if (BoardFill == 9)
				{
					Comunicate = "REMIS!";
					IsGameRunning = false;
				}
				else
					Comunicate = $"Koniec gry!\nWygrał gracz {player.Name}";

				//zmiana gracza
				SwitchCurrentPlayer();
			}
			//wyświetlenie wyniku
			Console.SetCursorPosition(0, 18);
			Console.WriteLine(Comunicate);
		}

		public static bool CheckBoard()
		{
			if (Board[0, 0] + Board[1, 0] + Board[2, 0] == 3 || Board[0, 0] + Board[1, 0] + Board[2, 0] == -3 ||
				Board[0, 1] + Board[1, 1] + Board[2, 1] == 3 || Board[0, 1] + Board[1, 1] + Board[2, 1] == -3 ||
				Board[0, 2] + Board[1, 2] + Board[2, 2] == 3 || Board[0, 2] + Board[1, 2] + Board[2, 2] == -3 ||

				Board[0, 0] + Board[0, 1] + Board[0, 2] == 3 || Board[0, 0] + Board[0, 1] + Board[0, 2] == -3 ||
				Board[1, 0] + Board[1, 1] + Board[1, 2] == 3 || Board[1, 0] + Board[1, 1] + Board[1, 2] == -3 ||
				Board[2, 0] + Board[2, 1] + Board[2, 2] == 3 || Board[2, 0] + Board[2, 1] + Board[2, 2] == -3 ||

				Board[0, 0] + Board[1, 1] + Board[2, 2] == 3 || Board[0, 0] + Board[1, 1] + Board[2, 2] == -3 ||
				Board[2, 0] + Board[1, 1] + Board[0, 2] == 3 || Board[2, 0] + Board[1, 1] + Board[0, 2] == -3
				)
				return false;


			return true;
		}

		public static void DrowChars()
		{
			//pierwszy element: x2-y4
			BoardPlace StartingPlace = new BoardPlace { X = 2, Y = 4 };

			for (int y = 0; y < Board.GetLength(1); y++)
			{
				for (int x = 0; x < Board.GetLength(0); x++)
				{
					Console.SetCursorPosition(StartingPlace.X, StartingPlace.Y);

					if(Board[x, y] != 0)
					{
						switch(Board[x, y])
						{
							case 1:
								Console.Write("O");
								break;
							default:
								Console.Write("X");
								break;
						}
					}

					//zmiana X
					StartingPlace.X = StartingPlace.X + 4;
				}
				//zmiana Y
				StartingPlace.Y = StartingPlace.Y + 4;
				StartingPlace.X = 2;
			}
		}

		public static void SwitchCurrentPlayer()
		{
			CurrentPlayer *= -1;
		}

		public static bool InsertIntoBoard(Player a_player, BoardPlace a_boardPlace)
		{
			if (Board[a_boardPlace.X, a_boardPlace.Y] == 0)
			{
				Board[a_boardPlace.X, a_boardPlace.Y] = a_player.Value;

				//usupełnienie liczby wypęłnionych miejsc na tablicy
				BoardFill++;

				return true;
			}

			return false;
		}

		public static BoardPlace PlayerInput(Player a_Currendt)
		{
			BoardPlace Result = new BoardPlace { };
			Console.WriteLine($"Gracz - {a_Currendt.Name} podaj, w którym miejscu postawić znak {a_Currendt.GetChar()}");

			//!@!@ ZABEZPIECYĆ PRZED INNYMI WARTOŚCIAMI NIŻ 1 I -1 I POPRAWIĆ TRY CATCH	@!@!
			try
			{
				Console.Write("Kolumna:");
				Result.X = int.Parse(Console.ReadLine());

				Console.Write("Wiersz:");
				Result.Y = int.Parse(Console.ReadLine());
			}
			catch(Exception e)
			{
				Console.SetCursorPosition(0, 18);
				Console.WriteLine(e.Message);
			}

			return Result;
		}

	}
}
