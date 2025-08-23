using System.Numerics;
using Quadrant.Interfaces;
using Quadrant.Models;

namespace Quadrant.Core
{
    /// <summary>
    /// Core engine that orchestrates the dial calculation process.
    /// Follows Dependency Inversion Principle - depends on abstractions, not concretions.
    /// Single Responsibility Principle - only responsible for orchestrating the workflow.
    /// </summary>
    public class QuadrantEngine
    {
        private readonly IInputReader _inputReader;
        private readonly IDialCostCalculator _calculator;
        private readonly IReporter _reporter;

        public QuadrantEngine(
            IInputReader inputReader,
            IDialCostCalculator calculator,
            IReporter reporter)
        {
            _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
            _calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
            _reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
        }

        /// <summary>
        /// Executes the main dial calculation workflow.
        /// </summary>
        /// <param name="inputSource">Source of input data (file path, connection string, etc.)</param>
        public void Run(string inputSource)
        {
            try
            {
                var dialSets = _inputReader.ReadDialSets(inputSource);
                var results = ProcessDialSets(dialSets);
                var product = CalculateProduct(results);

                _reporter.Report(dialSets.Count, results, product);
            }
            catch (Exception ex)
            {
                _reporter.ReportError(ex.Message);
            }
        }

        /// <summary>
        /// Processes all dial sets and returns calculation results.
        /// </summary>
        private List<DialSetResult> ProcessDialSets(List<DialSet> dialSets)
        {
            var results = new List<DialSetResult>();

            for (int i = 0; i < dialSets.Count; i++)
            {
                var dialSet = dialSets[i];
                var costs = _calculator.CalculateCosts(dialSet.Current, dialSet.Target);
                var sum = costs.Sum();

                var result = new DialSetResult(
                    index: i + 1,
                    current: dialSet.Current,
                    target: dialSet.Target,
                    costs: costs,
                    sum: sum);

                results.Add(result);
            }

            return results;
        }

        /// <summary>
        /// Calculates the product of all dial set sums.
        /// </summary>
        private BigInteger CalculateProduct(List<DialSetResult> results)
        {
            BigInteger product = BigInteger.One;

            foreach (var result in results)
            {
                product *= result.Sum;
            }

            return product;
        }
    }
}
