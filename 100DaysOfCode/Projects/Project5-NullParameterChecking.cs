using System;
namespace _100DaysOfCode.Projects
{
	public class Project5_NullParameterChecking
	{
		public Project5_NullParameterChecking()
		{
			List<string> list = null;

			TriggerNullParameterException(list);
		}

        private void TriggerNullParameterException(List<string> testParameter!!)
        {
			return;
        }
    }
}

