using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tennis
{
	public class TennisGame1 : ITennisGame
    {
		private readonly IGameScorer _scorer;
		private readonly Player _player1;
		private readonly Player _player2;
		private readonly IDictionary<string, Player> _players;

		public TennisGame1(string player1Name, string player2Name)
        {
			_scorer = new GameScorer();
			_player1 = new Player(player1Name);
			_player2 = new Player(player2Name);
			_players = new ReadOnlyDictionary<string, Player>(new Dictionary<string, Player>()
			{
				{ player1Name, _player1 },
				{ player2Name, _player2 },
			});
		}

        public void WonPoint(string playerName)
        {
			var winner = _players[playerName];
			winner.Score++;

			if(_scorer.IsGameCompleted(_player1, _player2))
				PlayerWins(winner);
		}

		public int GetPlayerWins(string playerName)
		{
			return _players[playerName].Wins;
		}

		public string GetScore()
        {
			return _scorer.GetScore(_player1, _player2);
		}

		private void PlayerWins(Player winner)
		{
			_player1.Score = 0;
			_player2.Score = 0;
			winner.Wins++;
		}
	}
}

