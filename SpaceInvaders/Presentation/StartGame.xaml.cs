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
            this.Loaded += StartGame_Loaded;
            _enemyManager = new EnemyManager(GameCanvas);
            _enemyManager.MoveEnemies();
            _protectionBlockManager = new ProtectionBlockManager(GameCanvas);
            _player = new Player("ms-appx:///Assets/Images/playerSpaceShip.png", GameCanvas);
            _gameViewModel = new GameViewModel(_player);
            generateObjects();
            DataContext = _gameViewModel;
            gridGame.Focus(FocusState.Programmatic);


        }
        private void StartGame_Loaded(object sender, RoutedEventArgs e)
        {
            StartGameSound.MediaPlayer.Volume = 1.0;
            StartGameSound.MediaPlayer.Play();
        }
        public void DamageEnemiesSound()
        {
            DamageEnemies.MediaPlayer.Volume = 1.0;
            DamageEnemies.MediaPlayer.Play();
        }
        public void ShootingSoundMedia()
        {
            ShootingSound.MediaPlayer.Volume = 1.0;
            ShootingSound.MediaPlayer.Play();
        }
        public void ProtectionBlockSound()
        {
            ProtectionBlockDamage.MediaPlayer.Volume = 1.0;
            ProtectionBlockDamage.MediaPlayer.Play();
        }
        public void ResetEnemiesSound()
        {
            ResetEnemies.MediaPlayer.Volume = 1.0;
            ResetEnemies.MediaPlayer.Play();
        }
        public void GameOverSound()
        {
            GameOver.MediaPlayer.Volume = 1.0;
            GameOver.MediaPlayer.Play();
        }

        public void generateObjects()
        {
            _protectionBlockManager.GenerateBlock(4);
            _enemyManager.GenerateNewRound(5, 15);
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

