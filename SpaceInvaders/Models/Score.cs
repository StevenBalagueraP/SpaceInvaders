
namespace SpaceInvaders.Models
{
    internal class Score
    {
        private int _scorePlayer;
        public int ScorePlayer
        {
            get { return _scorePlayer; }
        }
        public void incrementScore(int score)
        {
            _scorePlayer += score;
        }
    }
}
