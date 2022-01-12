using _100DaysOfCode.Projects.Interfaces;

namespace _100DaysOfCode
{
    internal class InventoryService : IInventoryService
    {
        List<Item> Items = new List<Item>();

        public double GetWeight()
        {
            var weight = 0.0;

            foreach(var item in Items)
            {
                weight += item.GetWeight();
            }

            return weight;
        }

        public int GetCount()
        {
            return Items.Count;
        }


        public void AddItemToInventory(string name, double weight)
        {
            Items.Add(new Item(name, weight));
        }
    }
}