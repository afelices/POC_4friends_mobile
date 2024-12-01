namespace POC_4friends_mobile;
using SQLite;

public partial class Credentials : ContentPage
{
    private readonly string _fileName = Path.Combine(FileSystem.AppDataDirectory, "poc.sqlite");

    public Credentials()
	{
		InitializeComponent();

        SaveBtn.Clicked += OnSaveBtnClicked!;
    }

    private async void OnSaveBtnClicked(object sender, EventArgs e)
    {
        // Get the credentials and copy them into the remote DB.


        Username.Text = string.Empty;
        Password.Text = string.Empty;

        await Shell.Current.GoToAsync("MainPage");
    }
}
