using SmoothLanding.Interfaces;
using SmoothLanding.Models;

namespace SmoothLanding.Services;

/// <summary>
/// Main orchestrator class that coordinates the puzzle solving process.
/// Follows the Dependency Inversion Principle by depending on abstractions rather than concrete implementations.
/// </summary>
public class PuzzleSolver
{
    private readonly IInputReader _inputReader;
    private readonly ISequenceProcessor _sequenceProcessor;
    private readonly IMedianCalculator _medianCalculator;

    /// <summary>
    /// Initializes a new instance of PuzzleSolver with required dependencies.
    /// </summary>
    /// <param name="inputReader">Service for reading input data</param>
    /// <param name="sequenceProcessor">Service for processing sequences</param>
    /// <param name="medianCalculator">Service for calculating medians</param>
    public PuzzleSolver(
        IInputReader inputReader,
        ISequenceProcessor sequenceProcessor,
        IMedianCalculator medianCalculator)
    {
        _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
        _sequenceProcessor = sequenceProcessor ?? throw new ArgumentNullException(nameof(sequenceProcessor));
        _medianCalculator = medianCalculator ?? throw new ArgumentNullException(nameof(medianCalculator));
    }

    /// <summary>
    /// Solves the complete puzzle by reading sequences, processing them, and calculating the final result.
    /// </summary>
    /// <param name="inputFilePath">Path to the input file</param>
    /// <returns>Complete puzzle result with details</returns>
    public async Task<PuzzleResult> SolvePuzzleAsync(string inputFilePath)
    {
        var result = new PuzzleResult();

        // Read all sequences from the input file
        var sequences = await _inputReader.ReadSequencesAsync(inputFilePath);

        int totalSum = 0;

        foreach (var sequence in sequences)
        {
            // Process the sequence to get counts of smaller numbers to the right
            var countsArray = _sequenceProcessor.ProcessSequence(sequence);

            // Calculate the rounded-up median of the counts array
            var median = _medianCalculator.CalculateRoundedUpMedian(countsArray);

            // Add to the total sum
            totalSum += median;

            // Store processing details for reporting
            result.ProcessingDetails.Add(new SequenceProcessingDetail
            {
                OriginalSequence = sequence,
                CountsArray = countsArray,
                Median = median
            });
        }

        result.FinalSum = totalSum;
        return result;
    }

    /// <summary>
    /// Synchronous version of SolvePuzzleAsync for simpler usage.
    /// </summary>
    /// <param name="inputFilePath">Path to the input file</param>
    /// <returns>Complete puzzle result with details</returns>
    public PuzzleResult SolvePuzzle(string inputFilePath)
    {
        return SolvePuzzleAsync(inputFilePath).GetAwaiter().GetResult();
    }
}
