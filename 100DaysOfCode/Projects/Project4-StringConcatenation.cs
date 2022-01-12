using System;
using System.Text;

namespace _100DaysOfCode.Projects
{
	public class Project4_StringConcatenation
	{
		public Project4_StringConcatenation()
		{
            Console.Clear();
            Typewrite($"Testing is beginning");

            RunAndReportStringConcatenationWithPlusOperator();
			RunAndReportStringConcatenationWithStringBuilder();
		}

        private void RunAndReportStringConcatenationWithStringBuilder()
        {
            var now = DateTime.Now;
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < 100000; i++)
            {
                stringBuilder.Append(i);
            }

            var finishTime = DateTime.Now;

            Typewrite($"\r\nString Builder: It took {finishTime - now} seconds");
        }

        private void RunAndReportStringConcatenationWithPlusOperator()
        {
            var now = DateTime.Now;
            var text = "";

            for (int i = 0; i < 100000; i++)
            {
                text += i.ToString();
            }

            var finishTime = DateTime.Now;

            Typewrite($"\r\nPlus Operator: It took {finishTime - now} seconds");
        }
    }
}

