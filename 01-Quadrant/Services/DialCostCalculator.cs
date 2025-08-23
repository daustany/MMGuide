using Quadrant.Interfaces;
using Quadrant.Models;

namespace Quadrant.Services
{
    /// <summary>
    /// Service for calculating dial movement costs.
    /// Follows Single Responsibility Principle - only handles cost calculation logic.
    /// Open/Closed Principle - can be extended for different cost calculation strategies.
    /// </summary>
    public class DialCostCalculator : IDialCostCalculator
    {
        private readonly DialConfiguration _configuration;

        public DialCostCalculator(DialConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public int[] CalculateCosts(int[] current, int[] target)
        {
            if (current == null)
                throw new ArgumentNullException(nameof(current));
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (current.Length != target.Length)
                throw new ArgumentException("Current and target arrays must have the same length");
            if (current.Length != _configuration.DialCount)
                throw new ArgumentException($"Arrays must have exactly {_configuration.DialCount} elements");

            var costs = new int[current.Length];
            for (int i = 0; i < current.Length; i++)
            {
                ValidateDialValue(current[i], nameof(current));
                ValidateDialValue(target[i], nameof(target));
                costs[i] = CalculateCost(current[i], target[i]);
            }
            return costs;
        }

        public int CalculateCost(int current, int target)
        {
            ValidateDialValue(current, nameof(current));
            ValidateDialValue(target, nameof(target));

            var dialRange = _configuration.MaxDialValue - _configuration.MinDialValue + 1;

            // Calculate cost turning right (decrementing with wrap-around)
            int right = (current - target + dialRange) % dialRange;

            // Calculate cost turning left (incrementing with wrap-around)
            int left = (target - current + dialRange) % dialRange;

            return Math.Min(right, left);
        }

        private void ValidateDialValue(int value, string parameterName)
        {
            if (!_configuration.IsValidDialValue(value))
                throw new ArgumentOutOfRangeException(parameterName,
                    $"Dial value {value} is outside valid range [{_configuration.MinDialValue}-{_configuration.MaxDialValue}]");
        }
    }
}
