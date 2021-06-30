namespace Algorithms
{
    public struct KnapsackItem
    {
        public readonly int Value;
        public readonly int Weight;
        public readonly double ValuePerWeight;

        public KnapsackItem(int value, int weight)
        {
            Value = value;
            Weight = weight;
            ValuePerWeight = value / weight;
        }
    }
}
