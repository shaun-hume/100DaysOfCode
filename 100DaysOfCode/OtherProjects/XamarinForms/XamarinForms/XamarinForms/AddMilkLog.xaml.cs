using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Forms;
using XamarinForms.Helpers;
using static XamarinForms.MainPage;

namespace XamarinForms
{
    public partial class AddMilkLog : ContentPage
    {
        private MilkLog _newMilkLog = new MilkLog();
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }

        public MilkLog NewMilkLog
        {
            get => _newMilkLog;
            set => _newMilkLog = value;
        }

        public AddMilkLog()
        {
            InitializeComponent();
            NewMilkLog.MeasurementType = "mL";
            NewMilkLog.Type = "Breast Milk";
            NewMilkLog.StartTimeDate = DateTime.Now;
            NewMilkLog.StartTimeSpan = DateTime.Now.TimeOfDay;
            NewMilkLog.FinishTimeDate = DateTime.Now;
            NewMilkLog.FinishTimeSpan = DateTime.Now.TimeOfDay;

            BindingContext = this;
            SaveChangesButton.Command = new Command(async () => { await SaveMilkLog(); });
        }

        private async Task SaveMilkLog()
        {
            try
            {
                var milkLog = new MilkLog
                {
                    Type = _newMilkLog.Type,
                    Amount = _newMilkLog.Amount,
                    EstimatedAmount = _newMilkLog.EstimatedAmount,
                    MeasurementType = _newMilkLog.MeasurementType,
                    Comment = _newMilkLog.Comment,
                    StartTime = new DateTime(_newMilkLog.StartTimeDate.Year, _newMilkLog.StartTimeDate.Month, _newMilkLog.StartTimeDate.Day, _newMilkLog.StartTimeSpan.Hours, _newMilkLog.StartTimeSpan.Minutes, _newMilkLog.StartTimeSpan.Seconds).ToUniversalTime(),
                    FinishTime = new DateTime(_newMilkLog.FinishTimeDate.Year, _newMilkLog.FinishTimeDate.Month, _newMilkLog.FinishTimeDate.Day, _newMilkLog.FinishTimeSpan.Hours, _newMilkLog.FinishTimeSpan.Minutes, _newMilkLog.FinishTimeSpan.Seconds).ToUniversalTime()
                };

                var client = new RestClient($"http://ubuntu:5000/BabyMonitor/AddMilk");
                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.AddJsonBody(milkLog);
                var response = await client.PostAsync(request);

                //Disabling offline data handling for now
                //await DataHandler.TryApiWithRequest(new Logs.Logs.MilkLog
                //{
                //    Type = milkLog.Type,
                //    Amount = milkLog.Amount,
                //    EstimatedAmount = milkLog.EstimatedAmount,
                //    MeasurementType = milkLog.MeasurementType,
                //    Comment = milkLog.Comment,
                //    StartTime = milkLog.StartTime,
                //    FinishTime = milkLog.FinishTime
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