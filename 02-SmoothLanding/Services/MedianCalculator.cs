using SmoothLanding.Interfaces;

namespace SmoothLanding.Services;

/// <summary>
/// Implementation of IMedianCalculator that calculates medians and rounds them up.
/// Handles both even and odd length arrays according to standard median calculation rules.
/// </summary>
public class MedianCalculator : IMedianCalculator
{
    /// <summary>
    /// Calculates the median of an array and rounds it up to the nearest integer.
    /// For even length arrays, takes the average of the two middle elements.
    /// For odd length arrays, takes the middle element.
    /// </summary>
    /// <param name="values">Array of integers</param>
    /// <returns>Rounded up median value</returns>
    /// <exception cref="ArgumentNullException">Thrown when values is null</exception>
    /// <exception cref="ArgumentException">Thrown when values array is empty</exception>
    public int CalculateRoundedUpMedian(int[] values)
    {
        if (values == null)
        {
            throw new ArgumentNullException(nameof(values));
        }

        if (values.Length == 0)
        {
            throw new ArgumentException("Cannot calculate median of empty array", nameof(values));
        }

        // Create a copy to avoid modifying the original array
        var sortedValues = new int[values.Length];
        Array.Copy(values, sortedValues, values.Length);
        Array.Sort(sortedValues);

        double median;

        if (sortedValues.Length % 2 == 0)
        {
            // Even number of elements: average of two middle elements
            int middleIndex1 = sortedValues.Length / 2 - 1;
            int middleIndex2 = sortedValues.Length / 2;
            median = (sortedValues[middleIndex1] + sortedValues[middleIndex2]) / 2.0;
        }
        else
        {
            // Odd number of elements: middle element
            int middleIndex = sortedValues.Length / 2;
            median = sortedValues[middleIndex];
        }

        // Round up to the nearest integer
        return (int)Math.Ceiling(median);
    }
}
