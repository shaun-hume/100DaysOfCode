using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using static XamarinForms.MainPage;
using RestSharp;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.CompilerServices;

namespace XamarinForms
{
    public partial class SleepDetailView : ContentPage
    {
        private SleepLog originalSleepLog;
        private SleepLog _newSleepLog = new SleepLog();
        public bool SaveChangesButtonIsEnabled { get; set; }
        public SleepLog NewSleepLog
        {
            get
            {
                return _newSleepLog;
            }
            set
            {
                if (_newSleepLog != originalSleepLog)
                {
                    _newSleepLog = value;
                }
            }
        }

        //todo: implement enabling button only when changes have been detected. The issue was that changes to the object are not detected, only changes to the entire object

        //protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    base.OnPropertyChanged(propertyName);
        //    if(originalSleepLog != null && _newSleepLog != null) UpdateStatusOfSaveButton();
        //}

        //private void UpdateStatusOfSaveButton()
        //{
        //    if (originalSleepLog == null) SaveChangesButton.IsEnabled = false;
        //    else
        //    {
        //        if (originalSleepLog.ID == _newSleepLog.ID &&
        //            originalSleepLog.Type == _newSleepLog.Type &&
        //            originalSleepLog.Amount == _newSleepLog.Amount &&
        //            originalSleepLog.EstimatedAmount == _newSleepLog.EstimatedAmount &&
        //            originalSleepLog.MeasurementType == _newSleepLog.MeasurementType &&
        //            originalSleepLog.Comment == _newSleepLog.Comment &&
        //            originalSleepLog.StartTime == _newSleepLog.StartTime &&
        //            originalSleepLog.FinishTime == _newSleepLog.FinishTime)
        //        {
        //            SaveChangesButton.IsEnabled = false;
        //        }
        //        else SaveChangesButton.IsEnabled = true;
        //    }
        //}


        public SleepDetailView(int id)
        {
            InitializeComponent();
            BindingContext = this;
            Task.Run(async () => { await UpdateSleepLog(id); });
            SaveChangesButton.Command = new Command(async () => { await SaveSleepLog(); });
        }

        private async Task UpdateSleepLog(int id)
        {
            DataLayout.IsVisible = false;
            LoadingLayout.IsVisible = true;
            try
            {
                var client = new WebClient();
                var url = new Uri(string.Concat("http://ubuntu:5000/BabyMonitor/GetSleep/", id));
                var response = await client.DownloadStringTaskAsync(url);
                originalSleepLog = JsonConvert.DeserializeObject<SleepLog>(response);
                NewSleepLog = originalSleepLog;
            }
            catch (Exception ex) {
                var x = ex;
            }
            DataLayout.IsVisible = true;
            LoadingLayout.IsVisible = false;
        }

        private async Task SaveSleepLog()
        {
            var client = new RestClient($"http://ubuntu:5000/BabyMonitor/UpdateSleep/{_newSleepLog.ID}");
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddJsonBody(_newSleepLog);
            var response = await client.PutAsync(request);
            await Navigation.PopAsync();
        }
    }
}