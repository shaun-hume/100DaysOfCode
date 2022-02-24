using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Forms;
using XamarinForms.Helpers;
using static XamarinForms.MainPage;

namespace XamarinForms
{
    public partial class AddBreastPumpLog : ContentPage
    {
        private BreastPumpLog _newBreastPumpLog = new BreastPumpLog();
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }

        public BreastPumpLog NewBreastPumpLog
        {
            get => _newBreastPumpLog;
            set => _newBreastPumpLog = value;
        }

        public AddBreastPumpLog()
        {
            InitializeComponent();
            NewBreastPumpLog.MeasurementType = "mL";
            NewBreastPumpLog.Type = "Breast BreastPump";
            NewBreastPumpLog.OccurrenceDate = DateTime.Now;
            NewBreastPumpLog.OccurrenceTimeSpan = DateTime.Now.TimeOfDay;

            BindingContext = this;
            SaveChangesButton.Command = new Command(async () => { await SaveBreastPumpLog(); });
        }

        private async Task SaveBreastPumpLog()
        {
            try
            {
                var BreastPumpLog = new BreastPumpLog
                {
                    Type = _newBreastPumpLog.Type,
                    Amount = _newBreastPumpLog.Amount,
                    MeasurementType = _newBreastPumpLog.MeasurementType,
                    Comment = _newBreastPumpLog.Comment,
                    OccurrenceTime = new DateTime(NewBreastPumpLog.OccurrenceDate.Year, NewBreastPumpLog.OccurrenceDate.Month, NewBreastPumpLog.OccurrenceDate.Day, NewBreastPumpLog.OccurrenceTimeSpan.Hours, NewBreastPumpLog.OccurrenceTimeSpan.Minutes, NewBreastPumpLog.OccurrenceTimeSpan.Seconds).ToUniversalTime(),
                };

                var client = new RestClient($"http://ubuntu:5000/BabyMonitor/AddBreastPump");
                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.AddJsonBody(BreastPumpLog);
                var response = await client.PostAsync(request);

                //Disabling offline data handling for now
                //await DataHandler.TryApiWithRequest(new Logs.Logs.BreastPumpLog
                //{
                //    Type = BreastPumpLog.Type,
                //    Amount = BreastPumpLog.Amount,
                //    EstimatedAmount = BreastPumpLog.EstimatedAmount,
                //    MeasurementType = BreastPumpLog.MeasurementType,
                //    Comment = BreastPumpLog.Comment,
                //    StartTime = BreastPumpLog.StartTime,
                //    FinishTime = BreastPumpLog.FinishTime
                //}
                //);
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                var x = ex;
            }
        }
    }
}