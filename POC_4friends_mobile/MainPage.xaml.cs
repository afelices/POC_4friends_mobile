
using POC_4friends_mobile.Models;
using SQLite;

namespace POC_4friends_mobile
{
    public partial class MainPage : ContentPage
    {
        SQLiteConnection conn;
        string _fileName = Path.Combine(FileSystem.AppDataDirectory, "poc.sqlite");

        public MainPage()
        {
            InitializeComponent();

            SaveBtn.Clicked += OnSaveBtnClicked;
            ShowBtn.Clicked += OnShowBtnClicked;

            conn = new SQLiteConnection(_fileName);
            conn.CreateTable<Training>();
        }

        private void OnSaveBtnClicked(object sender, EventArgs e)
        {
            var training = new Training
            {
                HR = int.Parse(HREdt.Text),
                Datetime = DateTimeEdt.Text
            };

            conn.Insert(training);
            
            HREdt.Text = string.Empty;
            DateTimeEdt.Text = string.Empty;
        }

        private void OnShowBtnClicked(object sender, EventArgs e)
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
