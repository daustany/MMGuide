namespace Quadrant.Interfaces
{
    /// <summary>
    /// Interface for calculating costs for dial movements.
    /// Follows Single Responsibility Principle - only responsible for cost calculations.
    /// </summary>
    public interface IDialCostCalculator
    {
        /// <summary>
        /// Calculates the cost for each dial position.
        /// </summary>
        /// <param name="current">Current dial positions</param>
        /// <param name="target">Target dial positions</param>
        /// <returns>Array of costs for each dial</returns>
        int[] CalculateCosts(int[] current, int[] target);

        /// <summary>
        /// Calculates the cost for a single dial movement.
        /// </summary>
        /// <param name="current">Current position</param>
        /// <param name="target">Target position</param>
        /// <returns>Minimum cost to move from current to target</returns>
        int CalculateCost(int current, int target);
    }
}
