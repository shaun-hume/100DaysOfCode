using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using static XamarinForms.MainPage;
using RestSharp;
using Newtonsoft.Json;
using System.Net;

namespace XamarinForms
{
    public partial class BreastPumpDetailView : ContentPage
    {
        private BreastPumpLog originalBreastPumpLog;
        private BreastPumpLog _newBreastPumpLog = new BreastPumpLog();
        public BreastPumpLog NewBreastPumpLog
        {
            get
            {
                return _newBreastPumpLog;
            }
            set
            {
                if (_newBreastPumpLog != originalBreastPumpLog)
                {
                    _newBreastPumpLog = value;
                }
            }
        }
        public bool SaveChangesButtonIsEnabled { get; set; }

        //todo: implement enabling button only when changes have been detected. The issue was that changes to the object are not detected, only changes to the entire object

        //protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    base.OnPropertyChanged(propertyName);
        //    if(originalBreastPumpLog != null && _newBreastPumpLog != null) UpdateStatusOfSaveButton();
        //}

        //private void UpdateStatusOfSaveButton()
        //{
        //    if (originalBreastPumpLog == null) SaveChangesButton.IsEnabled = false;
        //    else
        //    {
        //        if (originalBreastPumpLog.ID == _newBreastPumpLog.ID &&
        //            originalBreastPumpLog.Type == _newBreastPumpLog.Type &&
        //            originalBreastPumpLog.Amount == _newBreastPumpLog.Amount &&
        //            originalBreastPumpLog.EstimatedAmount == _newBreastPumpLog.EstimatedAmount &&
        //            originalBreastPumpLog.MeasurementType == _newBreastPumpLog.MeasurementType &&
        //            originalBreastPumpLog.Comment == _newBreastPumpLog.Comment &&
        //            originalBreastPumpLog.StartTime == _newBreastPumpLog.StartTime &&
        //            originalBreastPumpLog.FinishTime == _newBreastPumpLog.FinishTime)
        //        {
        //            SaveChangesButton.IsEnabled = false;
        //        }
        //        else SaveChangesButton.IsEnabled = true;
        //    }
        //}


        public BreastPumpDetailView(int id)
        {
            InitializeComponent();
            BindingContext = this;
            Task.Run(async () => { await UpdateBreastPumpLog(id); });
            SaveChangesButton.Command = new Command(async () => { await SaveBreastPumpLog(); });
        }

        private async Task UpdateBreastPumpLog(int id)
        {
            DataLayout.IsVisible = false;
            LoadingLayout.IsVisible = true;
            try
            {
                var client = new WebClient();
                var url = new Uri(string.Concat("http://ubuntu:5000/BabyMonitor/GetBreastPump/", id));
                var response = await client.DownloadStringTaskAsync(url);
                originalBreastPumpLog = JsonConvert.DeserializeObject<BreastPumpLog>(response);
                NewBreastPumpLog = originalBreastPumpLog;
                NewBreastPumpLog.OccurrenceDate = NewBreastPumpLog.OccurrenceDate.ToLocalTime();
                NewBreastPumpLog.OccurrenceTimeSpan = NewBreastPumpLog.OccurrenceTime.ToLocalTime().TimeOfDay;
            }
            catch (Exception ex) {
                var x = ex;
            }
            DataLayout.IsVisible = true;
            LoadingLayout.IsVisible = false;
        }

        private async Task SaveBreastPumpLog()
        {
            var client = new RestClient($"http://ubuntu:5000/BabyMonitor/UpdateBreastPump/{_newBreastPumpLog.ID}");
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            _newBreastPumpLog.OccurrenceTime = new DateTime(_newBreastPumpLog.OccurrenceDate.Year, _newBreastPumpLog.OccurrenceDate.Month, _newBreastPumpLog.OccurrenceDate.Day, _newBreastPumpLog.OccurrenceTimeSpan.Hours, _newBreastPumpLog.OccurrenceTimeSpan.Minutes, _newBreastPumpLog.OccurrenceTimeSpan.Seconds).ToUniversalTime();
            request.AddJsonBody(_newBreastPumpLog);
            var response = await client.PutAsync(request);
            await Navigation.PopAsync();
        }
    }
}