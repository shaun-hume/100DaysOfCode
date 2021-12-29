using _100DaysOfCode;
using System;

namespace _100DaysOfCode
{
    public class TextAdventure
    {
        public TextAdventure()
        {

            var GameOver = false;

            Console.WriteLine("What is your name?");

            Player? player = new(name: Console.ReadLine());

            Console.WriteLine($"Hello {player.GetName()}");

            while (!GameOver)
            {
                Console.WriteLine("What would you like to do?");

                var command = Console.ReadLine();

                if (command.ToLower().Contains("inventory") && command.ToLower().Contains("add"))
                {
                    Console.WriteLine("What would you like to add to inventory?");
                    var nameOfItem = Console.ReadLine();
                    Console.WriteLine("What is the weight of the item (in kg)?");
                    double weightOfItem = Double.Parse(Console.ReadLine());

                    player.AddItemToInventory(nameOfItem, weightOfItem);

                    Console.WriteLine($"Item added to inventory. You currently have {player.GetInventoryCount()} items in your inventory, weighing a total of {player.GetInventoryWeight()}kg.");
                }

                if (command.ToLower().Contains("go left"))
                {
                    Console.WriteLine("You are eaten by a hungry Allosaurus! 🦖");
                    Console.WriteLine("GAME OVER");
                    GameOver = true;
                }
            }
        }
    }
}

