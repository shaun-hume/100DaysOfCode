using System;
using _100DaysOfCode.Projects.Interfaces;
using _100DaysOfCode.Projects.Services;
using _100DaysOfCode.Projects.Shared;
using Autofac;

namespace _100DaysOfCode.Projects
{
	public class Project3_DependencyInjection
	{
		public Project3_DependencyInjection()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<NotificationService>().InstancePerLifetimeScope();
			var container = builder.Build();

			var consoleNotificationService = container.Resolve<NotificationService>();

			Typewrite("Welcome, what is your name?");
			var username = Console.ReadLine();
			var user1 = new User(username, consoleNotificationService);
			consoleNotificationService.NotifyUserNameChanged(user1);
			Typewrite("What would you like to change your name to?");
			username = Console.ReadLine();

			user1.ChangeUsername(username);

		}
	}
}