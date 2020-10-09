using System;
using System.Collections.Generic;

namespace TicTacToeConsole
{
	class Program
	{
		static void Main()
		{
			Menu();
		}

		static void Menu()
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("1. Nowa gra");
				Console.WriteLine("2. Statystyki dla gracza");
				Console.WriteLine("3. Statystyki dla pary");
				Console.WriteLine("4. Top 10");
				Console.WriteLine("5. Wyjście");

				switch (Console.ReadKey().Key)
				{
					case ConsoleKey.D1:
						Gameplay gameplay = new Gameplay(new Player(Character.KOLKO), new Player(Character.KRZYZYK));
						gameplay.StartGame();
						break;
					case ConsoleKey.D2:
						StatystykiGracza();
						break;
					case ConsoleKey.D5:
						Environment.Exit(0);
						break;
				}
			}
		}

		static string InsertNumber(int a_iMinValue, int a_iMaxValue, string a_sMessage)
        {
            try
            {
                Console.Write(a_sMessage);
				int _iInput;
				


            }
			catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

			return "";
        }

		static void StatystykiGracza()
        {
			Console.Clear();
			Statistics statistics = new Statistics();

			List<string> _oPlayersList = (List<string>)statistics.Read(StatsOptions.PlayersList);

			int i = 0;
            foreach (string name in _oPlayersList)
            {
                Console.WriteLine($"{++i}. {name}");
            }

            Console.WriteLine();

			string _sInput = InsertNumber(1, i, "Wybierz numer gracza: ");
			

        }
	}
}
