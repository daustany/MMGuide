namespace Quadrant.Models
{
    /// <summary>
    /// Represents a set of dial positions with current and target states.
    /// Immutable value object following good design practices.
    /// </summary>
    public class DialSet
    {
        public int[] Current { get; }
        public int[] Target { get; }

        public DialSet(int[] current, int[] target)
        {
            Current = current ?? throw new ArgumentNullException(nameof(current));
            Target = target ?? throw new ArgumentNullException(nameof(target));

            if (current.Length != target.Length)
                throw new ArgumentException("Current and target arrays must have the same length");

            if (current.Length == 0)
                throw new ArgumentException("Arrays cannot be empty");
        }

        /// <summary>
        /// Creates a deep copy of the dial set to ensure immutability.
        /// </summary>
        public static DialSet Create(int[] current, int[] target)
        {
            var currentCopy = new int[current.Length];
            var targetCopy = new int[target.Length];

            Array.Copy(current, currentCopy, current.Length);
            Array.Copy(target, targetCopy, target.Length);

            return new DialSet(currentCopy, targetCopy);
        }

        public override string ToString()
        {
            return $"Current: [{string.Join(", ", Current)}], Target: [{string.Join(", ", Target)}]";
        }
    }
}
