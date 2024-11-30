
using POC_4friends_mobile.Models;
using SQLite;
using System.Runtime.CompilerServices;

namespace POC_4friends_mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly SQLiteAsyncConnection conn;
        private readonly string _fileName = Path.Combine(FileSystem.AppDataDirectory, "poc.sqlite");

        public MainPage()
        {
            InitializeComponent();

            SaveBtn.Clicked += OnSaveBtnClicked!;
            ShowBtn.Clicked += OnShowBtnClickedAsync!;

            conn = new SQLiteAsyncConnection(_fileName);
            async Task result()
            {
                await conn.CreateTableAsync<Training>();
            }
        }

        private async void OnSaveBtnClicked(object sender, EventArgs e)
        {
            var training = new Training
            {
                HR = int.Parse(HREdt.Text),
                Datetime = DateTimeEdt.Text
            };

            conn.InsertAsync(training);
            
            HREdt.Text = string.Empty;
            DateTimeEdt.Text = string.Empty;
        }

        private async void OnShowBtnClickedAsync(object sender, EventArgs e)
        {
            if (File.Exists(_fileName))
            {
                TrainingEdt.Text = string.Empty;

                List<Training> trainingList = await conn.Table<Training>().ToListAsync();
                foreach (var training in trainingList)
                    TrainingEdt.Text += $"{training.Id} | {training.Datetime} | {training.HR}\n";
            }
        }
    }
}
