using SpaceInvaders.Presentation;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Controls;
using SpaceInvaders.Presentation.Game;
using SpaceInvaders.ViewModel;

namespace SpaceInvaders.Presentation;

public sealed partial class StartGame : Page
{
    private GameViewModel _gameViewModel;
    private EnemyManager _enemyManager;
    private Player _player;  
    public StartGame()
    {
        this.InitializeComponent();
        _gameViewModel = new GameViewModel();
        DataContext = _gameViewModel;
        _enemyManager = new EnemyManager(GameCanvas);
        _player = new Player("ms-appx:///Assets/Images/playerSpaceShip.png", GameCanvas);
        _enemyManager.GenerateNewRound(5, 15);
        _player.LoadImage();
    }
    private void GoToMain(object sender, RoutedEventArgs e)
    {
        Frame?.Navigate(typeof(MainPage));
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        _gameViewModel.IncreaseScore();


    }
}

