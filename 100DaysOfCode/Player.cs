using System;
namespace _100DaysOfCode
{
	public class Player
	{
		string Name;
		Inventory Inventory = new Inventory(); 

		public Player(string name)
		{
			Name = name;
		}

		public string GetName()
        {
			return Name;
        }

		public double GetInventoryCount()
		{
			return Inventory.GetCount();
		}

		public double GetInventoryWeight()
        {
			return Inventory.GetWeight();
        }

        internal void AddItemToInventory(string nameOfItem, double weightOfItem)
        {
			Inventory.AddItemToInventory(nameOfItem, weightOfItem);
        }
    }
}

