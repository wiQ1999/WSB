using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeConsole
{
	class Player
	{
		public string Name { get; set; }
		public int Value { get; set; }

		public Player(string Name, int Value)
		{
			this.Name = Name;
			this.Value = Value;
		}

		public Char GetChar()
		{
			switch (Value)
			{
				case 1:
					return 'O';
				case -1:
					return 'X';
			}
			return ' ';
		}

		public string GetCharName()
		{
			switch (Value)
			{
				case 1:
					return "KÓLKO";
				case -1:
					return "KŻYRZYK";
			}
			return " ";
		}
	}
}
