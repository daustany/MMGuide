
using Quadrant.Core;

namespace Quadrant
{
    /// <summary>
    /// Main program entry point.
    /// Demonstrates the use of the SOLID architecture with proper dependency injection.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Use the factory to create a fully configured engine
            // This demonstrates Dependency Inversion Principle - the main method
            // doesn't need to know about concrete implementations
            var engine = ServiceFactory.CreateEngine();

            // Default input path
            var inputPath = Path.Combine("Input", "input.txt");

            // Run the engine
            engine.Run(inputPath);
        }
    }
}
