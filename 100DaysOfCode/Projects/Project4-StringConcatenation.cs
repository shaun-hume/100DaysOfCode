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
            var runAmount = 0;
                var stringBuilder = new StringBuilder();

            var now = DateTime.Now;
            for (int i = 0; i < 10000; i++)
            {
                stringBuilder.Clear();
                RunAndReportStringConcatenationWithStringBuilder(stringBuilder);
                runAmount = i;
            }
            var finishTime = DateTime.Now;

            Typewrite($"\r\nString Builder: It took {finishTime - now} seconds and there were {runAmount} tests run.");


            now = DateTime.Now;
            for (int i = 0; i < 10000; i++)
            {
            RunAndReportStringConcatenationWithPlusOperator();
                runAmount = i;
            }
            finishTime = DateTime.Now;

            Typewrite($"\r\nPlus Operator: It took {finishTime - now} seconds and there were {runAmount} tests run.");

            now = DateTime.Now;
            for (int i = 0; i < 10000; i++)
            {
                stringBuilder.Clear();
                RunAndReportStringConcatenationWithStringBuilder(stringBuilder);
                runAmount = i;
            }
            finishTime = DateTime.Now;

            Typewrite($"\r\nString Builder Test 2: It took {finishTime - now} seconds and there were {runAmount} tests run.");

            now = DateTime.Now;
            for (int i = 0; i < 10000; i++)
            {
                RunAndReportStringConcatenationWithPlusOperator();
                runAmount = i;
            }
            finishTime = DateTime.Now;

            Typewrite($"\r\nPlus Operator Test 2: It took {finishTime - now} seconds and there were {runAmount} tests run.");
        }

        private void RunAndReportStringConcatenationWithStringBuilder(StringBuilder stringBuilder)
        {

            for (int i = 0; i < 12; i++)
            {
                stringBuilder.Append(i);
            }

        }

        private void RunAndReportStringConcatenationWithPlusOperator()
        {
            var text = "";

            for (int i = 0; i < 12; i++)
            {
                text += i.ToString();
            }
        }
    }
}

