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
    public partial class MainPage : ContentPage
    {
        private DateTime currentlySelectedDate = DateTime.Today;

        public DateTime CurrentlySelectedDate
        {
            get
            {
                return currentlySelectedDate;
            }
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
            GenericLogsView.IsRefreshing = true;
            SelectedDate.Text = CurrentlySelectedDate.ToString("ddd MMM dd, yy");
            GenericLogs = new ObservableCollection<GenericLog>();
            GenericLogsView.ItemsSource = genericLogs;

            UpdateMilkLogs();

            GenericLogsView.RefreshCommand = new Command(() =>
            {
                UpdateMilkLogs();

                GenericLogsView.IsRefreshing = false;
            });

            PreviousDayButton.Command = new Command(() =>
            {
                GoBackADay();
            });

            NextDayButton.Command = new Command(() =>
            {
                GoForwardADay();
            });

        }

        private async Task UpdateMilkLogs()
        {
            GenericLogs.Clear();
            var client = new WebClient();
            var response = await client.DownloadStringTaskAsync("http://ubuntu:5000/BabyMonitor/GetMilk");
            var milkLogs = JsonConvert.DeserializeObject<List<MilkLog>>(response);
            var genericLogs = milkLogs
                .Where(x => x.StartTime > CurrentlySelectedDate && x.StartTime < CurrentlySelectedDate.AddDays(1))
                .Select(x => new GenericLog()
                {
                    ID = x.ID,
                    Type = "Milk",
                    Icon = "🍼",
                    StartTime = x.StartTime,
                    FinishTime = x.FinishTime,
                    SummaryOfEvent = $"Fed {x.Amount}{x.MeasurementType}"
                }).ToList();

            var tempObservableCollection = new ObservableCollection<GenericLog>();
            foreach (var log in genericLogs)
            {
                tempObservableCollection.Add(log);
            }
            GenericLogsView.ItemsSource = tempObservableCollection;

            if (tempObservableCollection.Count == 0)
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

        public void GoBackADay()
        {
            GenericLogsView.IsRefreshing = true;
            CurrentlySelectedDate = CurrentlySelectedDate.AddDays(-1);
            UpdateMilkLogs();
            GenericLogsView.IsRefreshing = false;
        }

        public void GoForwardADay()
        {
            GenericLogsView.IsRefreshing = true;
            CurrentlySelectedDate = CurrentlySelectedDate.AddDays(1);
            UpdateMilkLogs();
            GenericLogsView.IsRefreshing = false;
        }

        protected void PressMeButton_Clicked(object sender, EventArgs e)
        {
            MenuItem x = sender as MenuItem;
            GenericLog logToDelete = (GenericLog)x.CommandParameter;
            DeleteItem(logToDelete);
        }

        public void DeleteItem(object genericLog)
        {
            var client = new RestClient("http://ubuntu:5000/BabyMonitor/DeleteMilk");
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            var body = "{'ID':}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = client.DeleteAsync(request);
            var x = 1;
        }

        public async void LogClicked(object sender, EventArgs e)
        {
            var listView = sender as ListView;
            var selectedLog = listView.SelectedItem as GenericLog;

            if (selectedLog.Type == "Milk")
            {
                Navigation.PushAsync(new MilkDetailView(selectedLog.ID));
            }
        }

        public class GenericLog
        {
            public int ID { get; set; }
            public string Type { get; set; }
            public string Icon { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime FinishTime { get; set; }
            public string StartTimeShort { get { return StartTime.ToString("t"); } }
            public string SummaryOfEvent { get; set; }
        }

        public class MilkLog
        {
            public int ID { get; set; }
            public string Type { get; set; }
            public decimal Amount { get; set; }
            public decimal EstimatedAmount { get; set; }
            public string MeasurementType { get; set; }
            public string Comment { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime FinishTime { get; set; }

            public string HumanReadableAmount
            {
                get
                {
                    return Amount + " " + MeasurementType;
                }
            }
        }
    }
}

