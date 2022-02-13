using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Forms;
using static XamarinForms.MainPage;

namespace XamarinForms
{
    public partial class AddPooLog : ContentPage
    {
        private PooLog _newPooLog = new PooLog();
        //private DateTime occurrenceTime;
        //public DateTime OccurrenceTime { get { return occurrenceTime; } set {
        //        Console.WriteLine($"occurrenceTime was {occurrenceTime}");
        //        occurrenceTime = new DateTime(value.Ticks);
        //        Console.WriteLine($"occurrenceTime is now {occurrenceTime}");
        //    }
        //}

        public PooLog NewPooLog
        {
            get => _newPooLog;
            set =>_newPooLog = value;
        }

        public AddPooLog()
        {
            InitializeComponent();
            NewPooLog.Colour = "Choose Colour";
            NewPooLog.Type = "Just poo?";
            NewPooLog.OccurrenceDate = DateTime.Now;
            NewPooLog.OccurrenceTimeSpan = DateTime.Now.TimeOfDay;

            BindingContext = this;
            SaveChangesButton.Command = new Command(async () => { await SavePooLog(); });
        }

        private async Task SavePooLog()
        {
            try
            {
                var pooLogToSend = _newPooLog;

                pooLogToSend.OccurrenceTime = new DateTime(pooLogToSend.OccurrenceDate.Year, pooLogToSend.OccurrenceDate.Month, pooLogToSend.OccurrenceDate.Day, pooLogToSend.OccurrenceTimeSpan.Hours, pooLogToSend.OccurrenceTimeSpan.Minutes, pooLogToSend.OccurrenceTimeSpan.Seconds).ToUniversalTime();
                var client = new RestClient($"http://ubuntu:5000/BabyMonitor/AddPoo");

                var test = JsonConvert.SerializeObject(pooLogToSend);
                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.AddJsonBody(pooLogToSend);
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