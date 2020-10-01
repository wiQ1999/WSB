using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeConsole.Interfaces
{
	interface IRules
	{
		int CurrentPlayer { get; set; }

		void InsertCharactersInfo();

		void SwitchPlayers();

		bool CheckResult();
	}
}
