using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net;

namespace XamarinForms
{
    public partial class App : Application
    {
        List<MilkLog> milkLogs = new List<MilkLog>();
        public App ()
        {
            InitializeComponent();

            MainPage = new MainPage();

        }

        protected override void OnStart ()
        {
            milkLogs = getListOfMilkLogs();
            this.BindingContext = milkLogs;
        }

        private List<MilkLog> getListOfMilkLogs()
        {
            var client = new WebClient();
            var response = client.DownloadString("http://ubuntu:5000/GetMilk");
            var x = JsonConvert.DeserializeObject<List<MilkLog>>(response);
            return x;
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }

    public class MilkLog
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string MeasurementType { get; set; }
        public string Comment { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }
}

