using System;
using _100DaysOfCode.Projects.Interfaces;
using _100DaysOfCode.Projects.Services;

namespace _100DaysOfCode.Projects.Shared
{
	public class User
	{
		private INotificationService _notificationService;
		public string Username { get; set; }
		public User(string username, INotificationService notificationService)
		{
			Username = username;
			_notificationService = notificationService;
		}

		public void ChangeUsername(string newUsername)
        {
			Username = newUsername;
			_notificationService.NotifyUserNameChanged(this);
        }
	}
}

