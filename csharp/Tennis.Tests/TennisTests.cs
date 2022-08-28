using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Tennis.Tests
{
    public class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {0, 0, "Love-All"},
            new object[] {1, 1, "Fifteen-All"},
            new object[] {2, 2, "Thirty-All"},
            new object[] {3, 3, "Deuce"},
            new object[] {4, 4, "Deuce"},
            new object[] {1, 0, "Fifteen-Love"},
            new object[] {0, 1, "Love-Fifteen"},
            new object[] {2, 0, "Thirty-Love"},
            new object[] {0, 2, "Love-Thirty"},
            new object[] {3, 0, "Forty-Love"},
            new object[] {0, 3, "Love-Forty"},
            //new object[] {4, 0, "Win for player1"},
            //new object[] {0, 4, "Win for player2"},
            new object[] {2, 1, "Thirty-Fifteen"},
            new object[] {1, 2, "Fifteen-Thirty"},
            new object[] {3, 1, "Forty-Fifteen"},
            new object[] {1, 3, "Fifteen-Forty"},
            //new object[] {4, 1, "Win for player1"},
            //new object[] {1, 4, "Win for player2"},
            new object[] {3, 2, "Forty-Thirty"},
            new object[] {2, 3, "Thirty-Forty"},
            //new object[] {4, 2, "Win for player1"},
            //new object[] {2, 4, "Win for player2"},
            new object[] {4, 3, "Advantage player1"},
            new object[] {3, 4, "Advantage player2"},
            new object[] {5, 4, "Advantage player1"},
            new object[] {4, 5, "Advantage player2"},
            new object[] {15, 14, "Advantage player1"},
            new object[] {14, 15, "Advantage player2"},
            //new object[] {6, 4, "Win for player1"},
            //new object[] {4, 6, "Win for player2"},
            //new object[] {16, 14, "Win for player1"},
            //new object[] {14, 16, "Win for player2"},
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class TennisTests
    {
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void Tennis1Test(int p1, int p2, string expected)
        {
            var game = new TennisGame1("player1", "player2");
			CheckAllScores(game, p1, p2, expected);
        }

		[Fact]
        public void Player1WinsAllPoints()
		{
			// Arrange
			const string player1Name = "player1";
			const string player2Name = "player2";

			var game = new TennisGame1(player1Name, player2Name);
			AddPoints(game, player1Name, 4);

			// Act
			var result = game.GetScore();

            // Assert
            Assert.Equal("Love-All", result);
			Assert.Equal(1, game.GetPlayerWins(player1Name));
			Assert.Equal(0, game.GetPlayerWins(player2Name));
		}

		[Fact]
		public void Player2WinsAllPoints()
		{
			// Arrange
			const string player1Name = "player1";
			const string player2Name = "player2";

			var game = new TennisGame1(player1Name, player2Name);
			AddPoints(game, player2Name, 4);

			// Act
			var result = game.GetScore();

			// Assert
			Assert.Equal("Love-All", result);
			Assert.Equal(0, game.GetPlayerWins(player1Name));
			Assert.Equal(1, game.GetPlayerWins(player2Name));
		}

		[Fact]
		public void BothPlayersGoToDeuce()
		{
			// Arrange
			const string player1Name = "player1";
			const string player2Name = "player2";

			var game = new TennisGame1(player1Name, player2Name);
			AddPoints(game, player1Name, 3);
			AddPoints(game, player2Name, 3);

			AddPoints(game, player1Name, 2);

			// Act
			var result = game.GetScore();

			// Assert
			Assert.Equal("Love-All", result);
			Assert.Equal(1, game.GetPlayerWins(player1Name));
			Assert.Equal(0, game.GetPlayerWins(player2Name));
		}

		[Fact]
		public void PlayMultipleGames()
		{
			// Arrange
			const string player1Name = "player1";
			const string player2Name = "player2";

			var game = new TennisGame1(player1Name, player2Name);
			AddPoints(game, player1Name, 4);
			AddPoints(game, player2Name, 4);

			AddPoints(game, player1Name, 3);
			AddPoints(game, player2Name, 5);

			// Act
			var result = game.GetScore();

			// Assert
			Assert.Equal("Love-All", result);
			Assert.Equal(1, game.GetPlayerWins(player1Name));
			Assert.Equal(2, game.GetPlayerWins(player2Name));
		}

		private static void AddPoints(ITennisGame game, string player, int points)
		{
			for (int i = 0; i < points; i++)
			{
				game.WonPoint(player);
			}
		}

		private static void CheckAllScores(ITennisGame game, int player1Score, int player2Score, string expectedScore)
        {
            var highestScore = Math.Max(player1Score, player2Score);
            for (var i = 0; i < highestScore; i++)
            {
                if (i < player1Score)
                    game.WonPoint("player1");
                if (i < player2Score)
                    game.WonPoint("player2");
            }

            Assert.Equal(expectedScore, game.GetScore());
        }
    }
}