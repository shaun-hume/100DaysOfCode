using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static XamarinForms.MainPage;

namespace XamarinForms.Helpers
{
	public class DataHandler
	{
		public DataHandler()
		{
		}

		public static async Task TryApiWithRequest(Logs.Logs.MilkLog log)
        {
			// Try to execute api call passed through
			// if a new record and it fails, store the new record into local database

			BabyTimeLocalDatabase database = await BabyTimeLocalDatabase.Instance;
			await database.SaveItemAsync(log);

			var listOfLogsLocally = await database.GetItemsAsync();
		}

		public static async Task<List<GenericLog>> ReturnOfflineLogs(DateTime CurrentlySelectedDate)
        {
			BabyTimeLocalDatabase database = await BabyTimeLocalDatabase.Instance;
			var milkLogs = await database.GetItemsAsync();
			var genericLogs = milkLogs
				.Where(x => x.StartTime.ToLocalTime() >= CurrentlySelectedDate && x.StartTime.ToLocalTime() < CurrentlySelectedDate.AddDays(1))
				.Select(x => new GenericLog()
				{
					ID = x.ID,
					Type = "Milk",
					Icon = "🍼",
					StartTime = x.StartTime.ToLocalTime(),
					FinishTime = x.FinishTime.ToLocalTime(),
					SummaryOfEvent = $"Fed {x.Amount}{x.MeasurementType} (This log is on this device only)",
					SuccessfullySentToApi = false
				}).ToList();

			return genericLogs;
		}
	}
}