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
        List<DinosaurResponse> dinos = new List<DinosaurResponse>();
        public App ()
        {
            InitializeComponent();

            MainPage = new MainPage();

        }

        protected override void OnStart ()
        {
            dinos = getListOfDinosaurs();
            this.BindingContext = dinos;
        }

        private List<DinosaurResponse> getListOfDinosaurs()
        {
            //var client = new RestClient("https://dinosaur-facts-api.shultzlab.com/dinosaurs");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //IRestResponse response = client.Execute(request);
            //var x = JsonSerializer.Deserialize<DinosaurResponse>(response.Content);
            //return x;

            var client = new WebClient();
            var response = client.DownloadString("https://dinosaur-facts-api.shultzlab.com/dinosaurs");
            var x = JsonConvert.DeserializeObject<List<DinosaurResponse>>(response);
            return x;
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }

        public class DinosaurResponse
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }

    public class MilkLog
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string MeasurementType { get; set; }
        public string? Comment { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }
}

