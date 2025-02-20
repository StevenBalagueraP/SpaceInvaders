
using Microsoft.UI.Xaml.Media.Imaging;

namespace SpaceInvaders.Models;

public abstract class Enemy
{
    private int _xPosition;
    private int _yPosition;
    private int _speed;
    private string _imagePath;
    private int _points;
    private Image? enemyImage;
    private bool isRemoved;
    public Enemy(int xPosition, int yPosition, int speed, string imagePath, int points)
    {
        _xPosition = xPosition;
        _yPosition = yPosition;
        _speed = speed;
        _imagePath = imagePath;
        _points = points;
        isRemoved = false;
    }

    public int XPosition
    {
        get { return _xPosition; }
        set { _xPosition = value; }
    }
    public int YPosition
    {
        get { return _yPosition; }
        set { _yPosition = value; }
    }
    public int Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public string ImagePath
    {
        get { return _imagePath; }
        set { _imagePath = value; }
    }

    public bool IsRemoved
    {
        get { return isRemoved; }
        set {  isRemoved = value; }
    }

    public int Points
    {
        get { return _points; }
        set { _points = value; }
    }
    public Image? EnemyImage
    {
        get { return enemyImage; }
        set { enemyImage = value; }
    }
    /// <summary>
    /// Remove enemy for canvas
    /// </summary>
    /// <param name="canvas">is the parameter passed on StartGame</param>
    public void RemoveEnemy(Canvas canvas)
    {
        canvas.Children.Remove(enemyImage);
        enemyImage = null;
    }

    /// <summary>
    /// add enemy for canvas
    /// </summary>
    /// <param name="enemyWidth">width for enemy</param>
    /// <param name="enemyHeight">heigth for enemy</param>
    /// <param name="canvas">is the parameter passed on StartGame</param>
    public void AddEnemy(int enemyWidth, int enemyHeight, Canvas canvas)
    {
        enemyImage = new Image
        {
            Source = new BitmapImage(new Uri(ImagePath)),
            Width = enemyWidth,
            Height = enemyHeight
        };

        Canvas.SetLeft(enemyImage, XPosition);
        Canvas.SetTop(enemyImage, YPosition);

        canvas.Children.Add(enemyImage);
    }

    /// <summary>
    /// check the edges and move the enemy
    /// </summary>
    /// <param name="value"> value can be positive or negative</param>
    /// <param name="canvas">is the parameter passed on StartGame</param>
    /// <returns></returns>
    public bool Update(string value, Canvas canvas)
    {
        if (value == "positive")
        {
            XPosition += 1;
            
            if (this is BossEnemy)
            {
          
                if (XPosition >= 680)
                {
                    isRemoved = true;
                }
            }
        }
        else
        {
            XPosition -= 1;
            if (this is BossEnemy)
            {
                if (XPosition <= -350)
                {
                    isRemoved = true;
                }

            }
        }
        if (!isRemoved)
        {
            Canvas.SetLeft(enemyImage, XPosition);
        }
        
        return isRemoved;

        
       
    }
}
