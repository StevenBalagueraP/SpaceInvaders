using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace SpaceInvaders.ViewModel;

public class MainPageViewModel : INotifyPropertyChanged
{
    private Score _score;
    public event PropertyChangedEventHandler? PropertyChanged;

    public MainPageViewModel()
    {
        _score = new Score();
    }

    public String MaxScoreText
    {
        get => $"MaxScore: {_score.ScorePlayer}";
    }
    public void IncreaseScore(int score)
    {
        _score.incrementScore(score);
        Console.WriteLine($"[DEBUG] Incrementando score: {score}");
        OnpropertyChanged(nameof(MaxScoreText));
        
    }
 
    public void OnpropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
