using System;
using System.Collections.Generic;

namespace Tennis
{
	public class GameScorer : IGameScorer
	{
		private static readonly IDictionary<int, string> _formattedScores = new Dictionary<int, string>
		{
			{0, "Love" },
			{1, "Fifteen" },
			{2, "Thirty" },
			{3, "Forty" },
		};

		public string GetScore(Player player1, Player player2)
		{
			if (CurrentlyDrawing(player1, player2))
				return GetEqualScores(player1.Score);

			else if (GameIsInLastPointOrEnded(player1, player2))
				return GetEndingResults(player1, player2);

			return GetCurrentScores(player1, player2);
		}

		private static string GetEqualScores(int score)
		{
			if (score >= 3)
				return "Deuce";
			return $"{_formattedScores[score]}-All";
		}

		private static string GetEndingResults(Player player1, Player player2)
		{
			var highPlayer = player1.Score > player2.Score ? player1 : player2;

			if (PlayerAdvantageBy1Point(player1, player2))
				return $"Advantage {highPlayer.Name}";
			else
				return $"Win for {highPlayer.Name}";
		}

		private static string GetCurrentScores(Player player1, Player player2)
		{
			return $"{_formattedScores[player1.Score]}-{_formattedScores[player2.Score]}";
		}


		private static bool CurrentlyDrawing(Player player1, Player player2)
		{
			return player1.Score == player2.Score;
		}

		private static bool GameIsInLastPointOrEnded(Player player1, Player player2)
		{
			return Math.Max(player1.Score, player2.Score) >= 4;
		}

		private static bool PlayerAdvantageBy1Point(Player player1, Player player2)
		{
			return Math.Abs(player1.Score - player2.Score) == 1;
		}

	}
}

