using SQLite;
using System;

namespace XamarinForms.Logs
{
	public class Logs
	{
		public Logs()
		{
		}

        public class MilkLog
        {
            public int ID { get; set; }
            public string Type { get; set; }
            public decimal Amount { get; set; }
            public decimal EstimatedAmount { get; set; }
            public string MeasurementType { get; set; }
            public string Comment { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime FinishTime { get; set; }
            public bool SuccessfullySentToApi { get; set; } = false;
        }

        public class SleepLog
        {
            public int ID { get; set; }
            public string Comment { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime FinishTime { get; set; }
            public bool SuccessfullySentToApi { get; set; } = false;
        }

        public class ExerciseLog
        {
            public int ID { get; set; }
            public string Type { get; set; }
            public string Comment { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime FinishTime { get; set; }
            public bool SuccessfullySentToApi { get; set; } = false;
        }

        public class PooLog
        {
            public int ID { get; set; }
            public string Type { get; set; }
            public string Comment { get; set; }
            public string Colour { get; set; }
            public DateTime OccurrenceTime { get; set; }
            public bool SuccessfullySentToApi { get; set; } = false;
        }
    }
}