using Quadrant.Models;

namespace Quadrant.Interfaces
{
    /// <summary>
    /// Interface for parsing dial data.
    /// Follows Single Responsibility Principle - only responsible for parsing logic.
    /// </summary>
    public interface IDialParser
    {
        /// <summary>
        /// Parses a dial line into an array of integers.
        /// </summary>
        /// <param name="line">Line to parse</param>
        /// <param name="lineNumber">Line number for error reporting</param>
        /// <returns>Array of dial positions</returns>
        int[] ParseDialLine(string line, int lineNumber);

        /// <summary>
        /// Parses multiple lines into dial sets.
        /// </summary>
        /// <param name="lines">Lines to parse</param>
        /// <returns>List of dial sets</returns>
        List<DialSet> ParseDialSets(IEnumerable<string> lines);
    }
}
