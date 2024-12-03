

using POC_4friends_mobile.Models;
using SQLite;

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
            Task.Run(() => conn.CreateTableAsync<Training>());
        }

        private void OnSaveBtnClicked(object sender, EventArgs e)
        {
            var training = new Training
            {
                HR = int.Parse(HREdt.Text),
                Datetime = DateTimeEdt.Text
            };

            var inserted = conn.InsertAsync(training);
            if (inserted.Result == 0)
                DisplayAlert("DB Error", "Training was not saved into the local DB", "OK");

            HREdt.Text = string.Empty;
            DateTimeEdt.Text = string.Empty;
        }

        private void OnShowBtnClickedAsync(object sender, EventArgs e)
        {
            if (File.Exists(_fileName))
            {
                var trainingList = Task.Run(() => conn.Table<Training>().ToListAsync());
                foreach (var training in trainingList.Result)
                    TrainingEdt.Text += $"{training.Id} | {training.Datetime} | {training.HR}\n";

                TrainingEdt.Text = string.Empty;
            }
        }
    }
}
