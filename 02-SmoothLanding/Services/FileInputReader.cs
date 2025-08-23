using System.Text.RegularExpressions;
using SmoothLanding.Interfaces;

namespace SmoothLanding.Services;

/// <summary>
/// Implementation of IInputReader that reads sequences from text files.
/// Handles parsing of array-like strings into integer arrays.
/// </summary>
public class FileInputReader : IInputReader
{
    /// <summary>
    /// Reads sequences from a file where each line contains an array in the format [1,2,3,4].
    /// </summary>
    /// <param name="filePath">Path to the input file</param>
    /// <returns>Collection of integer sequences</returns>
    /// <exception cref="FileNotFoundException">Thrown when the input file doesn't exist</exception>
    /// <exception cref="InvalidDataException">Thrown when file format is invalid</exception>
    public async Task<IEnumerable<int[]>> ReadSequencesAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Input file not found: {filePath}");
        }

        var sequences = new List<int[]>();
        var lines = await File.ReadAllLinesAsync(filePath);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            try
            {
                var sequence = ParseSequenceFromLine(line.Trim());
                sequences.Add(sequence);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"Failed to parse line: '{line}'. Error: {ex.Message}", ex);
            }
        }

        return sequences;
    }

    /// <summary>
    /// Parses a line containing an array representation like [1,2,3,4] into an integer array.
    /// </summary>
    /// <param name="line">Line containing the array</param>
    /// <returns>Parsed integer array</returns>
    private static int[] ParseSequenceFromLine(string line)
    {
        // Remove brackets and split by comma
        var cleanLine = line.Trim('[', ']');

        if (string.IsNullOrWhiteSpace(cleanLine))
        {
            return Array.Empty<int>();
        }

        var numberStrings = cleanLine.Split(',', StringSplitOptions.RemoveEmptyEntries);
        var numbers = new int[numberStrings.Length];

        for (int i = 0; i < numberStrings.Length; i++)
        {
            if (!int.TryParse(numberStrings[i].Trim(), out numbers[i]))
            {
                throw new FormatException($"Invalid number format: '{numberStrings[i].Trim()}'");
            }
        }

        return numbers;
    }
}
