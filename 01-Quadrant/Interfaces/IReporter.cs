using System.Numerics;
using Quadrant.Models;

namespace Quadrant.Interfaces
{
    /// <summary>
    /// Interface for reporting results and errors.
    /// Follows Single Responsibility Principle - only responsible for output/reporting.
    /// </summary>
    public interface IReporter
    {
        /// <summary>
        /// Reports the calculation results.
        /// </summary>
        /// <param name="setCount">Total number of dial sets processed</param>
        /// <param name="results">List of calculation results</param>
        /// <param name="product">Final product of all sums</param>
        void Report(int setCount, List<DialSetResult> results, BigInteger product);

        /// <summary>
        /// Reports an error message.
        /// </summary>
        /// <param name="message">Error message to report</param>
        void ReportError(string message);
    }
}
