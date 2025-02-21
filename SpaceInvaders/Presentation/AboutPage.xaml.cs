namespace SpaceInvaders.Presentation;

public sealed partial class AboutPage : Page
{
    public AboutPage()
    {
        this.InitializeComponent();
    }

    private void GoToHomePage_Click(object sender, RoutedEventArgs e)
    {
        Frame?.Navigate(typeof(MainPage));
    }
}
