using System;
using _100DaysOfCode.Projects.Interfaces;

namespace _100DaysOfCode.Projects.Services
{
	public class TextCommandService:ITextCommandService
	{
		public TextCommandService()
		{
		}

		public bool ProcessCommand(Player player, string command)
        {
            var GameOver = false;

            if (command.ToLower().Contains("inventory") && command.ToLower().Contains("add"))
            {
                Typewrite("What would you like to add to inventory?");
                var nameOfItem = Console.ReadLine();
                Typewrite("What is the weight of the item (in kg)?");
                double weightOfItem = Double.Parse(Console.ReadLine());

                player.AddItemToInventory(nameOfItem, weightOfItem);

                Typewrite($"Item added to inventory. You currently have {player.GetInventoryCount()} items in your inventory, weighing a total of {player.GetInventoryWeight()}kg.");

                return GameOver;
            }

            if (command.ToLower().Contains("go left"))
            {
                Typewrite("You are eaten by a hungry Allosaurus! 🦖");
                Typewrite("GAME OVER");
                GameOver = true;

                return GameOver;
            }

            return GameOver;
        }
	}
}

