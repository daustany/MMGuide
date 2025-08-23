using System.Text.RegularExpressions;
using StoneSplitting.Models;

namespace StoneSplitting.Services;

/// <summary>
/// Service responsible for parsing input data from files.
/// Implements the Single Responsibility Principle by focusing only on input parsing.
/// </summary>
public class InputParser : IInputParser
{
    private static readonly Regex InputLineRegex = new(
        @"^(\d+)\s*\[([^\]]+)\]$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    /// <summary>
    /// Parses the input file and converts each line into a StonePile object.
    /// Expected format: "number [comma,separated,values]"
    /// </summary>
    /// <param name="filePath">Path to the input file</param>
    /// <returns>Collection of parsed stone piles</returns>
    /// <exception cref="FileNotFoundException">Thrown when input file doesn't exist</exception>
    /// <exception cref="InvalidOperationException">Thrown when input format is invalid</exception>
    public async Task<IEnumerable<StonePile>> ParseInputAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Input file not found: {filePath}");
        }

        var lines = await File.ReadAllLinesAsync(filePath);
        var stonePiles = new List<StonePile>();

        for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
        {
            var line = lines[lineNumber].Trim();

            // Skip empty lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            try
            {
                var stonePile = ParseLine(line);
                stonePiles.Add(stonePile);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Error parsing line {lineNumber + 1}: '{line}'. {ex.Message}", ex);
            }
        }

        return stonePiles;
    }

    /// <summary>
    /// Parses a single line into a StonePile object.
    /// </summary>
    /// <param name="line">The input line to parse</param>
    /// <returns>A StonePile object</returns>
    private static StonePile ParseLine(string line)
    {
        var match = InputLineRegex.Match(line);

        if (!match.Success)
        {
            throw new FormatException("Invalid line format. Expected: 'number [x,y,z,...]'");
        }

        // Parse the initial pile size
        if (!long.TryParse(match.Groups[1].Value, out long initialSize))
        {
            throw new FormatException($"Invalid pile size: {match.Groups[1].Value}");
        }

        // Parse the splitting numbers
        var numbersString = match.Groups[2].Value;
        var splittingNumbers = ParseSplittingNumbers(numbersString);

        return new StonePile(initialSize, splittingNumbers);
    }

    /// <summary>
    /// Parses comma-separated numbers from the brackets.
    /// </summary>
    /// <param name="numbersString">Comma-separated string of numbers</param>
    /// <returns>List of parsed integers</returns>
    private static List<int> ParseSplittingNumbers(string numbersString)
    {
        var numbers = new List<int>();
        var parts = numbersString.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var part in parts)
        {
            var trimmedPart = part.Trim();

            if (!int.TryParse(trimmedPart, out int number))
            {
                throw new FormatException($"Invalid splitting number: {trimmedPart}");
            }

            if (number <= 0)
            {
                throw new FormatException($"Splitting numbers must be positive: {number}");
            }

            numbers.Add(number);
        }

        return numbers;
    }
}
