using Microsoft.UI.Xaml.Media.Imaging;

namespace SpaceInvaders.Presentation.Game
{
    internal class Player
    {
        private int _x;
        private int _y;
        private string _imagePath;
        private Canvas _canvas;
        private int _lives;
        private Image playerImage;

        public Player(string imagePath, Canvas canvas)
        {
            _imagePath = imagePath;
            _canvas = canvas;
            CenterPlayer(); 
            _lives = 3;
        }

        public int X { 
            get { return _x; }
            set { _x = value; }
        }
        public int Y { get { return _y; } }
        public string ImagePath { get { return _imagePath; } }

        public int Lives
        {
            get { return _lives; }
            set { _lives = value; }
        }

        public void LoadImage()
        {

            if (playerImage == null)
            {
                playerImage = new Image
                {
                    Source = new BitmapImage(new Uri(_imagePath)),
                    Width = 50,
                    Height = 50
                };
            }
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

            _x = 200;
            _y = 600 - playerHeight;
        }
        public void MoveLeft()
        {
            X -= 10;
            LoadImage();
            if (X <= -300)
            {
                X = 640 ;
            }
        }
        public void MoveRight()
        {
            X += 10;
            LoadImage();

            if (X >= 640)
            {
                X = -300; 
            }
        }
    }
}

