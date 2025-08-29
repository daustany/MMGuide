using ConnectedRegions.Interfaces;
using ConnectedRegions.Services;

namespace ConnectedRegions;

/// <summary>
/// Main program class for the Connected Regions application
/// </summary>
public class Program
{
    private const string DefaultInputFileName = "Input/input.txt";

    /// <summary>
    /// Main entry point of the application
    /// </summary>
    /// <param name="args">Command line arguments</param>
    public static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine();
            Console.WriteLine("=== Connected Regions Analyzer ===");
            Console.WriteLine("Finding the largest connected region in the map...\n");

            // Create service instances following dependency injection pattern
            IInputReader inputReader = new InputReader();
            IConnectedRegionFinder regionFinder = new ConnectedRegionFinder();

            // Determine input file name from command line args or use default
            string inputFileName = args.Length > 0 ? args[0] : DefaultInputFileName;

            // Read the grid map from the input file
            Console.WriteLine($"Reading input from: {inputFileName}");
            var gridMap = await inputReader.ReadGridMapAsync(inputFileName);
            Console.WriteLine($"Grid size: {gridMap.Rows} x {gridMap.Columns}");

            // Find all connected regions
            var regionSizes = regionFinder.FindAllConnectedRegionSizes(gridMap).ToList();
            Console.WriteLine($"Number of connected regions found: {regionSizes.Count}");

            if (regionSizes.Count > 0)
            {
                // Find and display the largest region
                int largestRegionSize = regionSizes.Max();
                Console.WriteLine($"Largest connected region size: {largestRegionSize}");

                // Display additional statistics
                Console.WriteLine("\n=== Region Statistics ===");
                Console.WriteLine($"Total regions: {regionSizes.Count}");
                Console.WriteLine($"Smallest region: {regionSizes.Min()}");
                Console.WriteLine($"Average region size: {regionSizes.Average():F2}");
                Console.WriteLine($"Total connected cells: {regionSizes.Sum()}");

                // Display the final answer
                Console.WriteLine("\n=== FINAL RESULT ===");
                Console.WriteLine(largestRegionSize);
            }
            else
            {
                Console.WriteLine("No connected regions found in the map.");
                Console.WriteLine("\n=== FINAL RESULT ===");
                Console.WriteLine("0");
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Please ensure the input file exists in the correct location.");
            Environment.Exit(1);
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Error parsing input file: {ex.Message}");
            Console.WriteLine("Please check that the input file contains valid space-separated integers.");
            Environment.Exit(1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            Environment.Exit(1);
        }
    }
}
