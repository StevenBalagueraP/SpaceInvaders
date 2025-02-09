
using Microsoft.UI.Xaml.Media.Imaging;

namespace SpaceInvaders.Models;

public abstract class Enemy
{
    private int _x;
    private int _y;
    private int _speed;
    private string _imagePath;
    private int _points;
    Image? enemyImage;

    public Enemy(int x, int y, int speed, string imagePath, int points)
    {
        _x = x;
        _y = y;
        _speed = speed;
        _imagePath = imagePath;
        _points = points;
    }

    public int X
    {
        get { return _x; }
        set { _x = value; }
    }
    public int Y
    {
        get { return _y; }
        set { _y = value; }
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

    public int Points
    {
        get { return _points; }
        set { _points = value; }
    }
    public Image? EnemyImage
    {
        get { return enemyImage; }
    }
    public void RemoveEnemy(Canvas canvas)
    {
        canvas.Children.Remove(enemyImage);
        enemyImage = null;
    }
    public void addEnemy(int enemyWidth, int enemyHeight, Canvas canvas)
    {
        enemyImage = new Image
        {
            Source = new BitmapImage(new Uri(ImagePath)),
            Width = enemyWidth,
            Height = enemyHeight
        };

        Canvas.SetLeft(enemyImage, X);
        Canvas.SetTop(enemyImage, Y);

        canvas.Children.Add(enemyImage);
    }
    public void Update(string value)
    {
        if (value == "positive") 
        {
            X += 1;
        }
        else
        {
            X -= 1;
        }
        
        Canvas.SetLeft(enemyImage, X);
       
    }
    
    public void increaseSpeed()
    {

    }
}
