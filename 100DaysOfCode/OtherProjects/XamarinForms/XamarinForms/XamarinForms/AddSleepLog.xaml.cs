using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Forms;
using static XamarinForms.MainPage;

namespace XamarinForms
{
    public partial class AddSleepLog : ContentPage
    {
        private SleepLog _newSleepLog = new SleepLog();

        public SleepLog NewSleepLog
        {
            get => _newSleepLog;
            set =>_newSleepLog = value;
        }

        public AddSleepLog()
        {
            InitializeComponent();
            NewSleepLog.StartTimeDate = DateTime.Now;
            NewSleepLog.StartTimeSpan = DateTime.Now.TimeOfDay;
            NewSleepLog.FinishTimeDate = DateTime.Now;
            NewSleepLog.FinishTimeSpan = DateTime.Now.TimeOfDay;

            BindingContext = this;
            SaveChangesButton.Command = new Command(async () => { await SaveSleepLog(); });
        }

        private async Task SaveSleepLog()
        {
            try
            {
                var SleepLogToSend = _newSleepLog;

                SleepLogToSend.StartTime = new DateTime(SleepLogToSend.StartTimeDate.Year, SleepLogToSend.StartTimeDate.Month, SleepLogToSend.StartTimeDate.Day, SleepLogToSend.StartTimeSpan.Hours, SleepLogToSend.StartTimeSpan.Minutes, SleepLogToSend.StartTimeSpan.Seconds).ToUniversalTime();
                SleepLogToSend.FinishTime = new DateTime(SleepLogToSend.FinishTimeDate.Year, SleepLogToSend.FinishTimeDate.Month, SleepLogToSend.FinishTimeDate.Day, SleepLogToSend.FinishTimeSpan.Hours, SleepLogToSend.FinishTimeSpan.Minutes, SleepLogToSend.FinishTimeSpan.Seconds).ToUniversalTime();
                var client = new RestClient($"http://ubuntu:5000/BabyMonitor/AddSleep");

                var test = JsonConvert.SerializeObject(SleepLogToSend);
                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.AddJsonBody(SleepLogToSend);
                var response = await client.PostAsync(request);
                await Navigation.PopAsync();
            }
            catch(Exception ex)
            {
                var x = ex;
            }
        }
    }
}