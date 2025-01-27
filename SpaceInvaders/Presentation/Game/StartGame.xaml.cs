using SpaceInvaders.Presentation;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Controls;

namespace SpaceInvaders.Presentation;

public sealed partial class StartGame : Page
{
    public StartGame()
    {
        this.InitializeComponent();
    }
    private void GoToMain(object sender, RoutedEventArgs e)
    {
        Frame?.Navigate(typeof(MainPage));
    }
}

