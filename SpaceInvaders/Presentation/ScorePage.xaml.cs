

namespace SpaceInvaders.Presentation;

partial class ScorePage : Page
{
    List<User> _users;
    public ScorePage()
    {
        InitializeComponent();
        _users = new List<User>();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is List<User> users)
        {
            foreach (User user in users)
            {
                if (!_users.Contains(user))
                {
                    _users.Add(user);
                }
                
            }
        }
        UserListView.ItemsSource = null; 
        UserListView.ItemsSource = _users;
        Console.WriteLine($"Users: {_users.Count}");
    }

    private void BtnShowBest_Click_1(object sender, RoutedEventArgs e)
    {

    }

    private void BtnShowAll_Click(object sender, RoutedEventArgs e)
    {

    }

    private void GoToMain_Click(object sender, RoutedEventArgs e)
    {
        Frame?.Navigate(typeof(MainPage));
    }
}
