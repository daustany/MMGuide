using System.Numerics;
using Quadrant.Interfaces;
using Quadrant.Models;

namespace Quadrant.Services
{
    /// <summary>
    /// Service for reporting results to the console.
    /// Follows Single Responsibility Principle - only handles console output.
    /// Open/Closed Principle - can be extended with different output formats.
    /// </summary>
    public class ConsoleReporter : IReporter
    {
        private readonly TextWriter _output;
        private readonly TextWriter _errorOutput;

        public ConsoleReporter() : this(Console.Out, Console.Error)
        {
        }

        public ConsoleReporter(TextWriter output, TextWriter errorOutput)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
            _errorOutput = errorOutput ?? throw new ArgumentNullException(nameof(errorOutput));
        }

        public void Report(int setCount, List<DialSetResult> results, BigInteger product)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            _output.WriteLine($"Total sets: {setCount}");

            foreach (var result in results)
            {
                _output.WriteLine(result.ToString());
            }

            _output.WriteLine();
            _output.WriteLine($"Final product: {product}");
        }

        public void ReportError(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            _errorOutput.WriteLine("ERROR: " + message);
        }
    }
}
