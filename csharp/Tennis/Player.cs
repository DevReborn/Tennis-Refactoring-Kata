using System;

namespace Tennis
{
	public record Player(string Name)
	{
		public int Score { get; set; }
		public int Wins { get; set; }
	}
}

