
namespace SpaceInvaders.Models;

public class Score
{
    private int _scorePlayer;
    public int ScorePlayer
    {
        get { return _scorePlayer; }
    }
    public void IncrementScore(int score)
    {
        _scorePlayer += score;
    }
}
