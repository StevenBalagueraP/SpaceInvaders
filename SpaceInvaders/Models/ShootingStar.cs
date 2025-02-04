
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Models
{
    internal class ShootingStar
    {
        private int _x;
        private int _y;
        private int _speed;
        private string _imagePath;
        private DispatcherTimer _timer;
        private Canvas _canvas;
        private Image _startImage;
        private Random _random;

        public ShootingStar(int x, int y, int speed, string imagePath, Canvas canvas)
        {
            _x = x;
            _y = y;
            _speed = speed;
            _imagePath = imagePath;
            _canvas = canvas;
            _random = new Random();
        }
        public void AddToCanvas()
        {
            _startImage = new Image
            {
                Source = new BitmapImage(new Uri(_imagePath)),
                Width = 100,
                Height = 100
            };
            Canvas.SetLeft(_startImage, _x);
            Canvas.SetTop(_startImage, _y);

            _canvas.Children.Add(_startImage);
            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(_speed)
            };
            _timer.Tick += OnTimerTick;
            _timer.Start();

        }
        public void OnTimerTick(object sender, object b)
        {
            double currentTop = Canvas.GetTop(_startImage);
            Canvas.SetTop(_startImage, currentTop + 5);

            if (currentTop > _canvas.ActualHeight)
            {
                ResetPosition();
            }
        }
        public void ResetPosition()
        {
            int maxX = Math.Max(0, (int)_canvas.ActualWidth - 100);

            double randomX = _random.Next(0, maxX + 1);
            Canvas.SetLeft(_startImage, randomX);
            Canvas.SetTop(_startImage, 0);

        }

    }
}
