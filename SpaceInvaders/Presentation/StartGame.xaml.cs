using Microsoft.UI.Xaml.Input;
using SpaceInvaders.Presentation.Game;
using SpaceInvaders.ViewModel;
using Windows.System;
using User = SpaceInvaders.Models.User;

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
            _enemyManager = new EnemyManager(GameCanvas, this);
            _enemyManager.MoveEnemies(this);
            _protectionBlockManager = new ProtectionBlockManager(GameCanvas);
            _player = new Player("ms-appx:///Assets/Images/playerSpaceShip.png", GameCanvas);
            _gameViewModel = new GameViewModel(_player);
            GenerateObjects();
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

        public void GenerateObjects()
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
                    _currentBullet = new Bullet(_player.X, _player.Y, "ms-appx:///Assets/Images/bulletImage.png", GameCanvas, true);
                    _currentBullet.Move(this); 
                    
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

        private void SaveScore_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(PlayerNameInput.Text, ViewModelGame.ScorePlayer.ScorePlayer);
            Console.WriteLine(user.Score);
            Frame?.Navigate(typeof(MainPage), user);
        }
        public void ValidateGameOver()
        {
            if (PlayerGame.Lives == 0)
            {
                GameOverPanel.Visibility = Visibility.Visible;
                _enemyManager.ResetTimers();
            }
        }
    }
}

