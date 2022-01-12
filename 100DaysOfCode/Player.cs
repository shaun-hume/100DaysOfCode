using System;
using _100DaysOfCode.Projects.Interfaces;

namespace _100DaysOfCode
{
	public class Player
	{
		string Name;
		IInventoryService InventoryService;

		public Player(string name, IInventoryService inventoryService)
		{
			Name = name;
			InventoryService = inventoryService;
		}

		public string GetName()
        {
			return Name;
        }

		public double GetInventoryCount()
		{
			return InventoryService.GetCount();
		}

		public double GetInventoryWeight()
        {
			return InventoryService.GetWeight();
        }

        internal void AddItemToInventory(string nameOfItem, double weightOfItem)
        {
			InventoryService.AddItemToInventory(nameOfItem, weightOfItem);
        }
    }
}

