using SpaceInvaders.Presentation.Game;
using System.ComponentModel;

namespace SpaceInvaders.ViewModel
{
    internal class GameViewModel : INotifyPropertyChanged
    {
        private Score _score;
        private Player _player;
        public event PropertyChangedEventHandler? PropertyChanged;

        public GameViewModel(Player player)
        {
            _score = new Score();
            _player = player;
        }

        public string ScoreText
        {
            get => $"Score: {_score.ScorePlayer}";
        }
        public string LifeText
        {
            get => $"Lives: {_player.Lives}";
        }

        public void IncreaseScore(int score)
        {
            _score.incrementScore(score);
            OnpropertyChanged(nameof(ScoreText));
            Console.WriteLine(ScoreText);
        }
        public void UpdateLife(int lives)
        {
            _player.Lives = lives;
            OnpropertyChanged(nameof(LifeText));
        }
        public void OnpropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
