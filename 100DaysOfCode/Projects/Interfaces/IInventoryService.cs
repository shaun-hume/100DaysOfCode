using System;
namespace _100DaysOfCode.Projects.Interfaces
{
	public interface IInventoryService
	{
		int GetCount();
		double GetWeight();
		void AddItemToInventory(string name, double weight);
	}
}

