namespace SmoothLanding.Interfaces;

/// <summary>
/// Interface for reading input data from various sources.
/// Follows the Single Responsibility Principle by focusing only on input operations.
/// </summary>
public interface IInputReader
{
    /// <summary>
    /// Reads sequences of integers from the specified file path.
    /// </summary>
    /// <param name="filePath">Path to the input file</param>
    /// <returns>Collection of integer sequences</returns>
    Task<IEnumerable<int[]>> ReadSequencesAsync(string filePath);
}
