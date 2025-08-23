using StoneSplitting.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace StoneSplitting;

/// <summary>
/// Main program entry point for the Stone Splitting application.
/// Reads input data and calculates maximum splits for each pile of stones.
/// </summary>
public class Program
{
    public static async Task Main(string[] args)
    {
        // Build host with dependency injection
        var host = CreateHost();

        // Get the calculator service from DI container
        var calculator = host.Services.GetRequiredService<IStoneSplittingCalculator>();

        try
        {
            // Use the first argument as input file path, or default to "Input/input.txt"
            string inputFile = args.Length > 0 ? args[0] : "Input/input.txt";

            // Calculate and display result
            var result = await calculator.CalculateMaximumSplitsAsync(inputFile);

            Console.WriteLine($"Final Result: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
    }

    /// <summary>
    /// Creates and configures the host with dependency injection.
    /// </summary>
    private static IHost CreateHost()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddScoped<IInputParser, InputParser>();
                services.AddScoped<ISplitCalculator, SplitCalculator>();
                services.AddScoped<IStoneSplittingCalculator, StoneSplittingCalculator>();
            })
            .Build();
    }
}