
namespace SpaceInvaders.Presentation.Game;

internal abstract class Enemy
{
    private int _x;
    private int _y;
    private int _speed;
    private string _imagePath;
    private int _points;

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



    public void move()
    {

    }
    public void Update()
    {

    }
    public void reset()
    {

    }
    public void increaseSpeed()
    {

    }
}
