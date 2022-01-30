using System;
using System.Threading.Tasks;
using RestSharp;
using Xamarin.Forms;
using static XamarinForms.MainPage;

namespace XamarinForms
{
    public partial class AddMilkLog : ContentPage
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal EstimatedAmount { get; set; }
        public string MeasurementType { get; set; }
        public string Comment { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }

        public AddMilkLog()
        {
            InitializeComponent();
            BindingContext = this;
            SaveChangesButton.Command = new Command(async () => { await SaveMilkLog(); });
        }

        private async Task SaveMilkLog()
        {
            var milkLog = new MilkLog
            {
                Type = Type,
                Amount = Amount,
                EstimatedAmount = EstimatedAmount,
                MeasurementType = "mL",
                Comment = Comment,
                StartTime = StartTime,
                FinishTime = FinishTime
            };
            var client = new RestClient($"http://ubuntu:5000/BabyMonitor/AddMilk");
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddJsonBody(milkLog);
            var response = await client.PostAsync(request);
            await Navigation.PopAsync();
        }
    }
}