namespace Algorithms
{
    public struct KnapsackItem
    {
        public readonly int Value;
        public readonly int Weight;
        public readonly int Profit;

        public KnapsackItem(int value, int weight)
        {
            Value = value;
            Weight = weight;
            Profit = value / weight;
        }
    }
}
