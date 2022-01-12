using System;
using _100DaysOfCode.Projects.Interfaces;
using _100DaysOfCode.Projects.Services;
using Autofac;

namespace _100DaysOfCode.Projects
{
	public class Project1_TextAdventure
	{
        bool GameOver;

        public Project1_TextAdventure()
		{
            var builder = new ContainerBuilder();

            builder.RegisterType<InventoryService>().InstancePerLifetimeScope();
            builder.RegisterType<TextCommandService>().InstancePerLifetimeScope();
            var container = builder.Build();

            var inventoryService = container.Resolve<InventoryService>();
            var textCommandService = container.Resolve<TextCommandService>();

            GameOver = false;
			RunGame(inventoryService, textCommandService);
		}

        private void RunGame(IInventoryService inventoryService, ITextCommandService textCommandService)
        {
            Console.Clear();
            Typewrite("Text Adventure Game!");
            Typewrite("What is your name?");

            Player? player = new(name: Console.ReadLine(), inventoryService);
            Console.Clear();

            while (!GameOver)
            {
                Typewrite($"Hello {player.GetName()}. What would you like to do?");

                var command = Console.ReadLine();

                GameOver = textCommandService.ProcessCommand(player, command);
            }
        }
    }
}

