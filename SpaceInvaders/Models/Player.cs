using Microsoft.UI.Xaml.Media.Imaging;

namespace SpaceInvaders.Presentation.Game;

public class Player
{
    private int _x;
    private int _y;
    private string _imagePath;
    private Canvas _canvas;
    private int _lives;
    private Image playerImage;
    private bool isAlive;

    public Player(string imagePath, Canvas canvas)
    {
        _imagePath = imagePath;
        _canvas = canvas;
        CenterPlayer(); 
        _lives = 3;
        isAlive = true;
    }

    public bool IsAlive
    {
        get { return isAlive; }
        set { isAlive = value; }
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
    public void resetPlayer()
    {
        _x = 200;
        _y = 600 - 90;
        Canvas.SetLeft(playerImage, _x);
        Canvas.SetTop(playerImage, _y);

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
        if (isAlive)
        {
            X -= 10;
            if (X <= -300)
            {
                X = 640;
            }
            LoadImage();
        }
        
    }
    public void MoveRight()
    {
        if (isAlive)
        {
            X += 10;


            if (X >= 640)
            {
                X = -300;
            }
            LoadImage();
        }
        
    }
    public void RemovePlayer(Canvas canvas)
    {
        if (playerImage != null)
        {
            canvas.Children.Remove(playerImage);
            playerImage.Source = null;
            playerImage = null;
            canvas.UpdateLayout(); // <-- Forzar actualización de la UI
        }
    }
}

