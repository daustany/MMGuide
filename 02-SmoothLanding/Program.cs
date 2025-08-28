using SmoothLanding.Services;
using SmoothLanding.Models;
using SmoothLanding.Interfaces;

namespace SmoothLanding;

/// <summary>
/// Main entry point for the Smooth Landing puzzle solver.
/// Reads numeric sequences from input.txt and calculates the sum of rounded-up medians
/// based on the count of smaller numbers to the right for each element.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine();
            Console.WriteLine("=== Smooth Landing Puzzle Solver ===");
            Console.WriteLine();

            // Initialize services using dependency injection pattern
            IInputReader inputReader = new FileInputReader();
            ISequenceProcessor sequenceProcessor = new SequenceProcessor();
            IMedianCalculator medianCalculator = new MedianCalculator();

            var puzzleSolver = new PuzzleSolver(inputReader, sequenceProcessor, medianCalculator);

            // Process the puzzle
            string inputFilePath = Path.Combine("Input", "input.txt");
            var result = puzzleSolver.SolvePuzzle(inputFilePath);

            // Console.WriteLine("Processing Details:");
            // for (int i = 0; i < result.ProcessingDetails.Count; i++)
            // {
            //     var detail = result.ProcessingDetails[i];
            //     Console.WriteLine($"Array {i + 1}: [{string.Join(", ", detail.OriginalSequence)}]");
            //     Console.WriteLine($"  Counts: [{string.Join(", ", detail.CountsArray)}]");
            //     Console.WriteLine($"  Median: {detail.Median}");
            //     Console.WriteLine();
            // }

            // Display results
            Console.WriteLine($"Final Result: {result.FinalSum}");
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
    }
}
