using System.ComponentModel;
namespace SpaceInvaders.ViewModel;

public class MainPageViewModel : INotifyPropertyChanged
{
    private Score _score;
    public event PropertyChangedEventHandler? PropertyChanged;

    public MainPageViewModel()
    {
        _score = new Score();
    }

    public string MaxScoreText
    {
        get => $"MaxScore: {_score.ScorePlayer}";
    }
    public void IncreaseScore(int score)
    {
        _score.IncrementScore(score);
        Console.WriteLine($"[DEBUG] Incrementando score: {score}");
        OnpropertyChanged(nameof(MaxScoreText));
        Console.WriteLine(_score.ScorePlayer);


    }
 
    public void OnpropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
