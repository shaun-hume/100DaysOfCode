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
    public partial class PooDetailView : ContentPage
    {
        private PooLog originalPooLog;
        private PooLog _newPooLog = new PooLog();
        public bool SaveChangesButtonIsEnabled { get; set; }
        public PooLog NewPooLog
        {
            get
            {
                return _newPooLog;
            }
            set
            {
                if (_newPooLog != originalPooLog)
                {
                    _newPooLog = value;
                }
            }
        }

        //todo: implement enabling button only when changes have been detected. The issue was that changes to the object are not detected, only changes to the entire object

        //protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    base.OnPropertyChanged(propertyName);
        //    if(originalPooLog != null && _newPooLog != null) UpdateStatusOfSaveButton();
        //}

        //private void UpdateStatusOfSaveButton()
        //{
        //    if (originalPooLog == null) SaveChangesButton.IsEnabled = false;
        //    else
        //    {
        //        if (originalPooLog.ID == _newPooLog.ID &&
        //            originalPooLog.Type == _newPooLog.Type &&
        //            originalPooLog.Amount == _newPooLog.Amount &&
        //            originalPooLog.EstimatedAmount == _newPooLog.EstimatedAmount &&
        //            originalPooLog.MeasurementType == _newPooLog.MeasurementType &&
        //            originalPooLog.Comment == _newPooLog.Comment &&
        //            originalPooLog.StartTime == _newPooLog.StartTime &&
        //            originalPooLog.FinishTime == _newPooLog.FinishTime)
        //        {
        //            SaveChangesButton.IsEnabled = false;
        //        }
        //        else SaveChangesButton.IsEnabled = true;
        //    }
        //}


        public PooDetailView(int id)
        {
            InitializeComponent();
            BindingContext = this;
            Task.Run(async () => { await UpdatePooLog(id); });
            SaveChangesButton.Command = new Command(async () => { await SavePooLog(); });
        }

        private async Task UpdatePooLog(int id)
        {
            DataLayout.IsVisible = false;
            LoadingLayout.IsVisible = true;
            try
            {
                var client = new WebClient();
                var url = new Uri(string.Concat("http://ubuntu:5000/BabyMonitor/GetPoo/", id));
                var response = await client.DownloadStringTaskAsync(url);
                originalPooLog = JsonConvert.DeserializeObject<PooLog>(response);
                NewPooLog = originalPooLog;
                NewPooLog.OccurrenceTime = NewPooLog.OccurrenceTime.ToLocalTime();

            }
            catch (Exception ex) {
                var x = ex;
            }
            DataLayout.IsVisible = true;
            LoadingLayout.IsVisible = false;
        }

        private async Task SavePooLog()
        {
            var pooLogToSend = new PooLog
            {
                ID = NewPooLog.ID,
                Type = NewPooLog.Type,
                Comment = NewPooLog.Comment,
                Colour = NewPooLog.Colour,
                OccurrenceTime = new DateTime(NewPooLog.OccurrenceDate.Year, NewPooLog.OccurrenceDate.Month, NewPooLog.OccurrenceDate.Day, NewPooLog.OccurrenceTimeSpan.Hours, NewPooLog.OccurrenceTimeSpan.Minutes, NewPooLog.OccurrenceTimeSpan.Seconds).ToUniversalTime()
            };

            var client = new RestClient($"http://ubuntu:5000/BabyMonitor/UpdatePoo/{_newPooLog.ID}");
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddJsonBody(pooLogToSend);
            var response = await client.PutAsync(request);
            await Navigation.PopAsync();
        }
    }
}