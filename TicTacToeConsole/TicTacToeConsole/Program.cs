using System;

namespace TicTacToeConsole
{
	class Program
	{
		static void Main()
		{
			Menu();
		}

		public static void Menu()
		{
			Console.Clear();
			Console.WriteLine("1. Nowa gra");
			Console.WriteLine("2. Statystyki dla gracza");
			Console.WriteLine("3. Wyjście");

			while (true)
			{
				switch (Console.ReadKey().Key)
				{
					case ConsoleKey.D1:
						Gameplay gameplay = new Gameplay(SetPlayer(Character.KOLKO), SetPlayer(Character.KRZYZYK));
						gameplay.StartGame();
						break;
					case ConsoleKey.D2:
						Console.WriteLine("DODAĆ STATYSTYKI TRZEBA!");
						break;
					case ConsoleKey.D3:
						Environment.Exit(0);
						break;
				}
			}
		}

		public static Player SetPlayer(Character a_Character)//przeniesc do ctora player albo podklasy player
		{
			Console.Clear();

			string _sName;
			Console.WriteLine("GRACZ "+ a_Character);
			Console.Write("Podaj nazwę:		");
			_sName = Console.ReadLine();
			return new  Player(_sName, a_Character);
		}

		//public static void StartGane()
		//{
		//	string Comunicate = string.Empty;
		//	bool IsGameRunning = true;
		//	while (IsGameRunning)
		//	{
		//		//zmienna bool sprawdzająca czy podane dane są porpawne
		//		bool IsInputCorrect;
		//		Player player;

		//		//pętla do wprowadzania danych dopóki nie są poprawne
		//		do
		//		{
		//			//ustalenie który gracz jest aktualny
		//			if (CurrentPlayer == 1)
		//				player = player1;
		//			else
		//				player = player2;

		//			//pobranie miejsca postawienia znaku przez gracza
		//			Console.SetCursorPosition(0, 15);
		//			Coordinates boardPlace;
		//			boardPlace = PlayerInput(player);

		//			//próba wporwadzenia danych podanych przez gracza
		//			IsInputCorrect = InsertIntoBoard(player, boardPlace);

		//			if(IsInputCorrect == false)
		//				Console.WriteLine("Niepoprawne dane!");
		//		}
		//		while (!IsInputCorrect);

		//		//wyświetlenie tablicy oraz znaków
		//		DrawBoard();
		//		DrowChars();

		//		//sprawdzenie czy ktoś nie wygrał
		//		IsGameRunning = CheckBoard();
		//		if (BoardFill == 9)
		//		{
		//			Comunicate = "REMIS!";
		//			IsGameRunning = false;
		//		}
		//		else
		//			Comunicate = $"Koniec gry!\nWygrał gracz {player.Name}";

		//		//zmiana gracza
		//		SwitchCurrentPlayer();
		//	}
		//	//wyświetlenie wyniku
		//	Console.SetCursorPosition(0, 18);
		//	Console.WriteLine(Comunicate);
		//}

		//public static bool CheckBoard()
		//{
		//	if (Board[0, 0] + Board[1, 0] + Board[2, 0] == 3 || Board[0, 0] + Board[1, 0] + Board[2, 0] == -3 ||
		//		Board[0, 1] + Board[1, 1] + Board[2, 1] == 3 || Board[0, 1] + Board[1, 1] + Board[2, 1] == -3 ||
		//		Board[0, 2] + Board[1, 2] + Board[2, 2] == 3 || Board[0, 2] + Board[1, 2] + Board[2, 2] == -3 ||

		//		Board[0, 0] + Board[0, 1] + Board[0, 2] == 3 || Board[0, 0] + Board[0, 1] + Board[0, 2] == -3 ||
		//		Board[1, 0] + Board[1, 1] + Board[1, 2] == 3 || Board[1, 0] + Board[1, 1] + Board[1, 2] == -3 ||
		//		Board[2, 0] + Board[2, 1] + Board[2, 2] == 3 || Board[2, 0] + Board[2, 1] + Board[2, 2] == -3 ||

		//		Board[0, 0] + Board[1, 1] + Board[2, 2] == 3 || Board[0, 0] + Board[1, 1] + Board[2, 2] == -3 ||
		//		Board[2, 0] + Board[1, 1] + Board[0, 2] == 3 || Board[2, 0] + Board[1, 1] + Board[0, 2] == -3
		//		)
		//		return false;


		//	return true;
		//}

		//public static void DrowChars()
		//{
		//	//pierwszy element: x2-y4
		//	Coordinates StartingPlace = new Coordinates { X = 2, Y = 4 };

		//	for (int y = 0; y < Board.GetLength(1); y++)
		//	{
		//		for (int x = 0; x < Board.GetLength(0); x++)
		//		{
		//			Console.SetCursorPosition(StartingPlace.X, StartingPlace.Y);

		//			if(Board[x, y] != 0)
		//			{
		//				switch(Board[x, y])
		//				{
		//					case 1:
		//						Console.Write("O");
		//						break;
		//					default:
		//						Console.Write("X");
		//						break;
		//				}
		//			}

		//			//zmiana X
		//			StartingPlace.X = StartingPlace.X + 4;
		//		}
		//		//zmiana Y
		//		StartingPlace.Y = StartingPlace.Y + 4;
		//		StartingPlace.X = 2;
		//	}
		//}

		//public static void SwitchCurrentPlayer()
		//{
		//	CurrentPlayer *= -1;
		//}

		//public static bool InsertIntoBoard(Player a_player, Coordinates a_boardPlace)
		//{
		//	if (Board[a_boardPlace.X, a_boardPlace.Y] == 0)
		//	{
		//		Board[a_boardPlace.X, a_boardPlace.Y] = a_player.Character;

		//		//usupełnienie liczby wypęłnionych miejsc na tablicy
		//		BoardFill++;

		//		return true;
		//	}

		//	return false;
		//}

		//public static Coordinates PlayerInput(Player a_Currendt)
		//{
		//	Coordinates Result = new Coordinates { };
		//	Console.WriteLine($"Gracz - {a_Currendt.Name} podaj, w którym miejscu postawić znak {a_Currendt.GetChar()}");

		//	//!@!@ ZABEZPIECYĆ PRZED INNYMI WARTOŚCIAMI NIŻ 1 I -1 I POPRAWIĆ TRY CATCH	@!@!
		//	try
		//	{
		//		Console.Write("Kolumna:");
		//		Result.X = int.Parse(Console.ReadLine());

		//		Console.Write("Wiersz:");
		//		Result.Y = int.Parse(Console.ReadLine());
		//	}
		//	catch(Exception e)
		//	{
		//		Console.SetCursorPosition(0, 18);
		//		Console.WriteLine(e.Message);
		//	}

		//	return Result;
		//}

	}
}
