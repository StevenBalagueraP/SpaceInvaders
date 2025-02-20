using Microsoft.UI.Xaml.Media.Imaging;

namespace SpaceInvaders.Presentation.Game;

public class Player
{
    private int _x;
    private int _y;
    private string _imagePath;
    private Canvas _canvas;
    private int _lives;
    private int _lastMilestone;
    private Image playerImage;
    private bool isAlive;

    public Player(string imagePath, Canvas canvas)
    {
        _imagePath = imagePath;
        _canvas = canvas;
        CenterPlayer(); 
        _lives = 3;
        isAlive = true;
        _lastMilestone = 0;
    }

    public bool IsAlive
    {
        get { return isAlive; }
        set { isAlive = value; }
    }
    public int X 
    { 
        get { return _x; }
        set { _x = value; }
    }
    public int Y 
    {
        get { return _y; } 
    }
    public string ImagePath { get { return _imagePath; } }

    public int Lives
    {
        get { return _lives; }
        set { _lives = value; }
    }

    /// <summary>
    /// Generate the image for a player
    /// </summary>
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

    /// <summary>
    /// Reset the player position when the player collides with a enemy bullet
    /// </summary>
    public void resetPlayer()
    {
        _x = 200;
        _y = 510;
        Canvas.SetLeft(playerImage, _x);
        Canvas.SetTop(playerImage, _y);

    }


    private void CenterPlayer()
    {
        double canvasWidth = _canvas.ActualWidth;
        double canvasHeight = _canvas.ActualHeight;

        int playerHeight = 90;

        _x = 200;
        _y = 600 - playerHeight;
    }
    /// <summary>
    /// the player moves to the left when he presses the left key
    /// </summary>
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
    /// <summary>
    /// the player moves to the right when he presses the right key
    /// </summary>
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

    /// <summary>
    /// remove player to the screen when its lives arrives to 0
    /// </summary>
    /// <param name="canvas"> param passed on StartGame</param>
    public void RemovePlayer(Canvas canvas)
    {
        if (playerImage != null)
        {
            canvas.Children.Remove(playerImage);
            playerImage.Source = null;
            playerImage = null;
            canvas.UpdateLayout(); 
        }
    }

    /// <summary>
    /// Increase the playerLives each 1000 score
    /// </summary>
    /// <param name="score">score passed to the view</param>
    public void IncreaseLives(int score)
    {
        if (score / 1000 > _lastMilestone && Lives < 6)
        {
            _lives++;
            _lastMilestone = score / 1000; 
        }
    }
}

