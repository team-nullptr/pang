using NUnit.Framework;

public class DefaultScoreboardTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void DefaultScoreboardTestSimplePasses()
    {
		// Get the default scoreboard
        Scoreboard scoreboard = Scoreboard.DefaultScoreboard();

		// Check if the scoreboard is not null
		Assert.IsNotNull(scoreboard);

		// Check if the scoreboard has 10 rows
		Assert.AreEqual(10, scoreboard.rows.Length);

		// Check if every next row has lower score than the previous one
		for(int i = 0; i < 9; i++) {
			Assert.Greater(scoreboard.rows[i].score, scoreboard.rows[i + 1].score);
		}
    }
}
