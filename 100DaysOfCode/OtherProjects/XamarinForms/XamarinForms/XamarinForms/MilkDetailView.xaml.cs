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
    public partial class MilkDetailView : ContentPage
    {
        private MilkLog originalMilkLog;
        private MilkLog _newMilkLog = new MilkLog();
        public bool SaveChangesButtonIsEnabled { get; set; }
        public MilkLog NewMilkLog
        {
            get
            {
                return _newMilkLog;
            }
            set
            {
                if (_newMilkLog != originalMilkLog)
                {
                    _newMilkLog = value;
                }
            }
        }

        //todo: implement enabling button only when changes have been detected. The issue was that changes to the object are not detected, only changes to the entire object

        //protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    base.OnPropertyChanged(propertyName);
        //    if(originalMilkLog != null && _newMilkLog != null) UpdateStatusOfSaveButton();
        //}

        //private void UpdateStatusOfSaveButton()
        //{
        //    if (originalMilkLog == null) SaveChangesButton.IsEnabled = false;
        //    else
        //    {
        //        if (originalMilkLog.ID == _newMilkLog.ID &&
        //            originalMilkLog.Type == _newMilkLog.Type &&
        //            originalMilkLog.Amount == _newMilkLog.Amount &&
        //            originalMilkLog.EstimatedAmount == _newMilkLog.EstimatedAmount &&
        //            originalMilkLog.MeasurementType == _newMilkLog.MeasurementType &&
        //            originalMilkLog.Comment == _newMilkLog.Comment &&
        //            originalMilkLog.StartTime == _newMilkLog.StartTime &&
        //            originalMilkLog.FinishTime == _newMilkLog.FinishTime)
        //        {
        //            SaveChangesButton.IsEnabled = false;
        //        }
        //        else SaveChangesButton.IsEnabled = true;
        //    }
        //}


        public MilkDetailView(int id)
        {
            InitializeComponent();
            BindingContext = this;
            Task.Run(async () => { await UpdateMilkLog(id); });
            SaveChangesButton.Command = new Command(async () => { await SaveMilkLog(); });
        }

        private async Task UpdateMilkLog(int id)
        {
            DataLayout.IsVisible = false;
            LoadingLayout.IsVisible = true;
            try
            {
                var client = new WebClient();
                var url = new Uri(string.Concat("http://ubuntu:5000/BabyMonitor/GetMilk/", id));
                var response = await client.DownloadStringTaskAsync(url);
                originalMilkLog = JsonConvert.DeserializeObject<MilkLog>(response);
                NewMilkLog = originalMilkLog;
            }
            catch (Exception ex) {
                var x = ex;
            }
            DataLayout.IsVisible = true;
            LoadingLayout.IsVisible = false;
        }

        private async Task SaveMilkLog()
        {
            var client = new RestClient($"http://ubuntu:5000/BabyMonitor/UpdateMilk/{_newMilkLog.ID}");
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddJsonBody(_newMilkLog);
            var response = await client.PutAsync(request);
            await Navigation.PopAsync();
        }
    }
}