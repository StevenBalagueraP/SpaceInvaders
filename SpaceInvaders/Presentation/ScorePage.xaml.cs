using Xamarin.Essentials;

namespace SpaceInvaders.Presentation;

partial class ScorePage : Page
{
    List<User> _users;
    public ScorePage()
    {
        InitializeComponent();
        _users = new List<User>();
    }


    /// <summary>
    /// Validates if the user exists and if it exists, it overwrites its score only if the new score
    /// is higher than the current one. Otherwise, the added score does not exist.
    /// </summary>
    /// <param name="e"> User list</param>
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is List<User> users)
        {
            foreach (User user in users)
            {
                if (_users.Any(u => u.Name == user.Name))
                {
                    UpdateScore(user.Name, user.Score);
                    Console.WriteLine($"Puntaje actualizado para {user.Name}");
                }
                else
                {
                    _users.Add(user);
                    Console.WriteLine($"Usuario agregado: {user.Name}");
                }
            }
        }
        var topUsers = _users.OrderByDescending(user => user.Score).Take(5).ToList();
        UserListView.ItemsSource = null;
        UserListView.ItemsSource = topUsers;
        Console.WriteLine($"Users: {_users.Count}");
    }

    /// <summary>
    /// Updates the score only if the new score is higher than the current one
    /// </summary>
    /// <param name="name">User Name</param>
    /// <param name="newScore">New Score for User</param>
    public void UpdateScore(string name, int newScore)
    {
        var user = _users.FirstOrDefault(u => u.Name == name);
        if (user != null && newScore > user.Score)
        {
            user.Score = newScore;
        }
    }

    private void BtnShowBest_Click_1(object sender, RoutedEventArgs e)
    {
        if (IsEmptyUserList()) 
        {
            ShowMessageDialog("Info", "No users available.");
            return;
        }

        var topUsers = _users.OrderByDescending(user => user.Score).Take(5).ToList();

        UserListView.ItemsSource = topUsers;
    }

    private void BtnShowAll_Click(object sender, RoutedEventArgs e)
    {
        UserListView.ItemsSource = null;
        UserListView.ItemsSource = _users;
    }

    private void GoToMain_Click(object sender, RoutedEventArgs e)
    {
        Frame?.Navigate(typeof(MainPage));
    }
    private bool IsEmptyUserList()
    {
        return _users.Count == 0;
    }

    private async void ExportScores_Click(object sender, RoutedEventArgs e) 
    {
        if (IsEmptyUserList())
        {
            ShowMessageDialog("Error", "There are no Scores on the List");
            return;
        }
        var savePicker = new Windows.Storage.Pickers.FileSavePicker
        {
            SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary
        };

        savePicker.FileTypeChoices.Add("Text File", new List<string>() { ".txt" });
        savePicker.SuggestedFileName = "ExportedList";

        Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();

        if (file == null)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            file = await storageFolder.CreateFileAsync("DefaultExport.txt", Windows.Storage.CreationCollisionOption.OpenIfExists);
        }

        using (Stream stream = await file.OpenStreamForWriteAsync())
        using (StreamWriter writer = new StreamWriter(stream))
        {
            foreach (var item in _users)
            {
                await writer.WriteLineAsync($"{item.Name}: {item.Score}");
            }
        }
        ShowMessageDialog("sucess", $"File saved at: {file.Path}");

    }
    private async void ShowMessageDialog(string message, string content)
    {
        ContentDialog dialog = new ContentDialog
        {
            Title = message,
            Content = content,
            CloseButtonText = "OK",
            XamlRoot = this.XamlRoot
        };

        await dialog.ShowAsync();
    }

}
