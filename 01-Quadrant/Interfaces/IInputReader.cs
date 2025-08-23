using Quadrant.Models;

namespace Quadrant.Interfaces
{
    /// <summary>
    /// Interface for reading dial sets from various input sources.
    /// Follows Single Responsibility Principle - only responsible for reading input.
    /// </summary>
    public interface IInputReader
    {
        /// <summary>
        /// Reads dial sets from the specified source.
        /// </summary>
        /// <param name="source">The source to read from (file path, connection string, etc.)</param>
        /// <returns>List of dial sets</returns>
        List<DialSet> ReadDialSets(string source);
    }
}
