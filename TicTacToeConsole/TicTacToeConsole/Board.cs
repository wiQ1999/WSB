using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeConsole
{
	abstract class Board
	{
		public Player PlayerKOLKO { get; private set; }
		public Player PlayerKRZYZYK { get; private set; }

		public int[,] CharactersArray { get; set; }

		public Board(Player a_PlayerKOLKO, Player a_PlayerKRZYZYK)
		{
			PlayerKOLKO = a_PlayerKOLKO;
			PlayerKRZYZYK = a_PlayerKRZYZYK;
			CharactersArray = new int[3, 3];
		}

		public void DrowPlayersInformation()
		{
			
		}

		public void DrawPlayBoard()
		{
			for (int i = 0; i < 13; i++)
			{
				if (i == 0 || i == 4 || i == 8 || i == 12)
					Console.WriteLine("+---+---+---+");
				else
					Console.WriteLine("|   |   |   |");
			}
		}
	}
}
