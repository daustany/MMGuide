namespace SmoothLanding.Interfaces;

/// <summary>
/// Interface for processing sequences according to the puzzle rules.
/// Follows the Single Responsibility Principle by focusing only on sequence processing logic.
/// </summary>
public interface ISequenceProcessor
{
    /// <summary>
    /// Processes a sequence to count smaller numbers to the right of each element.
    /// </summary>
    /// <param name="sequence">Input sequence of integers</param>
    /// <returns>Array of counts for each position</returns>
    int[] ProcessSequence(int[] sequence);
}
