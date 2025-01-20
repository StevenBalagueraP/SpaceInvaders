using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace SpaceInvaders.Presentation;

public sealed partial class MainPage : Page
{
    private DispatcherTimer _timer;
    private double _playerYPosition;

    public MainPage()
    {
        this.InitializeComponent();

        // Inicializar posición del jugador
        _playerYPosition = Canvas.GetTop(PlayerImage);

        // Configurar y arrancar el temporizador
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(50) // Velocidad de actualización
        };
        _timer.Tick += OnTimerTick;
        _timer.Start();
    }

    private void OnTimerTick(object sender, object e)
    {
        // Mover hacia arriba disminuyendo la posición Y
        _playerYPosition += 5;

        // Si llega al borde superior, reinicia la posición
        if (_playerYPosition >= 500)
        {
            _playerYPosition = 0; // Posición inicial (ajusta según tu diseño)
        }

        // Actualizar la posición de la imagen en el Canvas
        Canvas.SetTop(PlayerImage, _playerYPosition);
    }

    private void GoToSecond(object sender, RoutedEventArgs e)
    {
        Frame?.Navigate(typeof(SecondPage));
    }
}
