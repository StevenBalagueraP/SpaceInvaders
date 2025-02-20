using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI.Xaml;
using Microsoft.UI;
namespace SpaceInvaders.Presentation.Game;

public class ProtectionBlock
{
    private int _xPosition;
    private int _yPosition;
    private string _imagePath;
    private int _healt;
    private bool _isDestroyed;
    private Image? _protectionImage;
    private int[,] matriz;
    private int pixelSize;

    public ProtectionBlock(int xPosition, int yPosition, string imagePath)
    {
        _xPosition = xPosition;
        _yPosition = yPosition;
        _imagePath = imagePath;
        _healt = 5;
        pixelSize = 20;
        _isDestroyed = false;
        matriz = new int[,]
       {
            { 1, 1, 1, 1 },
            { 1, 1, 1, 1 },
            { 1, 0, 0, 1 }
       };

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
    public bool RemovePixel(Canvas _canvas, double hitX, double hitY)
    {
        // Ajuste en el eje X
        hitX -= -10;

        int column = (int)((hitX - _xPosition) / pixelSize);
        int row = (int)((hitY - _yPosition) / pixelSize);

        if (row >= 0 && row < matriz.GetLength(0) &&
            column >= 0 && column < matriz.GetLength(1) &&
            matriz[row, column] == 1)
        {
            matriz[row, column] = 0;

            Rectangle? pixelToRemove = null;

            foreach (var element in _canvas.Children)
            {
                if (element is Rectangle rect)
                {
                    double left = Canvas.GetLeft(rect);
                    double top = Canvas.GetTop(rect);

                    if (left == _xPosition + column * pixelSize &&
                        top == _yPosition + row * pixelSize)
                    {
                        pixelToRemove = rect;
                        break;
                    }
                }
            }

            if (pixelToRemove != null)
            {
                _canvas.Children.Remove(pixelToRemove);
                return true;
            }
        }

        return false;
    }



    /// <summary>
    /// Add a Default block
    /// </summary>
    /// <param name="_canvas">canvas param passes for StartGame</param</param>
    public void AddBlock(Canvas _canvas)
    {
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                if (matriz[i, j] == 1) // Solo dibujar si es un bloque (1)
                {
                    Rectangle pixel = new Rectangle
                    {
                        Width = pixelSize,
                        Height = pixelSize,
                        Fill = new SolidColorBrush(Colors.Green),
                        Stroke = new SolidColorBrush(Colors.Black),
                        StrokeThickness = 1
                    };

                    Canvas.SetLeft(pixel, XPosition + j * pixelSize);
                    Canvas.SetTop(pixel, YPosition + i * pixelSize);
                    _canvas.Children.Add(pixel);
                }
            }
        }
    }

}
