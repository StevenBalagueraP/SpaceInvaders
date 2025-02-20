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
        /// <summary>
        /// Generate the image for bullet
        /// </summary>
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
        /// <summary>
        /// Move the bullet and validate collisions between different objects
        /// </summary>
        /// <param name="startGame">is the view</param>
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


        private bool CheckPlayerCollition(StartGame startGame)
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

        private bool CheckEnemyCollisions(StartGame startGame)
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
        private bool CheckBlockCollitions(StartGame startGame)
        {
            var blocks = startGame.ProtectionBlockManagerGame.ProtectionBlocks;

            for (int i = blocks.Count - 1; i >= 0; i--)
            {
                bool isCorrect = blocks[i].RemovePixel(_canvas, XPosition, YPosition);

                if (isCorrect)
                {
                    return true;
                }
            }
            return false;
        }

    }
}