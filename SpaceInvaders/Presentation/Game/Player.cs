using Microsoft.UI.Xaml.Media.Imaging;

namespace SpaceInvaders.Presentation.Game
{
    internal class Player
    {
        private int _x;
        private int _y;
        private string _imagePath;
        private Canvas _canvas;

        public Player(string imagePath, Canvas canvas)
        {
            _imagePath = imagePath;
            _canvas = canvas;
            CenterPlayer(); 
        }

        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public string ImagePath { get { return _imagePath; } }

        public void LoadImage()
        {
            Image playerImage = new Image
            {
                Source = new BitmapImage(new Uri(_imagePath)),
                Width = 50,
                Height = 50
            };

            Canvas.SetLeft(playerImage, _x);
            Canvas.SetTop(playerImage, _y);

            _canvas.Children.Add(playerImage);
        }

        private void CenterPlayer()
        {
            double canvasWidth = _canvas.ActualWidth;
            double canvasHeight = _canvas.ActualHeight;

            int playerWidth = 90;
            int playerHeight = 90;

            _x = (int)((canvasWidth - playerWidth) / 2);
            _y = (int)((canvasHeight - playerHeight) / 2) + 220;
        }
    }
}

