namespace _100DaysOfCode
{
    internal class Item
    {
        string Name;
        double Weight;

        public Item(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }

        public double GetWeight()
        {
            return Weight;
        }
    }
}