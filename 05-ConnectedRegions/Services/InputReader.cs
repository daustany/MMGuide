using ConnectedRegions.Interfaces;
using ConnectedRegions.Models;

namespace ConnectedRegions.Services;

/// <summary>
/// Service for reading and parsing input files containing grid data
/// </summary>
public class InputReader : IInputReader
{
    /// <summary>
    /// Reads a grid map from the specified file path
    /// </summary>
    /// <param name="filePath">The path to the input file</param>
    /// <returns>A GridMap object representing the parsed grid</returns>
    /// <exception cref="FileNotFoundException">Thrown when the file is not found</exception>
    /// <exception cref="FormatException">Thrown when the file format is invalid</exception>
    public async Task<GridMap> ReadGridMapAsync(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Input file not found: {filePath}");

        try
        {
            var lines = await File.ReadAllLinesAsync(filePath);

            // Filter out empty lines and trim whitespace
            var gridLines = lines
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Trim())
                .ToList();

            if (gridLines.Count == 0)
                throw new FormatException("Input file contains no valid grid data");

            // Parse the first line to determine the number of columns
            var firstRowValues = ParseRow(gridLines[0]);
            int rows = gridLines.Count;
            int columns = firstRowValues.Length;

            // Create the 2D grid array
            var grid = new int[rows, columns];

            // Parse each row and populate the grid
            for (int row = 0; row < rows; row++)
            {
                var rowValues = ParseRow(gridLines[row]);

                if (rowValues.Length != columns)
                    throw new FormatException($"Row {row + 1} has {rowValues.Length} columns, expected {columns}");

                for (int col = 0; col < columns; col++)
                {
                    grid[row, col] = rowValues[col];
                }
            }

            return new GridMap(grid);
        }
        catch (Exception ex) when (!(ex is FileNotFoundException || ex is FormatException))
        {
            throw new FormatException($"Error parsing input file: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Parses a single row of space-separated integers
    /// </summary>
    /// <param name="line">The line to parse</param>
    /// <returns>Array of integers from the line</returns>
    /// <exception cref="FormatException">Thrown when the line format is invalid</exception>
    private static int[] ParseRow(string line)
    {
        try
        {
            return line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                      .Select(int.Parse)
                      .ToArray();
        }
        catch (Exception ex)
        {
            throw new FormatException($"Invalid row format: '{line}'. Expected space-separated integers.", ex);
        }
    }
}
