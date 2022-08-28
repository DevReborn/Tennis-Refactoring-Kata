using System;

namespace Tennis
{
	public record Player(string Name)
	{
		public int Score { get; private set; }

		public void AddPoint()
		{
			Score++;
		}
	}
}

