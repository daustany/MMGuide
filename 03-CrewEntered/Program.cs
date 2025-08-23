using System;
using System.IO;
using CrewEntered.Services;

namespace CrewEntered
{
    /// <summary>
    /// Main program entry point for the dependency analysis console application.
    /// This application reads symbol dependencies from input.txt and determines if each line
    /// represents a valid dependency graph (no cycles).
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting Dependency Analysis...");

                // Initialize the dependency analyzer service
                var dependencyAnalyzer = new DependencyAnalyzerService();

                // Read and process the input file
                string inputFilePath = Path.Combine("Input", "input.txt");

                if (!File.Exists(inputFilePath))
                {
                    Console.WriteLine($"Error: Input file '{inputFilePath}' not found.");
                    return;
                }

                // Process the file and get the final result
                int result = dependencyAnalyzer.AnalyzeDependenciesFromFile(inputFilePath);

                Console.WriteLine($"\nFinal Result: {result}");
                Console.WriteLine("Analysis complete!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
