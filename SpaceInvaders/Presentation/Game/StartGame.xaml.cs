using SpaceInvaders.Presentation;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Controls;
using SpaceInvaders.Presentation.Game;

namespace SpaceInvaders.Presentation;

public sealed partial class StartGame : Page
{
    EnemyManager _enemyManager;
    public StartGame()
    {
        this.InitializeComponent();
        _enemyManager = new EnemyManager(GameCanvas);
        _enemyManager.GenerateNewRound(5, 15);
    }
    private void GoToMain(object sender, RoutedEventArgs e)
    {
        Frame?.Navigate(typeof(MainPage));
    }
}

