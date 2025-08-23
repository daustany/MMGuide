using Quadrant.Models;

namespace Quadrant.Interfaces
{
    /// <summary>
    /// Interface for input validation.
    /// Follows Single Responsibility Principle - only responsible for validation logic.
    /// </summary>
    public interface IInputValidator
    {
        /// <summary>
        /// Validates a dial line format.
        /// </summary>
        /// <param name="line">Line to validate</param>
        /// <param name="lineNumber">Line number for error reporting</param>
        /// <returns>True if valid, false otherwise</returns>
        bool ValidateDialLine(string line, int lineNumber);

        /// <summary>
        /// Validates the overall structure of input data.
        /// </summary>
        /// <param name="lines">All input lines</param>
        /// <returns>True if valid structure, false otherwise</returns>
        bool ValidateInputStructure(IEnumerable<string> lines);
    }
}
