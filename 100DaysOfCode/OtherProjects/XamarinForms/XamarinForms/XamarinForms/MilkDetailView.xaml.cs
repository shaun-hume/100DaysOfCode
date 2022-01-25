using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using static XamarinForms.MainPage;
using RestSharp;
using Newtonsoft.Json;
using System.Net;

namespace XamarinForms
{
    public partial class MilkDetailView : ContentPage
    {
        public MilkLog milkLog;
        public MilkDetailView(int id)
        {
            InitializeComponent();
            UpdateMilkLog(id);

        }

        private void UpdateMilkLog(int id)
        {
            var client = new WebClient();
            var response = client.DownloadString(string.Concat("http://ubuntu:5000/BabyMonitor/GetMilk/", id));
            milkLog = JsonConvert.DeserializeObject<MilkLog>(response);

            EstimatedAmount.Text = milkLog.EstimatedAmount + milkLog.MeasurementType;
            ActualAmount.Text = milkLog.Amount + milkLog.MeasurementType;
            Comment.Text = milkLog.Comment;
            StartTime.Text = milkLog.StartTime.ToShortTimeString();
            FinishTime.Text = milkLog.FinishTime.ToShortTimeString();
        }
    }
}

