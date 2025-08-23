using StoneSplitting.Models;

namespace StoneSplitting.Services;

/// <summary>
/// Interface for parsing input data into stone piles.
/// </summary>
public interface IInputParser
{
    /// <summary>
    /// Parses the input file and returns a collection of stone piles.
    /// </summary>
    /// <param name="filePath">Path to the input file</param>
    /// <returns>A collection of parsed stone piles</returns>
    Task<IEnumerable<StonePile>> ParseInputAsync(string filePath);
}

/// <summary>
/// Interface for calculating the maximum number of splits for a pile.
/// </summary>
public interface ISplitCalculator
{
    /// <summary>
    /// Calculates the maximum number of splits possible for a given pile.
    /// </summary>
    /// <param name="pile">The stone pile to analyze</param>
    /// <returns>The result containing the maximum splits</returns>
    SplitResult CalculateMaxSplits(StonePile pile);
}

/// <summary>
/// Interface for the main stone splitting calculation service.
/// </summary>
public interface IStoneSplittingCalculator
{
    /// <summary>
    /// Processes the input file and calculates the concatenated result of all maximum splits.
    /// </summary>
    /// <param name="inputFilePath">Path to the input file</param>
    /// <returns>The concatenated result of all maximum splits</returns>
    Task<string> CalculateMaximumSplitsAsync(string inputFilePath);
}
