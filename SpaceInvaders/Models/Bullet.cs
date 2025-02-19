using Microsoft.UI.Xaml.Media.Imaging;
using SpaceInvaders.ViewModel;

namespace SpaceInvaders.Presentation.Game
{
    class Bullet
    {
        private int _xPosition;
        private int _yPosition;
        private double _speed;
        private bool _isActive;
        private string _imagePath;
        private Canvas _canvas;
        private Image bulletImage;
        private DispatcherTimer _timer;
        private bool _isPlayerBullet;

        public Bullet(int x, int y, string imagePath, Canvas canvas, bool isPlayerBuller)
        {
            _xPosition = x;
            _yPosition = y;
            _speed = 12.5;
            _isActive = false;
            _imagePath = imagePath;
            _canvas = canvas;
            _timer = new DispatcherTimer();
            _isPlayerBullet = isPlayerBuller;
        }

        public int XPosition => _xPosition;
        public int YPosition
        {
            get { return _yPosition; }
            set { _yPosition = value; }
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

                Canvas.SetLeft(bulletImage, XPosition + 5);
                Canvas.SetTop(bulletImage, YPosition);
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
                    
                    if (YPosition <= 0 || YPosition >= 550)
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
                            YPosition -= 5;
                            Canvas.SetTop(bulletImage, YPosition);
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
                            YPosition += 5;
                        }
                    }
                        
                        Canvas.SetTop(bulletImage, YPosition);
                };

                if (!_timer.IsEnabled) _timer.Start(); 
            }
        }


        public bool CheckPlayerCollition(StartGame startGame)
        {
            bool intervalCollitionX = startGame.PlayerGame.X >= XPosition - 20 && startGame.PlayerGame.X <= XPosition + 20;
            bool intervalCollitionY = startGame.PlayerGame.Y >= YPosition - 20 && startGame.PlayerGame.Y <= YPosition + 20;

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

                startGame.ValidateGameOver();
                return true;
            }
            return false;
        }

        public bool CheckEnemyCollisions(StartGame startGame)
        {
            foreach (Enemy enemy in startGame.EnemyManagerGame.Enemies)
            {
                bool intervalCollitionX = enemy.XPosition >= XPosition - 20 && enemy.XPosition <= XPosition + 20;
                bool intervalCollitionY = enemy.YPosition >= YPosition - 20 && enemy.YPosition <= YPosition + 20;

                if (intervalCollitionX && intervalCollitionY)
                {
                    startGame.ViewModelGame.IncreaseScore(enemy.Points);
                    startGame.EnemyManagerGame.Enemies.Remove(enemy);
                    startGame.EnemyManagerGame.ResetEnemies(startGame);
                    startGame.PlayerGame.IncreaseLives(startGame.ViewModelGame.ScorePlayer.ScorePlayer);
                    startGame.ViewModelGame.UpdateLife(startGame.PlayerGame.Lives);
                    enemy.RemoveEnemy(_canvas);
                    enemy.IsRemoved = true;

                    startGame.ValidateGameOver();
                    return true;
                }
            
            }
            return false;
        }
        public bool CheckBlockCollitions(StartGame startGame)
        {
            foreach (ProtectionBlock protectionBlock in startGame.ProtectionBlockManagerGame.ProtectionBlocks)
            {
                bool intervalCollitionX = protectionBlock.XPosition >= XPosition - 80 && protectionBlock.XPosition <= XPosition + 5;
                bool intervalCollitionY = protectionBlock.YPosition >= YPosition - 65 && protectionBlock.YPosition <= YPosition + 16;

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
    }
}