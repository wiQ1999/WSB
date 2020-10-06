using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeConsole
{
	abstract class Board
	{
		public const int Header_Y = 1;

		public Player PlayerKOLKO { get; private set; }
		public Player PlayerKRZYZYK { get; private set; }

		public int[,] CharactersArray { get; set; }

		public Board(Player a_PlayerKOLKO, Player a_PlayerKRZYZYK)
		{
			PlayerKOLKO = a_PlayerKOLKO;
			PlayerKRZYZYK = a_PlayerKRZYZYK;
			CharactersArray = new int[3, 3];
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
