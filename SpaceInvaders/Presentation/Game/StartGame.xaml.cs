using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using SpaceInvaders.Presentation.Game;
using SpaceInvaders.ViewModel;
using Windows.System;

namespace SpaceInvaders.Presentation
{
    public sealed partial class StartGame : Page
    {
        private GameViewModel _gameViewModel;
        private EnemyManager _enemyManager;
        private Player _player;
        private ProtectionBlockManager _protectionBlockManager;

        public StartGame()
        {
            this.InitializeComponent();
            _enemyManager = new EnemyManager(GameCanvas);
            _protectionBlockManager = new ProtectionBlockManager(GameCanvas);
            _player = new Player("ms-appx:///Assets/Images/playerSpaceShip.png", GameCanvas);
            _gameViewModel = new GameViewModel(_player);
            generateObjects();
            DataContext = _gameViewModel;
            gridGame.Focus(FocusState.Programmatic);


        }

        public void generateObjects()
        {
            _protectionBlockManager.GenerateBlock(4);
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

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Space) 
            {
                Console.WriteLine("disparo!");
            }
            else
            {
                Console.WriteLine($"{e.Key}");
            }
        }
    }
}

