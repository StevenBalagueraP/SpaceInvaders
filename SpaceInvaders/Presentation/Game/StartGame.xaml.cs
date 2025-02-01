using Microsoft.UI.Xaml.Input;
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
        private Bullet _currentBullet;

        public Player PlayerGame
        {
            get { return _player; }
        }

        public EnemyManager EnemyManagerGame
        {
            get { return _enemyManager; }
        }

        public GameViewModel ViewModelGame
        {
            get { return _gameViewModel; }
        }
        public ProtectionBlockManager ProtectionBlockManagerGame
        {
            get { return _protectionBlockManager; }
        }

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
            _protectionBlockManager.GenerateBlock(1);
            _enemyManager.GenerateNewRound(5, 3);
            _player.LoadImage();
        }

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Space)
            {
                if (_currentBullet == null || !_currentBullet.IsActive)
                {
                    _currentBullet = new Bullet(_player.X, _player.Y, "ms-appx:///Assets/Images/bulletImage.png", GameCanvas);
                    _currentBullet.Move(this); 
                    
                }
                else
                {
                    Console.WriteLine("there is already an active bullet");
                }
            }
            else if (e.Key == VirtualKey.Left)
            {
                _player.MoveLeft();
            }
            else if (e.Key == VirtualKey.Right)
            {
                _player.MoveRight();
            }
        }
    }
}

