using ConnectedRegions.Models;

namespace ConnectedRegions.Interfaces;

/// <summary>
/// Interface for services that can read and parse input files
/// </summary>
public interface IInputReader
{
    /// <summary>
    /// Reads a grid map from the specified file path
    /// </summary>
    /// <param name="filePath">The path to the input file</param>
    /// <returns>A GridMap object representing the parsed grid</returns>
    /// <exception cref="FileNotFoundException">Thrown when the file is not found</exception>
    /// <exception cref="FormatException">Thrown when the file format is invalid</exception>
    Task<GridMap> ReadGridMapAsync(string filePath);
}
