using System;
using _100DaysOfCode.Projects.Interfaces;
using _100DaysOfCode.Projects.Shared;

namespace _100DaysOfCode.Projects.Services
{
	public class NotificationService : INotificationService
	{
		public NotificationService()
		{

		}

		public void NotifyUserNameChanged(User user)
        {
			SendConsoleNotification(user);
			SendWebNotification(user);
        }

        private void SendConsoleNotification(User user)
        {
            Typewrite($"Username has been changed to: {user.Username}");
        }

        private void SendWebNotification(User user)
        {
            Typewrite($"Web Updates sent for: {user.Username}");
        }
    }
}

