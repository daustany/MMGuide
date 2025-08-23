namespace Quadrant.Models
{
    /// <summary>
    /// Represents the result of a dial set calculation.
    /// Immutable value object with computed properties for display.
    /// </summary>
    public class DialSetResult
    {
        public int Index { get; }
        public int[] Current { get; }
        public int[] Target { get; }
        public int[] Costs { get; }
        public int Sum { get; }

        // Computed properties for string representation
        public string CurrentStr => string.Join("", Current);
        public string TargetStr => string.Join("", Target);
        public string CostsStr => string.Join("+", Costs);

        public DialSetResult(int index, int[] current, int[] target, int[] costs, int sum)
        {
            if (index <= 0)
                throw new ArgumentException("Index must be positive", nameof(index));

            Index = index;
            Current = current ?? throw new ArgumentNullException(nameof(current));
            Target = target ?? throw new ArgumentNullException(nameof(target));
            Costs = costs ?? throw new ArgumentNullException(nameof(costs));
            Sum = sum;

            ValidateArrayLengths();
            ValidateSum();
        }

        private void ValidateArrayLengths()
        {
            if (Current.Length != Target.Length || Current.Length != Costs.Length)
                throw new ArgumentException("All arrays must have the same length");
        }

        private void ValidateSum()
        {
            var calculatedSum = Costs.Sum();
            if (Sum != calculatedSum)
                throw new ArgumentException($"Sum {Sum} does not match calculated sum {calculatedSum}");
        }

        public override string ToString()
        {
            return $"{Index:D2}. {CurrentStr} -> {TargetStr} | per-digit costs: {CostsStr} = {Sum}";
        }
    }
}
