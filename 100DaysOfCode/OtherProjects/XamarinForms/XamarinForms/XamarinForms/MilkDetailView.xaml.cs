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
        public MilkLog NewMilkLog {
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

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if(originalMilkLog != null && _newMilkLog != null) UpdateStatusOfSaveButton();
        }

        private void UpdateStatusOfSaveButton()
        {
            if (originalMilkLog == null) SaveChangesButton.IsEnabled = false;
            else
            {
                if (originalMilkLog.ID == _newMilkLog.ID &&
                    originalMilkLog.Type == _newMilkLog.Type &&
                    originalMilkLog.Amount == _newMilkLog.Amount &&
                    originalMilkLog.EstimatedAmount == _newMilkLog.EstimatedAmount &&
                    originalMilkLog.MeasurementType == _newMilkLog.MeasurementType &&
                    originalMilkLog.Comment == _newMilkLog.Comment &&
                    originalMilkLog.StartTime == _newMilkLog.StartTime &&
                    originalMilkLog.FinishTime == _newMilkLog.FinishTime)
                {
                    SaveChangesButton.IsEnabled = false;
                }
                else SaveChangesButton.IsEnabled = true;
            }
        }

        public bool SaveChangesButtonIsEnabled { get; set; }

        public MilkDetailView(int id)
        {
            InitializeComponent();
            BindingContext = this;
            UpdateMilkLog(id);
        }

        private void UpdateMilkLog(int id)
        {
            var client = new WebClient();
            var response = client.DownloadString(string.Concat("http://ubuntu:5000/BabyMonitor/GetMilk/", id));
            originalMilkLog = JsonConvert.DeserializeObject<MilkLog>(response);
            NewMilkLog = originalMilkLog;

            //EstimatedAmount = originalMilkLog.EstimatedAmount;
            //EstimatedAmountMeasurementType.Text = originalMilkLog.MeasurementType;
            //ActualAmount.Text = originalMilkLog.Amount.ToString();
            //ActualAmountMeasurementType.Text = originalMilkLog.MeasurementType;
            //Comment.Text = originalMilkLog.Comment;
            //StartDate.Date = originalMilkLog.StartTime;
            //StartTime.Time = new TimeSpan(originalMilkLog.StartTime.Hour, originalMilkLog.StartTime.Minute, originalMilkLog.StartTime.Second);
            //FinishDate.Date = originalMilkLog.FinishTime;
            //FinishTime.Time = new TimeSpan(originalMilkLog.FinishTime.Hour, originalMilkLog.FinishTime.Minute, originalMilkLog.FinishTime.Second);

        }
    }
}