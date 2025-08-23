namespace SmoothLanding.Interfaces;

/// <summary>
/// Interface for calculating medians with rounding up functionality.
/// Follows the Single Responsibility Principle by focusing only on median calculations.
/// </summary>
public interface IMedianCalculator
{
    /// <summary>
    /// Calculates the median of an array and rounds it up to the nearest integer.
    /// </summary>
    /// <param name="values">Array of integers</param>
    /// <returns>Rounded up median value</returns>
    int CalculateRoundedUpMedian(int[] values);
}
