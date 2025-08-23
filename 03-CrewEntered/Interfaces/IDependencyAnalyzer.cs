using CrewEntered.Models;
using System.Collections.Generic;

namespace CrewEntered.Interfaces
{
    /// <summary>
    /// Interface for dependency analysis operations.
    /// Provides methods to analyze symbol dependencies and detect cycles.
    /// </summary>
    public interface IDependencyAnalyzer
    {
        /// <summary>
        /// Analyzes a list of dependencies to determine if they form a valid dependency graph.
        /// </summary>
        /// <param name="dependencies">List of symbol dependencies</param>
        /// <returns>1 if dependencies are valid (no cycles), 0 if invalid (cycles detected)</returns>
        int AnalyzeDependencies(List<Dependency> dependencies);

        /// <summary>
        /// Processes dependencies from a file and returns the sum of all line results.
        /// </summary>
        /// <param name="filePath">Path to the input file containing dependencies</param>
        /// <returns>Sum of all dependency analysis results</returns>
        int AnalyzeDependenciesFromFile(string filePath);

        /// <summary>
        /// Parses a line of dependency text into a list of Dependency objects.
        /// </summary>
        /// <param name="line">Input line containing dependencies in format [a,b],[c,d],...</param>
        /// <returns>List of parsed dependencies</returns>
        List<Dependency> ParseDependencyLine(string line);
    }
}
