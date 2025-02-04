using Microsoft.UI.Xaml.Media.Imaging;

namespace SpaceInvaders.Presentation.Game;

public class ProtectionBlock
{
    private int _x;
    private int _y;
    private string _imagePath;
    private int _healt;
    private bool _isDestroyed;
    private Image? _protectionImage;

    public ProtectionBlock(int x, int y, string imagePath)
    {
        _x = x;
        _y = y;
        _imagePath = imagePath;
        _healt = 5;
        _isDestroyed = false;

    }
    public int X { get { return _x; } }
    public int Y { get { return _y; } }

    public string ImagePath { get { return _imagePath; } }

    public int Healt
    {
        get { return _healt; }
        set { _healt = value; }
    }

    public bool IsDestroyed { 
        get { return _isDestroyed; }
        set { _isDestroyed = value; }
    }
    public void RemoveBlock(Canvas _canvas)
    {
        _canvas.Children.Remove(_protectionImage);
        _protectionImage = null;
    }
    public void AddBlock(Canvas _canvas)
    {
        _protectionImage = new Image
        {
            Source = new BitmapImage(new Uri(ImagePath)),
            Width = 130,
            Height = 130
        };
        Canvas.SetLeft(_protectionImage, X);
        Canvas.SetTop(_protectionImage, Y);

        _canvas.Children.Add(_protectionImage);
    }

}
