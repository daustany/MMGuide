using System.Text;
using StoneSplitting.Models;

namespace StoneSplitting.Services;

/// <summary>
/// Main orchestration service that coordinates input parsing and split calculation.
/// Implements the Single Responsibility Principle by focusing on workflow coordination.
/// Follows the Dependency Inversion Principle by depending on abstractions.
/// </summary>
public class StoneSplittingCalculator : IStoneSplittingCalculator
{
    private readonly IInputParser _inputParser;
    private readonly ISplitCalculator _splitCalculator;

    /// <summary>
    /// Initializes a new instance of StoneSplittingCalculator with required dependencies.
    /// </summary>
    /// <param name="inputParser">Service for parsing input data</param>
    /// <param name="splitCalculator">Service for calculating splits</param>
    public StoneSplittingCalculator(IInputParser inputParser, ISplitCalculator splitCalculator)
    {
        _inputParser = inputParser ?? throw new ArgumentNullException(nameof(inputParser));
        _splitCalculator = splitCalculator ?? throw new ArgumentNullException(nameof(splitCalculator));
    }

    /// <summary>
    /// Processes the entire workflow: parse input, calculate splits, and concatenate results.
    /// </summary>
    /// <param name="inputFilePath">Path to the input file</param>
    /// <returns>Concatenated string of all maximum splits</returns>
    public async Task<string> CalculateMaximumSplitsAsync(string inputFilePath)
    {
        Console.WriteLine();
        Console.WriteLine("Starting stone splitting calculation...");

        // Step 1: Parse input file
        Console.WriteLine($"Reading input from: {inputFilePath}");
        var stonePiles = await _inputParser.ParseInputAsync(inputFilePath);

        var pilesList = stonePiles.ToList();
        Console.WriteLine($"Parsed {pilesList.Count} stone piles");

        // Step 2: Calculate maximum splits for each pile
        var results = new List<SplitResult>();
        var resultBuilder = new StringBuilder();

        for (int i = 0; i < pilesList.Count; i++)
        {
            var pile = pilesList[i];
            //Console.WriteLine($"Processing pile {i + 1}/{pilesList.Count}: {pile.InitialSize} stones, {pile.SplittingNumbers.Count} splitting numbers");

            var result = _splitCalculator.CalculateMaxSplits(pile);
            results.Add(result);

            // Append to final result string
            resultBuilder.Append(result.MaxSplits);

            //Console.WriteLine($"  → Maximum splits: {result.MaxSplits}");
        }

        // Step 3: Return concatenated result
        var finalResult = resultBuilder.ToString();
        Console.WriteLine($"\nCalculation complete!");
        Console.WriteLine();

        //Console.WriteLine($"Individual results: {string.Join(", ", results.Select(r => r.MaxSplits))}");

        return finalResult;
    }
}
