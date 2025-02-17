using Microsoft.UI.Xaml.Media.Imaging;
using SpaceInvaders.ViewModel;

namespace SpaceInvaders.Presentation.Game
{
    class Bullet
    {
        private int _x;
        private int _y;
        private double _speed;
        private bool _isActive;
        private string _imagePath;
        private Canvas _canvas;
        private Image bulletImage;
        private DispatcherTimer _timer;
        private bool _isPlayerBullet;

        public Bullet(int x, int y, string imagePath, Canvas canvas, bool isPlayerBuller)
        {
            _x = x;
            _y = y;
            _speed = 12.5;
            _isActive = false;
            _imagePath = imagePath;
            _canvas = canvas;
            _timer = new DispatcherTimer();
            _isPlayerBullet = isPlayerBuller;
        }

        public int X => _x;
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public double Speed => _speed;
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        public string ImagePath => _imagePath;

        public void GenerateImage()
        {
            if (!IsActive)
            {
                IsActive = true;

                if (bulletImage == null)
                {
                    bulletImage = new Image
                    {
                        Source = new BitmapImage(new Uri(ImagePath)),
                        Width = 40,
                        Height = 40
                    };
                }

                Canvas.SetLeft(bulletImage, X + 5);
                Canvas.SetTop(bulletImage, Y);
                _canvas.Children.Add(bulletImage);
            }
        }

        public void Move(StartGame startGame)
        {
            GenerateImage();

            if (IsActive)
            {
                _timer.Interval = TimeSpan.FromMilliseconds(10);
                _timer.Tick += (sender, e) =>
                {
                    
                    if (Y <= 0 || Y >= 550)
                    {
                        _timer.Stop();
                        _canvas.Children.Remove(bulletImage);
                        IsActive = false;
                    }
                    else if (_isPlayerBullet)
                    {
                        if (CheckEnemyCollisions(startGame))
                        {
                            _timer.Stop();
                            _canvas.Children.Remove(bulletImage);
                            startGame.DamageEnemiesSound();
                            IsActive = false;
                        }
                        else if (CheckBlockCollitions(startGame))
                        {
                            _timer.Stop();
                            _canvas.Children.Remove(bulletImage);
                            startGame.ProtectionBlockSound();
                            IsActive = false;
                        }
                        else
                        {
                            Y -= 5;
                            Canvas.SetTop(bulletImage, Y);
                        }
                    }
                    else if (!_isPlayerBullet)
                    {
                        if (CheckBlockCollitions(startGame))
                        {
                            _timer.Stop();
                            _canvas.Children.Remove(bulletImage);
                            startGame.ProtectionBlockSound();
                            IsActive = false;
                        }
                        else if (CheckPlayerCollition(startGame))
                        {
                            _timer.Stop();
                            _canvas.Children.Remove(bulletImage);

                            startGame.ViewModelGame.UpdateLife(startGame.PlayerGame.Lives);
                            IsActive = false;

                        }
                        else
                        {
                            Y += 5;
                        }
                    }
                        
                        Canvas.SetTop(bulletImage, Y);
                };

                if (!_timer.IsEnabled) _timer.Start(); 
            }
        }
        public bool CheckPlayerCollition(StartGame startGame)
        {
            bool intervalCollitionX = startGame.PlayerGame.X >= X - 20 && startGame.PlayerGame.X <= X + 20;
            bool intervalCollitionY = startGame.PlayerGame.Y >= Y - 20 && startGame.PlayerGame.Y <= Y + 20;

            if (intervalCollitionX && intervalCollitionY) 
            {
                if (startGame.PlayerGame.Lives > 1)
                {
                   startGame.PlayerGame.Lives--;
                   startGame.PlayerGame.resetPlayer();
                }
                else
                {
                    startGame.PlayerGame.RemovePlayer(_canvas);
                    startGame.PlayerGame.IsAlive = false;
                    startGame.PlayerGame.Lives--;
                }

                ValidateGameOver(startGame);
                return true;
            }
            return false;
        }

        public bool CheckEnemyCollisions(StartGame startGame)
        {
            foreach (Enemy enemy in startGame.EnemyManagerGame.Enemies)
            {
                bool intervalCollitionX = enemy.X >= X - 20 && enemy.X <= X + 20;
                bool intervalCollitionY = enemy.Y >= Y - 20 && enemy.Y <= Y + 20;

                if (intervalCollitionX && intervalCollitionY)
                {
                    startGame.ViewModelGame.IncreaseScore(enemy.Points);
                    startGame.EnemyManagerGame.Enemies.Remove(enemy);
                    startGame.EnemyManagerGame.ResetEnemies(startGame);
                    enemy.RemoveEnemy(_canvas);
                    enemy.IsRemoved = true;
                    
                    ValidateGameOver(startGame);
                    return true;
                }
            
            }
            return false;
        }
        public bool CheckBlockCollitions(StartGame startGame)
        {
            foreach (ProtectionBlock protectionBlock in startGame.ProtectionBlockManagerGame.ProtectionBlocks)
            {
                bool intervalCollitionX = protectionBlock.X >= X - 80 && protectionBlock.X <= X + 5;
                bool intervalCollitionY = protectionBlock.Y >= Y - 65 && protectionBlock.Y <= Y + 16;

                if (intervalCollitionX && intervalCollitionY)
                {
                    if (protectionBlock.Healt < 2)
                    {
                        protectionBlock.RemoveBlock(_canvas);
                        startGame.ProtectionBlockManagerGame.ProtectionBlocks.Remove(protectionBlock);
                        return true;
                    }
                    else
                    {
                        protectionBlock.Healt -= 1;
                        return true;

                    } 
                }

            }
            return false;
        }
        public void ValidateGameOver(StartGame startGame)
        {
            if (!startGame.PlayerGame.IsAlive)
            {
                startGame.GameOverSound();
                startGame.Frame?.Navigate(typeof(MainPage), startGame.ViewModelGame.ScorePlayer.ScorePlayer);
                
            }
        }
    }
}