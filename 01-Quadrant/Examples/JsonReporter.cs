using System.Numerics;
using System.Text.Json;
using Quadrant.Interfaces;
using Quadrant.Models;

namespace Quadrant.Examples
{
    /// <summary>
    /// Example implementation of IReporter that outputs results in JSON format.
    /// Demonstrates how the system can be extended following the Open/Closed Principle.
    /// </summary>
    public class JsonReporter : IReporter
    {
        private readonly string _outputPath;
        private readonly JsonSerializerOptions _jsonOptions;

        public JsonReporter(string outputPath)
        {
            _outputPath = outputPath ?? throw new ArgumentNullException(nameof(outputPath));
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public void Report(int setCount, List<DialSetResult> results, BigInteger product)
        {
            var report = new
            {
                Timestamp = DateTime.UtcNow,
                TotalSets = setCount,
                Results = results.Select(r => new
                {
                    r.Index,
                    Current = r.CurrentStr,
                    Target = r.TargetStr,
                    Costs = r.Costs,
                    CostsString = r.CostsStr,
                    r.Sum
                }).ToArray(),
                FinalProduct = product.ToString()
            };

            var json = JsonSerializer.Serialize(report, _jsonOptions);
            File.WriteAllText(_outputPath, json);

            Console.WriteLine($"Results saved to: {_outputPath}");
        }

        public void ReportError(string message)
        {
            var error = new
            {
                Timestamp = DateTime.UtcNow,
                Level = "Error",
                Message = message
            };

            var json = JsonSerializer.Serialize(error, _jsonOptions);
            var errorPath = Path.ChangeExtension(_outputPath, ".error.json");
            File.WriteAllText(errorPath, json);

            Console.Error.WriteLine($"ERROR: {message}");
            Console.Error.WriteLine($"Error details saved to: {errorPath}");
        }
    }

    /// <summary>
    /// Example program showing how to use the JsonReporter extension.
    /// </summary>
    public class JsonReporterExample
    {
        public static void RunExample()
        {
            // Create services manually to use custom reporter
            var inputReader = Core.ServiceFactory.CreateFileInputReader();
            var calculator = Core.ServiceFactory.CreateDialCostCalculator();
            var reporter = new JsonReporter("output.json");

            // Create engine with custom reporter
            var engine = new Core.QuadrantEngine(inputReader, calculator, reporter);

            // Run with input file
            var inputPath = Path.Combine("Input", "input.txt");
            engine.Run(inputPath);
        }
    }
}
