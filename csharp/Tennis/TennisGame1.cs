namespace Tennis
{
	public class TennisGame1 : ITennisGame
    {
		private readonly IGameScorer _scorer;
		private readonly Player _player1;
		private readonly Player _player2;

		public TennisGame1(string player1Name, string player2Name)
        {
			_scorer = new GameScorer();
            _player1 = new Player(player1Name);
			_player2 = new Player(player2Name);
		}

        public void WonPoint(string playerName)
        {
			if (_player1.Name == playerName)
				_player1.AddPoint();
			else
				_player2.AddPoint();
		}

        public string GetScore()
        {
			return _scorer.GetScore(_player1, _player2);
        }
	}
}

