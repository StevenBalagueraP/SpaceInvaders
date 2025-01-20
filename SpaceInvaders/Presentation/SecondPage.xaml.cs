using SpaceInvaders.Presentation;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Controls;

namespace SpaceInvaders.Presentation;

public sealed partial class SecondPage : Page
{
    public SecondPage()
    {
        this.InitializeComponent();
    }
    private void GoToMain(object sender, RoutedEventArgs e)
    {
        Frame?.Navigate(typeof(MainPage));
    }
}

