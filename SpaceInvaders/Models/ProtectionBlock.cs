using Microsoft.UI.Xaml.Media.Imaging;

namespace SpaceInvaders.Presentation.Game;

public class ProtectionBlock
{
    private int _xPosition;
    private int _yPosition;
    private string _imagePath;
    private int _healt;
    private bool _isDestroyed;
    private Image? _protectionImage;

    public ProtectionBlock(int xPosition, int yPosition, string imagePath)
    {
        _xPosition = xPosition;
        _yPosition = yPosition;
        _imagePath = imagePath;
        _healt = 5;
        _isDestroyed = false;

    }
    public int XPosition { get { return _xPosition; } }
    public int YPosition { get { return _yPosition; } }

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

    /// <summary>
    /// removes a block when enemy or player collides
    /// </summary>
    /// <param name="_canvas"> canvas param passes for StartGame</param>
    public void RemoveBlock(Canvas _canvas)
    {
        _canvas.Children.Remove(_protectionImage);
        _protectionImage = null;
    }
    /// <summary>
    /// Add a Default block
    /// </summary>
    /// <param name="_canvas">canvas param passes for StartGame</param</param>
    public void AddBlock(Canvas _canvas)
    {
        _protectionImage = new Image
        {
            Source = new BitmapImage(new Uri(ImagePath)),
            Width = 130,
            Height = 130
        };
        Canvas.SetLeft(_protectionImage, XPosition);
        Canvas.SetTop(_protectionImage, YPosition);

        _canvas.Children.Add(_protectionImage);
    }

}
