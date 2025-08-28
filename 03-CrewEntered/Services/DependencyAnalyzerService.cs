using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CrewEntered.Interfaces;
using CrewEntered.Models;

namespace CrewEntered.Services
{
    /// <summary>
    /// Service class for analyzing symbol dependencies and detecting cycles.
    /// Implements the IDependencyAnalyzer interface to provide comprehensive
    /// dependency analysis functionality.
    /// </summary>
    public class DependencyAnalyzerService : IDependencyAnalyzer
    {
        private readonly ICycleDetector _cycleDetector;

        /// <summary>
        /// Initializes a new instance of the DependencyAnalyzerService class.
        /// </summary>
        public DependencyAnalyzerService()
        {
            _cycleDetector = new CycleDetectorService();
        }

        /// <summary>
        /// Initializes a new instance with a custom cycle detector (for dependency injection).
        /// </summary>
        /// <param name="cycleDetector">Custom cycle detector implementation</param>
        public DependencyAnalyzerService(ICycleDetector cycleDetector)
        {
            _cycleDetector = cycleDetector ?? throw new ArgumentNullException(nameof(cycleDetector));
        }

        /// <summary>
        /// Analyzes a list of dependencies to determine if they form a valid dependency graph.
        /// Creates a dependency graph and checks for cycles using DFS algorithm.
        /// </summary>
        /// <param name="dependencies">List of symbol dependencies</param>
        /// <returns>1 if dependencies are valid (no cycles), 0 if invalid (cycles detected)</returns>
        public int AnalyzeDependencies(List<Dependency> dependencies)
        {
            if (dependencies == null || dependencies.Count == 0)
            {
                return 1; // Empty dependencies are considered valid
            }

            // Build dependency graph: symbol -> list of symbols it depends on
            var dependencyGraph = BuildDependencyGraph(dependencies);

            // Check for cycles in the dependency graph
            bool hasCycle = _cycleDetector.HasCycle(dependencyGraph);

            return hasCycle ? 0 : 1;
        }

        /// <summary>
        /// Processes dependencies from a file and returns the sum of all line results.
        /// Reads each line, parses dependencies, analyzes them, and sums the results.
        /// </summary>
        /// <param name="filePath">Path to the input file containing dependencies</param>
        /// <returns>Sum of all dependency analysis results</returns>
        public int AnalyzeDependenciesFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Input file not found: {filePath}");
            }

            string[] lines = File.ReadAllLines(filePath);
            int totalSum = 0;

            Console.WriteLine($"Processing {lines.Length} lines from {filePath}");

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (string.IsNullOrEmpty(line))
                {
                    continue; // Skip empty lines
                }

                try
                {
                    // Parse dependencies from the current line
                    var dependencies = ParseDependencyLine(line);

                    // Analyze the dependencies for cycles
                    int result = AnalyzeDependencies(dependencies);

                    // Add to total sum
                    totalSum += result;

                    // Log the result for debugging
                    var analysisResult = new AnalysisResult(result, i + 1, result == 1 ? "Valid dependencies" : "Cycle detected");

                    #region MY TRACING TEST
                    // Console.WriteLine($"Line {i + 1:D3}: Result = {result} | {dependencies.Count} dependencies");

                    // //Show detailed info for first few lines or cycles
                    // if (i < 5 || result == 0)
                    // {
                    //     Console.WriteLine($"    Dependencies: {string.Join(", ", dependencies)}");
                    // }
                    #endregion
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing line {i + 1}: {ex.Message}");
                    // Continue processing other lines even if one fails
                }
            }

            Console.WriteLine("=".PadRight(50, '='));
            Console.WriteLine($"Total lines processed: {lines.Length}");
            Console.WriteLine($"Sum of all results: {totalSum}");

            return totalSum;
        }

        /// <summary>
        /// Parses a line of dependency text into a list of Dependency objects.
        /// Expected format: [a,b],[c,d],... where each pair represents a dependency.
        /// </summary>
        /// <param name="line">Input line containing dependencies in format [a,b],[c,d],...</param>
        /// <returns>List of parsed dependencies</returns>
        public List<Dependency> ParseDependencyLine(string line)
        {
            var dependencies = new List<Dependency>();

            if (string.IsNullOrWhiteSpace(line))
            {
                return dependencies;
            }

            // Regular expression to match dependency pairs [a,b]
            var regex = new Regex(@"\[(.),(.)\]");
            var matches = regex.Matches(line);

            foreach (Match match in matches)
            {
                if (match.Groups.Count >= 3)
                {
                    char dependent = match.Groups[1].Value[0];
                    char prerequisite = match.Groups[2].Value[0];

                    // Create dependency: dependent depends on prerequisite
                    dependencies.Add(new Dependency(dependent, prerequisite));
                }
            }

            return dependencies;
        }

        /// <summary>
        /// Builds a dependency graph from a list of dependencies.
        /// The graph is represented as a dictionary where each key is a symbol
        /// and the value is a list of symbols it depends on.
        /// </summary>
        /// <param name="dependencies">List of dependency relationships</param>
        /// <returns>Dictionary representing the dependency graph</returns>
        /// Example 1: [*,<],[<,*]
        /// Input dependencies after parsing:
        /// Dependency('*', '<')  // * depends on <
        /// Dependency('<', '*')  // < depends on *
        /// BuildDependencyGraph creates:
        /// graph['*'] = ['<']    * needs < to be pressed first
        /// graph['<'] = ['*']    < needs * to be pressed first
        /// Visual Representation For [+,~],[',+],[-,']:
        /// Dependencies created:
        /// ~  →  +  →  '  →  -
        /// ↑     ↑     ↑     ↑
        /// no    needs needs needs
        /// deps  ~     +     '
        private Dictionary<char, List<char>> BuildDependencyGraph(List<Dependency> dependencies)
        {
            // Using dictionary to have an efficient lookup instead of recursive calls
            var graph = new Dictionary<char, List<char>>();

            foreach (var dependency in dependencies)
            {
                // Add dependent symbol to graph if not exists
                // If 'a' depends on 'b', add 'b' to 'a's dependency list
                if (!graph.ContainsKey(dependency.Dependent))
                {
                    graph[dependency.Dependent] = new List<char>();
                }

                // Add prerequisite as a dependency of the dependent symbol
                // Make sure 'b' exists in graph (even if it has no dependencies)
                graph[dependency.Dependent].Add(dependency.Prerequisite);

                // Ensure prerequisite symbol exists in graph (even if it has no dependencies)
                if (!graph.ContainsKey(dependency.Prerequisite))
                {
                    graph[dependency.Prerequisite] = new List<char>();
                }
            }

            return graph;
        }
    }
}
