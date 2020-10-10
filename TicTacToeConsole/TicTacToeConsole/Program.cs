using System;
using System.Collections.Generic;
using System.IO;

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
				Console.WriteLine("LOADING...");
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
					case ConsoleKey.D3:
						StatystykiPary();
						break;
					case ConsoleKey.D4:
						Top10();
						break;
					case ConsoleKey.D5:
						Environment.Exit(0);
						break;
					case ConsoleKey.Escape:
						Environment.Exit(0);
						break;
				}
			}
		}

		static void Top10()
		{
			Console.WriteLine("LOADING...");
			Console.Clear();
			Statistics statistics = new Statistics();

			if (!File.Exists(statistics.FileName))
			{
				Console.WriteLine("Nie rozegrano jeszcze rozgrywek.");
			}
			else
			{
				Queue<PlayerStats> _oTop10 = new Queue<PlayerStats>(10);
				List<PlayerStats> _oPlayersStats = new List<PlayerStats>();
				List<string> _oPlayersList = statistics.ReadPlayerList();

				//konwersja z listy nazw graczy na listę statystyk graczy
				foreach (string name in _oPlayersList)
				{
					_oPlayersStats.Add(statistics.ReadPlayerStats(name));
				}

				//wyodrebnienie najlepszych graczy
				int x = 0;
				while (x < 10 && _oPlayersStats.Count != 0)
				{
					PlayerStats _HighestStats = _oPlayersStats[0];
					int _iHighestIndex = 0;
					for (int i = 1; i < _oPlayersStats.Count; i++)
					{
						double _iHighestStatsCount = Math.Round(_HighestStats.Wins * 100 / (float)(_HighestStats.Wins + _HighestStats.Draw + _HighestStats.Loses), 2);
						double _iCurrentStatsCount = Math.Round(_oPlayersStats[i].Wins * 100 / (float)(_oPlayersStats[i].Wins + _oPlayersStats[i].Draw + _oPlayersStats[i].Loses), 2);

						if (_iCurrentStatsCount > _iHighestStatsCount)
						{
							_HighestStats = _oPlayersStats[i];
							_iHighestIndex = i;
						}
					}

					_oTop10.Enqueue(_HighestStats);
					_oPlayersStats.RemoveAt(_iHighestIndex);

				}

				//wyświetlenie TOP10
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("TOP 10 najlepszych graczy:");
				for (int i = 0; i < 10; i++)
				{
					if (_oTop10.Count != 0)
					{
						PlayerStats _Player = _oTop10.Dequeue();
						Console.WriteLine($"{i + 1}. \t{_Player.Name} - {Math.Round(_Player.Wins * 100 / (float)(_Player.Wins + _Player.Draw + _Player.Loses), 2)}%");
					}
					else
						Console.WriteLine($"{i + 1}. \t___ - ___");
				}
				Console.ResetColor();
			}

			Console.WriteLine("(Wcisnij dowolny klawisz, aby wrocic do menu)");
			Console.ReadKey();
		}

		static void StatystykiPary()
		{
			Console.WriteLine("LOADING...");
			Console.Clear();
			Statistics statistics = new Statistics();

			if (!File.Exists(statistics.FileName))
			{
				Console.WriteLine("Nie rozegrano jeszcze rozgrywek.");
			}
			else
			{
				List<string> _oPlayersList = statistics.ReadPlayerList();

				Console.WriteLine("Lista graczy: ");
				int i = 0;
				foreach (string name in _oPlayersList)
				{
					Console.WriteLine($"{++i}. {name}");
				}

				Console.WriteLine();

				int _iPlayerIndex1 = InsertNumber(1, i, "Wybierz pierwszey numer gracza: ");
				int _iPlayerIndex2;
				while ((_iPlayerIndex2 = InsertNumber(1, i, "Wybierz drugi numer gracza: ")) == _iPlayerIndex1) { }

				PlayerStats playerStats = statistics.ReadPlayerStats(_oPlayersList[_iPlayerIndex1 - 1], _oPlayersList[_iPlayerIndex2 - 1]);

				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Cyan;

				Console.WriteLine($"Statystyki pary graczy\t{_oPlayersList[_iPlayerIndex1 - 1]} vs {_oPlayersList[_iPlayerIndex2 - 1]}");
				int _iGamesCount = playerStats.Wins + playerStats.Draw + playerStats.Loses;
				Console.WriteLine($"- liczba rozgrywek:\t{_iGamesCount}\t{_iGamesCount}");
				Console.WriteLine($"- liczba wygranych:\t{playerStats.Wins}\t{playerStats.Loses}");
				Console.WriteLine($"- liczba remisów:\t{playerStats.Draw}\t{playerStats.Draw}");
				Console.WriteLine($"- liczba przegranych:\t{playerStats.Loses}\t{playerStats.Wins}");
				Console.WriteLine($"- stosunek wygranych:\t{Math.Round(playerStats.Wins * 100 / (float)_iGamesCount, 2)}%\t{Math.Round(playerStats.Loses * 100 / (float)_iGamesCount, 2)}%");

				Console.ResetColor();
			}

			Console.WriteLine("(Wcisnij dowolny klawisz, aby wrocic do menu)");
			Console.ReadKey();
		}

		static void StatystykiGracza()
        {
			Console.WriteLine("LOADING...");
			Console.Clear();
			Statistics statistics = new Statistics();

			if (!File.Exists(statistics.FileName))
			{
				Console.WriteLine("Nie rozegrano jeszcze rozgrywek.");
			}
			else
			{
				List<string> _oPlayersList = statistics.ReadPlayerList();

				Console.WriteLine("Lista graczy: ");
				int i = 0;
				foreach (string name in _oPlayersList)
				{
					Console.WriteLine($"{++i}. {name}");
				}

				Console.WriteLine();

				int _iPlayerIndex = InsertNumber(1, i, "Wybierz numer gracza: ");

				PlayerStats playerStats = statistics.ReadPlayerStats(_oPlayersList[_iPlayerIndex - 1]);

				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Cyan;

				Console.WriteLine("Statystyki gracza " + _oPlayersList[_iPlayerIndex - 1]);
				Console.WriteLine("- łączna liczba rozgrywek: " + (playerStats.Loses + playerStats.Wins + playerStats.Draw));
				Console.WriteLine("- liczba wygranych: " + playerStats.Wins);
				Console.WriteLine("- liczba remisów: " + playerStats.Draw);
				Console.WriteLine("- liczba przegranych: " + playerStats.Loses);
				Console.WriteLine($"- stosunek wygranych: {Math.Round(playerStats.Wins * 100 / (float)(playerStats.Wins + playerStats.Loses + playerStats.Draw), 2)}%");

				Console.ResetColor();
			}

			Console.WriteLine("(Wcisnij dowolny klawisz, aby wrocic do menu)");
			Console.ReadKey();
		}

		/// <summary>
		/// A method that handles data entry errors
		/// </summary>
		/// <param name="a_iMinValue">Minimum integer value</param>
		/// <param name="a_iMaxValue">Maximum integer value</param>
		/// <param name="a_sMessage">Error message</param>
		/// <returns>Correct entry</returns>
		static int InsertNumber(int a_iMinValue, int a_iMaxValue, string a_sMessage)
		{
			while (true)
			{
				try
				{
					Console.Write(a_sMessage);
					int _iInput = int.Parse(Console.ReadLine());

					if (_iInput < a_iMinValue || _iInput > a_iMaxValue)
						throw new Exception("Wartośc z poza zakresu!");

					return _iInput;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}
	}
}
