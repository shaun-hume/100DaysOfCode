using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Forms;

namespace XamarinForms
{
    // todo: Add Weight to the app

    public partial class MainPage : ContentPage
    {
        private DateTime currentlySelectedDate = DateTime.Today;

        public DateTime CurrentlySelectedDate
        {
            get => currentlySelectedDate;
            set
            {
                currentlySelectedDate = value;
                SelectedDate.Text = CurrentlySelectedDate.ToString("ddd MMM dd, yy");
            }
        }

        ObservableCollection<GenericLog> genericLogs = new ObservableCollection<GenericLog>();
        public ObservableCollection<GenericLog> GenericLogs { get { return genericLogs; } set { genericLogs = GenericLogs; } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            SelectedDate.Text = CurrentlySelectedDate.ToString("ddd MMM dd, yy");
            GenericLogs = new ObservableCollection<GenericLog>();
            GenericLogsView.ItemsSource = genericLogs;
            UpdateAllLogs();

            GenericLogsView.RefreshCommand = new Command(() =>
            {
                UpdateAllLogs();
            });

            PreviousDayButton.Command = new Command(() =>
            {
                GoBackADay();
            });

            NextDayButton.Command = new Command(() =>
            {
                GoForwardADay();
            });

            NewLogButton.Command = new Command(async () =>
            {
                await SelectTypeOfNewLog();
            });
        }

        async Task SelectTypeOfNewLog()
        {
            string answer = await DisplayActionSheet("What would you like to add?", "Cancel", null, new string[] { "Milk", "Poo", "Exercise", "Sleep" });
            if (answer != "Cancel")
            {
                switch (answer)
                {
                    case "Milk":
                        await Navigation.PushAsync(new AddMilkLog());
                        break;
                    case "Poo":
                        await Navigation.PushAsync(new AddPooLog());
                        break;
                    case "Exercise":
                        //Navigation.PushAsync(new AddExerciseLog());
                        break;
                    case "Sleep":
                        //Navigation.PushAsync(new AddSleepLog());
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateAllLogs();
        }

        private void UpdateAllLogs()
        {
            GenericLogsView.IsRefreshing = true;
            if (GenericLogs.Count > 0) GenericLogs.Clear();

            var tempObservableList = new ObservableCollection<GenericLog>();

            tempObservableList = UpdateMilkLogs(tempObservableList);
            tempObservableList = UpdatePooLogs(tempObservableList);
            var orderedList = tempObservableList.OrderBy(x => x.StartTime).ToList();

            foreach (var genericLog in orderedList)
            {
                GenericLogs.Add(genericLog);
            }

            GenericLogsView.ItemsSource = GenericLogs;
            GenericLogsView.IsRefreshing = false;

            GenericLogsView.IsVisible = true;
            NoItemsLabel.IsVisible = false;

            if (GenericLogs.Count == 0)
            {
                GenericLogsView.IsVisible = false;
                NoItemsLabel.IsVisible = true;
            }
            else
            {
                GenericLogsView.IsVisible = true;
                NoItemsLabel.IsVisible = false;
            }
        }

        private ObservableCollection<GenericLog> UpdateMilkLogs(ObservableCollection<GenericLog> tempObservableList)
        {
            var client = new WebClient();
            var response = client.DownloadString("http://ubuntu:5000/BabyMonitor/GetMilk");
            var milkLogs = JsonConvert.DeserializeObject<List<MilkLog>>(response);
            var genericLogs = milkLogs
                .Where(x => x.StartTime.ToLocalTime() >= CurrentlySelectedDate && x.StartTime.ToLocalTime() < CurrentlySelectedDate.AddDays(1))
                .Select(x => new GenericLog()
                {
                    ID = x.ID,
                    Type = "Milk",
                    Icon = "🍼",
                    StartTime = x.StartTime.ToLocalTime(),
                    FinishTime = x.FinishTime.ToLocalTime(),
                    SummaryOfEvent = $"Fed {x.Amount}{x.MeasurementType}"
                }).ToList();

            foreach (var log in genericLogs)
            {
                tempObservableList.Add(log);
            }

            return tempObservableList;
        }

        private ObservableCollection<GenericLog> UpdateExerciseLogs(ObservableCollection<GenericLog> tempObservableList)
        {
            var client = new WebClient();
            var response = client.DownloadString("http://ubuntu:5000/BabyMonitor/GetExercise");
            var exerciseLogs = JsonConvert.DeserializeObject<List<ExerciseLog>>(response);
            var genericLogs = exerciseLogs
                .Where(x => x.StartTime.ToLocalTime() >= CurrentlySelectedDate && x.StartTime.ToLocalTime() < CurrentlySelectedDate.AddDays(1))
                .Select(x => new GenericLog()
                {
                    ID = x.ID,
                    Type = "Exercise",
                    Icon = "💪",
                    StartTime = x.StartTime.ToLocalTime(),
                    FinishTime = x.FinishTime.ToLocalTime(),
                    SummaryOfEvent = $"{x.Type} at {x.StartTime}"
                }).ToList();

            foreach (var log in genericLogs)
            {
                tempObservableList.Add(log);
            }

            return tempObservableList;

        }

        private ObservableCollection<GenericLog> UpdatePooLogs(ObservableCollection<GenericLog> tempObservableList)
        {            
            var client = new WebClient();
            var response = client.DownloadString("http://ubuntu:5000/BabyMonitor/GetPoo");
            var pooLogs = JsonConvert.DeserializeObject<List<PooLog>>(response);
            var genericLogs = pooLogs
                .Where(x => x.OccurrenceTime.ToLocalTime() >= CurrentlySelectedDate && x.OccurrenceTime.ToLocalTime() < CurrentlySelectedDate.AddDays(1))
                .Select(x => new GenericLog()
                {
                    ID = x.ID,
                    Type = "Poo",
                    Icon = "💩",
                    StartTime = x.OccurrenceTime.ToLocalTime(),
                    FinishTime = x.OccurrenceTime.ToLocalTime(),
                    SummaryOfEvent = $"{x.Type}"
                }).ToList();

            foreach (var log in genericLogs)
            {
                tempObservableList.Add(log);
            }

            return tempObservableList;

        }

        private ObservableCollection<GenericLog> UpdateSleepLogs(ObservableCollection<GenericLog> tempObservableList)
        {
            var client = new WebClient();
            var response = client.DownloadString("http://ubuntu:5000/BabyMonitor/GetSleep");
            var sleepLogs = JsonConvert.DeserializeObject<List<SleepLog>>(response);
            var genericLogs = sleepLogs
                .Where(x => x.StartTime.ToLocalTime() >= CurrentlySelectedDate && x.StartTime.ToLocalTime() < CurrentlySelectedDate.AddDays(1))
                .Select(x => new GenericLog()
                {
                    ID = x.ID,
                    Type = "Sleep",
                    Icon = "🛏",
                    StartTime = x.StartTime.ToLocalTime(),
                    FinishTime = x.FinishTime.ToLocalTime(),
                    SummaryOfEvent = ""
                }).ToList();

            foreach (var log in genericLogs)
            {
                tempObservableList.Add(log);
            }

            return tempObservableList;

        }

        public void GoBackADay()
        {
            CurrentlySelectedDate = CurrentlySelectedDate.AddDays(-1);
            UpdateAllLogs();
        }

        public void GoForwardADay()
        {
            CurrentlySelectedDate = CurrentlySelectedDate.AddDays(1);
            UpdateAllLogs();
        }

        protected void DeleteLog(object sender, EventArgs e)
        {
            MenuItem x = sender as MenuItem;
            GenericLog logToDelete = (GenericLog)x.CommandParameter;
            DeleteItem(logToDelete);
        }

        public void DeleteItem(GenericLog genericLog)
        {
            var client = new RestClient($"http://ubuntu:5000/BabyMonitor/DeleteMilk/{genericLog.ID}");
            var request = new RestRequest();
            var response = client.DeleteAsync(request);
            UpdateAllLogs();
        }

        public async void LogClicked(object sender, EventArgs e)
        {
            var listView = sender as ListView;
            var selectedLog = listView.SelectedItem as GenericLog;

            if (selectedLog.Type == "Milk")
            {
                await Navigation.PushAsync(new MilkDetailView(selectedLog.ID));
            }
            if (selectedLog.Type == "Poo")
            {
                await Navigation.PushAsync(new PooDetailView(selectedLog.ID));
            }
        }

        public class GenericLog : INotifyPropertyChanged
        {
            public int ID { get; set; }
            public string Type { get; set; }
            public string Icon { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime FinishTime { get; set; }
            public string StartTimeShort { get { return StartTime.ToString("t"); } }
            public string SummaryOfEvent { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        public class MilkLog : INotifyPropertyChanged
        {
            public int ID { get; set; }
            public string Type { get; set; }
            public decimal Amount { get; set; }
            public decimal EstimatedAmount { get; set; }
            public string MeasurementType { get; set; }
            public string Comment { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime FinishTime { get; set; }

            public List<string> Types { get; set; } = new List<string> { "Breast Milk", "Formula" };

            public string HumanReadableAmount
            {
                get
                {
                    return Amount + " " + MeasurementType;
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        public class PooLog : INotifyPropertyChanged
        {
            public int ID { get; set; }
            public string Type { get; set; }
            public List<string> Types { get; set; } = new List<string> { "Poo", "Pee", "Poo & Pee" };
            public string Comment { get; set; }
            public string Colour { get; set; }
            public List<string> Colours { get; set; } = new List<string> { "Brown", "Yellow", "Green", "Black", "Other" };
            public DateTime OccurrenceDate { get; set; }
            public TimeSpan OccurrenceTimeSpan { get; set; }
            public DateTime OccurrenceTime { get; set; }


            public event PropertyChangedEventHandler PropertyChanged;
        }

        public class SleepLog : INotifyPropertyChanged
        {
            public int ID { get; set; }
            public string Comment { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime FinishTime { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        public class ExerciseLog : INotifyPropertyChanged
        {
            public int ID { get; set; }
            public string Type { get; set; }
            public string Comment { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime FinishTime { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
        }
    }
}

