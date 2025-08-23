namespace Quadrant.Models
{
    /// <summary>
    /// Configuration class for dial calculation parameters.
    /// Allows for easy extension and configuration changes.
    /// </summary>
    public class DialConfiguration
    {
        public int DialCount { get; }
        public int MaxDialValue { get; }
        public int MinDialValue { get; }

        public DialConfiguration(int dialCount = 4, int minDialValue = 0, int maxDialValue = 9)
        {
            if (dialCount <= 0)
                throw new ArgumentException("Dial count must be positive", nameof(dialCount));
            if (minDialValue < 0)
                throw new ArgumentException("Min dial value cannot be negative", nameof(minDialValue));
            if (maxDialValue <= minDialValue)
                throw new ArgumentException("Max dial value must be greater than min dial value", nameof(maxDialValue));

            DialCount = dialCount;
            MinDialValue = minDialValue;
            MaxDialValue = maxDialValue;
        }

        /// <summary>
        /// Default configuration for standard 4-digit dials (0-9).
        /// </summary>
        public static DialConfiguration Default => new DialConfiguration();

        /// <summary>
        /// Validates if a dial value is within the configured range.
        /// </summary>
        public bool IsValidDialValue(int value)
        {
            return value >= MinDialValue && value <= MaxDialValue;
        }
    }
}
