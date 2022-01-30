using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Forms;
using static XamarinForms.MainPage;

namespace XamarinForms
{
    public partial class AddMilkLog : ContentPage
    {
        private MilkLog _newMilkLog = new MilkLog();
        public MilkLog NewMilkLog
        {
            get => _newMilkLog;
            set =>_newMilkLog = value;
        }

        public AddMilkLog()
        {
            InitializeComponent();
            NewMilkLog.MeasurementType = "mL";
            NewMilkLog.Type = "Breast Milk";
            NewMilkLog.StartTime = DateTime.Now;
            NewMilkLog.FinishTime = DateTime.Now;

            BindingContext = this;
            SaveChangesButton.Command = new Command(async () => { await SaveMilkLog(); });
        }

        private async Task SaveMilkLog()
        {
            try
            {
                var milkLog = _newMilkLog;
                var client = new RestClient($"http://ubuntu:5000/BabyMonitor/AddMilk");

                var test = JsonConvert.SerializeObject(milkLog);
                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.AddJsonBody(milkLog);
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