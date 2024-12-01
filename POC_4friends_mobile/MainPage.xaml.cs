

using POC_4friends_mobile.Models;
using SQLite;

namespace POC_4friends_mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly SQLiteConnection conn;
        private readonly string _fileName = Path.Combine(FileSystem.AppDataDirectory, "poc.sqlite");

        public MainPage()
        {
            InitializeComponent();

            SaveBtn.Clicked += OnSaveBtnClicked!;
            ShowBtn.Clicked += OnShowBtnClickedAsync!;

            conn = new SQLiteConnection(_fileName);
            conn.CreateTable<Training>();
            //var result = async () => await conn.CreateTableAsync<Training>();
        }

        private void OnSaveBtnClicked(object sender, EventArgs e)
        {
            var training = new Training
            {
                HR = int.Parse(HREdt.Text),
                Datetime = DateTimeEdt.Text
            };
            
            var inserted = conn.Insert(training);
            if (inserted == 0)
                DisplayAlert("DB Error", "Training was not saved into the local DB", "OK");

            HREdt.Text = string.Empty;
            DateTimeEdt.Text = string.Empty;
        }

        private void OnShowBtnClickedAsync(object sender, EventArgs e)
        {
            if (File.Exists(_fileName))
            {
                TrainingEdt.Text = string.Empty;

                List<Training> trainingList = conn.Table<Training>().ToList();
                foreach (var training in trainingList)
                    TrainingEdt.Text += $"{training.Id} | {training.Datetime} | {training.HR}\n";
            }
        }
    }
}
