using SpaceInvaders.ViewModel;
namespace SpaceInvaders.Presentation;

public sealed partial class MainPage : Page
{
    private DispatcherTimer _timer;
    List<User> users;
    MainPageViewModel mainPageViewModel;

    public MainPage()
    {
        this.InitializeComponent();
        mainPageViewModel = new MainPageViewModel();
        DataContext = mainPageViewModel;
        this.initializeStars();
        this.Loaded += MainPage_Loaded;
        users = new List<User>();
    }
    private void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
        MenuSound.MediaPlayer.Volume = 1.0;
        MenuSound.MediaPlayer.Play();
    }


    private void GoToSecond(object sender, RoutedEventArgs e)
    {
        Frame?.Navigate(typeof(StartGame));

    }
    private void initializeStars()
    {
        var shootingStar1 = new ShootingStar(
            x: 100,
            y: 0,
            speed: 50,
            imagePath: "ms-appx:///Assets/Images/ShootingStar_Menu.png",
            canvas: MainCanvas
        );

        var shootingStar2 = new ShootingStar(
            x: 800,
            y: 0,
            speed: 30,
            imagePath: "ms-appx:///Assets/Images/ShootingStar_Menu.png",
            canvas: MainCanvas
        );
        var shootingStar3 = new ShootingStar(
            x: 600,
            y: 0,
            speed: 40,
            imagePath: "ms-appx:///Assets/Images/ShootingStar_Menu.png",
            canvas: MainCanvas
        );
        var shootingStar4 = new ShootingStar(
            x: 300,
            y: 0,
            speed: 40,
            imagePath: "ms-appx:///Assets/Images/ShootingStar_Menu.png",
            canvas: MainCanvas
        );
        var shootingStar5 = new ShootingStar(
            x: 900,
            y: 0,
            speed: 40,
            imagePath: "ms-appx:///Assets/Images/ShootingStar_Menu.png",
            canvas: MainCanvas
        );
        var shootingStar6 = new ShootingStar(
            x: 0,
            y: 0,
            speed: 40,
            imagePath: "ms-appx:///Assets/Images/ShootingStar_Menu.png",
            canvas: MainCanvas
        );

        shootingStar1.AddToCanvas();
        shootingStar2.AddToCanvas();
        shootingStar3.AddToCanvas();
        shootingStar4.AddToCanvas();
        shootingStar5.AddToCanvas();
        shootingStar6.AddToCanvas();
    }

    /// <summary>
    /// receives user information and saves it in a list
    /// </summary>
    /// <param name="e">User Info</param>
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is User user)
        {
            users.Add(user);
            Console.WriteLine(users.Count);
        }
    }

    private void SocoreClickBtn(object sender, RoutedEventArgs e)
    {
        Frame?.Navigate(typeof(ScorePage), users);
    }
    private void AboutClickBtn(object sender, RoutedEventArgs e)
    {
        Frame?.Navigate(typeof(AboutPage));
    }
    
}
