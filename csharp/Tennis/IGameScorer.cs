namespace Tennis
{
	public interface IGameScorer
	{
		string GetScore(Player player1, Player player2);
		bool IsGameCompleted(Player player1, Player player2);
	}
}