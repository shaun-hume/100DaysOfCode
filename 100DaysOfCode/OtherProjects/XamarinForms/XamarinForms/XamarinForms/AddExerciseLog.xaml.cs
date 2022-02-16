using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Forms;
using static XamarinForms.MainPage;

namespace XamarinForms
{
    public partial class AddExerciseLog : ContentPage
    {
        private ExerciseLog _newExerciseLog = new ExerciseLog();

        public ExerciseLog NewExerciseLog
        {
            get => _newExerciseLog;
            set =>_newExerciseLog = value;
        }

        public AddExerciseLog()
        {
            InitializeComponent();
            NewExerciseLog.Type = "Tummy Time";
            NewExerciseLog.StartTimeDate = DateTime.Now;
            NewExerciseLog.StartTimeSpan = DateTime.Now.TimeOfDay;
            NewExerciseLog.FinishTimeDate = DateTime.Now;
            NewExerciseLog.FinishTimeSpan = DateTime.Now.TimeOfDay;

            BindingContext = this;
            SaveChangesButton.Command = new Command(async () => { await SaveExerciseLog(); });
        }

        private async Task SaveExerciseLog()
        {
            try
            {
                var ExerciseLogToSend = _newExerciseLog;

                ExerciseLogToSend.StartTime = new DateTime(ExerciseLogToSend.StartTimeDate.Year, ExerciseLogToSend.StartTimeDate.Month, ExerciseLogToSend.StartTimeDate.Day, ExerciseLogToSend.StartTimeSpan.Hours, ExerciseLogToSend.StartTimeSpan.Minutes, ExerciseLogToSend.StartTimeSpan.Seconds).ToUniversalTime();
                ExerciseLogToSend.FinishTime = new DateTime(ExerciseLogToSend.FinishTimeDate.Year, ExerciseLogToSend.FinishTimeDate.Month, ExerciseLogToSend.FinishTimeDate.Day, ExerciseLogToSend.FinishTimeSpan.Hours, ExerciseLogToSend.FinishTimeSpan.Minutes, ExerciseLogToSend.FinishTimeSpan.Seconds).ToUniversalTime();
                var client = new RestClient($"http://ubuntu:5000/BabyMonitor/AddExercise");

                var test = JsonConvert.SerializeObject(ExerciseLogToSend);
                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.AddJsonBody(ExerciseLogToSend);
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