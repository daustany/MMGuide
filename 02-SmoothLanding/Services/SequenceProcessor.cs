using SmoothLanding.Interfaces;

namespace SmoothLanding.Services;

/// <summary>
/// Implementation of ISequenceProcessor that processes sequences according to the puzzle rules.
/// For each number, counts how many numbers to its right are strictly smaller.
/// </summary>
public class SequenceProcessor : ISequenceProcessor
{
    /// <summary>
    /// Processes a sequence to count smaller numbers to the right of each element.
    /// Time complexity: O(n²) where n is the length of the sequence.
    /// </summary>
    /// <param name="sequence">Input sequence of integers</param>
    /// <returns>Array of counts for each position</returns>
    /// <exception cref="ArgumentNullException">Thrown when sequence is null</exception>
    public int[] ProcessSequence(int[] sequence)
    {
        if (sequence == null)
        {
            throw new ArgumentNullException(nameof(sequence));
        }

        if (sequence.Length == 0)
        {
            return Array.Empty<int>();
        }

        var result = new int[sequence.Length];

        // For each element in the sequence
        for (int i = 0; i < sequence.Length; i++)
        {
            int count = 0;

            // Count elements to the right that are smaller
            for (int j = i + 1; j < sequence.Length; j++)
            {
                if (sequence[j] < sequence[i])
                {
                    count++;
                }
            }

            result[i] = count;
        }

        return result;
    }
}
