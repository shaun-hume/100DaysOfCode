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
        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand RefreshGenericLogs
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await UpdateMilkLogs();

                    IsRefreshing = false;
                });
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
            GenericLogs = new ObservableCollection<GenericLog>();
            GenericLogsView.ItemsSource = genericLogs;

            UpdateMilkLogs();

        }

        private async Task UpdateMilkLogs()
        {
            var client = new WebClient();
            var response = await client.DownloadStringTaskAsync("http://ubuntu:5000/BabyMonitor/GetMilk");
            var milkLogs = JsonConvert.DeserializeObject<List<MilkLog>>(response);
            var genericLogs = milkLogs.Select(x => new GenericLog()
            {
                ID = x.ID,
                Type = "Milk",
                Icon = "🍼",
                StartTime = x.StartTime,
                FinishTime = x.FinishTime
            }).ToList();
            foreach (var log in genericLogs)
            {
                GenericLogs.Add(log);
            }
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

        public class GenericLog
        {
            public int ID { get; set; }
            public string Type { get; set; }
            public string Icon { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime FinishTime { get; set; }
        }

        public class MilkLog
        {
            public int ID { get; set; }
            public string Type { get; set; }
            public decimal Amount { get; set; }
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

