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

			else if (GameIsInLastPoint(player1, player2))
				return GetAdvantageResult(player1, player2);

			return GetCurrentScores(player1, player2);
		}

		public bool IsGameCompleted(Player player1, Player player2)
		{
			if (CurrentlyDrawing(player1, player2))
				return false;
			if (GameIsInLastPoint(player1, player2))
				return !PlayerAdvantageBy1Point(player1, player2);
			return false;
		}

		private static string GetEqualScores(int score)
		{
			if (score >= 3)
				return "Deuce";
			return $"{_formattedScores[score]}-All";
		}

		private static string GetAdvantageResult(Player player1, Player player2)
		{
			var highPlayer = player1.Score > player2.Score ? player1 : player2;
			return $"Advantage {highPlayer.Name}";
		}

		private static string GetCurrentScores(Player player1, Player player2)
		{
			return $"{_formattedScores[player1.Score]}-{_formattedScores[player2.Score]}";
		}


		private static bool CurrentlyDrawing(Player player1, Player player2)
		{
			return player1.Score == player2.Score;
		}

		private static bool GameIsInLastPoint(Player player1, Player player2)
		{
			return Math.Max(player1.Score, player2.Score) >= 4;
		}

		private static bool PlayerAdvantageBy1Point(Player player1, Player player2)
		{
			return Math.Abs(player1.Score - player2.Score) == 1;
		}
	}
}

