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

        public Bullet(int x, int y, string imagePath, Canvas canvas)
        {
            _x = x;
            _y = y;
            _speed = 12.5;
            _isActive = false;
            _imagePath = imagePath;
            _canvas = canvas;
            _timer = new DispatcherTimer();
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

        public void Move(EnemyManager enemyManager, GameViewModel gameViewModel)
        {
            GenerateImage();

            if (IsActive)
            {
                _timer.Interval = TimeSpan.FromMilliseconds(10); // Tiempo fijo para el movimiento
                _timer.Tick += (sender, e) =>
                {
                    if (Y <= 0)
                    {
                        _timer.Stop();
                        _canvas.Children.Remove(bulletImage);
                        IsActive = false;
                    }
                    else if (CheckCollisions(enemyManager, gameViewModel))
                    {
                        _timer.Stop();
                        _canvas.Children.Remove(bulletImage);
                        IsActive = false;
                    }
                    else
                    {
                        Y -= 5;
                        Canvas.SetTop(bulletImage, Y);
                    }
                };

                if (!_timer.IsEnabled) _timer.Start(); 
            }
        }

        public bool CheckCollisions(EnemyManager enemyManager, GameViewModel gameViewModel)
        {
            foreach (Enemy enemy in enemyManager.Enemies)
            {
                if (enemy.X >= X - 20 && enemy.X <= X + 20 && enemy.Y >= Y - 20 && enemy.Y <= Y + 20)
                {
                    gameViewModel.IncreaseScore(enemy.Points);
                    enemy.RemoveEnemy(_canvas);
                    enemyManager.Enemies.Remove(enemy);
                    enemyManager.ResetEnemies();
                    Console.WriteLine(enemyManager.Enemies.Count);
                    return true;
                }
            }
            return false;
        }
    }
}