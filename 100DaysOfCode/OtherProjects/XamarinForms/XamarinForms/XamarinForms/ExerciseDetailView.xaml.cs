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
    public partial class ExerciseDetailView : ContentPage
    {
        private ExerciseLog originalExerciseLog;
        private ExerciseLog _newExerciseLog = new ExerciseLog();
        public bool SaveChangesButtonIsEnabled { get; set; }
        public ExerciseLog NewExerciseLog
        {
            get
            {
                return _newExerciseLog;
            }
            set
            {
                if (_newExerciseLog != originalExerciseLog)
                {
                    _newExerciseLog = value;
                }
            }
        }

        //todo: implement enabling button only when changes have been detected. The issue was that changes to the object are not detected, only changes to the entire object

        //protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    base.OnPropertyChanged(propertyName);
        //    if(originalExerciseLog != null && _newExerciseLog != null) UpdateStatusOfSaveButton();
        //}

        //private void UpdateStatusOfSaveButton()
        //{
        //    if (originalExerciseLog == null) SaveChangesButton.IsEnabled = false;
        //    else
        //    {
        //        if (originalExerciseLog.ID == _newExerciseLog.ID &&
        //            originalExerciseLog.Type == _newExerciseLog.Type &&
        //            originalExerciseLog.Amount == _newExerciseLog.Amount &&
        //            originalExerciseLog.EstimatedAmount == _newExerciseLog.EstimatedAmount &&
        //            originalExerciseLog.MeasurementType == _newExerciseLog.MeasurementType &&
        //            originalExerciseLog.Comment == _newExerciseLog.Comment &&
        //            originalExerciseLog.StartTime == _newExerciseLog.StartTime &&
        //            originalExerciseLog.FinishTime == _newExerciseLog.FinishTime)
        //        {
        //            SaveChangesButton.IsEnabled = false;
        //        }
        //        else SaveChangesButton.IsEnabled = true;
        //    }
        //}


        public ExerciseDetailView(int id)
        {
            InitializeComponent();
            BindingContext = this;
            Task.Run(async () => { await UpdateExerciseLog(id); });
            SaveChangesButton.Command = new Command(async () => { await SaveExerciseLog(); });
        }

        private async Task UpdateExerciseLog(int id)
        {
            DataLayout.IsVisible = false;
            LoadingLayout.IsVisible = true;
            try
            {
                var client = new WebClient();
                var url = new Uri(string.Concat("http://ubuntu:5000/BabyMonitor/GetExercise/", id));
                var response = await client.DownloadStringTaskAsync(url);
                originalExerciseLog = JsonConvert.DeserializeObject<ExerciseLog>(response);
                NewExerciseLog = originalExerciseLog;
                NewExerciseLog.StartTimeDate = NewExerciseLog.StartTimeDate.ToLocalTime();
                NewExerciseLog.FinishTimeDate = NewExerciseLog.FinishTimeDate.ToLocalTime();
                NewExerciseLog.StartTimeSpan = NewExerciseLog.StartTime.ToLocalTime().TimeOfDay;
                NewExerciseLog.FinishTimeSpan = NewExerciseLog.FinishTime.ToLocalTime().TimeOfDay;
            }
            catch (Exception ex) {
                var x = ex;
            }
            DataLayout.IsVisible = true;
            LoadingLayout.IsVisible = false;
        }

        private async Task SaveExerciseLog()
        {
            var client = new RestClient($"http://ubuntu:5000/BabyMonitor/UpdateExercise/{_newExerciseLog.ID}");
            var request = new RestRequest();
            _newExerciseLog.StartTime = new DateTime(_newExerciseLog.StartTimeDate.Year, _newExerciseLog.StartTimeDate.Month, _newExerciseLog.StartTimeDate.Day, _newExerciseLog.StartTimeSpan.Hours, _newExerciseLog.StartTimeSpan.Minutes, _newExerciseLog.StartTimeSpan.Seconds).ToUniversalTime();
            _newExerciseLog.FinishTime = new DateTime(_newExerciseLog.FinishTimeDate.Year, _newExerciseLog.FinishTimeDate.Month, _newExerciseLog.FinishTimeDate.Day, _newExerciseLog.FinishTimeSpan.Hours, _newExerciseLog.FinishTimeSpan.Minutes, _newExerciseLog.FinishTimeSpan.Seconds).ToUniversalTime();
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddJsonBody(_newExerciseLog);
            var response = await client.PutAsync(request);
            await Navigation.PopAsync();
        }
    }
}