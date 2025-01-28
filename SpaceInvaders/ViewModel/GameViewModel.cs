using System.ComponentModel;

namespace SpaceInvaders.ViewModel
{
    internal class GameViewModel : INotifyPropertyChanged
    {
        private Score _score;
        public event PropertyChangedEventHandler? PropertyChanged;

        public GameViewModel()
        {
            _score = new Score();
        }

        public string ScoreText
        {
            get => $"Score: {_score.ScorePlayer}";
        }

        public void IncreaseScore()
        {
            _score.incrementScore();
            Console.WriteLine(ScoreText);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScoreText)));

        }
    }
}
