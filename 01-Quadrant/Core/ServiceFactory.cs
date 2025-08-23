using Quadrant.Interfaces;
using Quadrant.Services;
using Quadrant.Models;

namespace Quadrant.Core
{
    /// <summary>
    /// Factory for creating service instances with proper dependency injection.
    /// Follows Dependency Inversion Principle and makes it easy to configure different implementations.
    /// </summary>
    public static class ServiceFactory
    {
        /// <summary>
        /// Creates a file-based input reader with all required dependencies.
        /// </summary>
        public static IInputReader CreateFileInputReader(DialConfiguration? configuration = null)
        {
            configuration ??= DialConfiguration.Default;

            var validator = new InputValidator(configuration);
            var parser = new DialParser(configuration, validator);

            return new FileInputReader(validator, parser);
        }

        /// <summary>
        /// Creates a dial cost calculator with the specified configuration.
        /// </summary>
        public static IDialCostCalculator CreateDialCostCalculator(DialConfiguration? configuration = null)
        {
            configuration ??= DialConfiguration.Default;
            return new DialCostCalculator(configuration);
        }

        /// <summary>
        /// Creates a console reporter.
        /// </summary>
        public static IReporter CreateConsoleReporter()
        {
            return new ConsoleReporter();
        }

        /// <summary>
        /// Creates a fully configured QuadrantEngine with default dependencies.
        /// </summary>
        public static QuadrantEngine CreateEngine(DialConfiguration? configuration = null)
        {
            var inputReader = CreateFileInputReader(configuration);
            var calculator = CreateDialCostCalculator(configuration);
            var reporter = CreateConsoleReporter();

            return new QuadrantEngine(inputReader, calculator, reporter);
        }
    }
}
