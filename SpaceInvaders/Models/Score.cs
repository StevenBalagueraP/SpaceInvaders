
namespace SpaceInvaders.Models;

public class Score
{
    private int _scorePlayer;
    public int ScorePlayer
    {
        get { return _scorePlayer; }
    }
    /// <summary>
    /// Increases the player's score when the player's bullet collides with an enemy
    /// </summary>
    public void IncrementScore(int score)
    {
        _scorePlayer += score;
    }
}
