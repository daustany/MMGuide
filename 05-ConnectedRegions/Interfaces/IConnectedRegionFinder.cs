using ConnectedRegions.Models;

namespace ConnectedRegions.Interfaces;

/// <summary>
/// Interface for services that can find connected regions in a grid
/// </summary>
public interface IConnectedRegionFinder
{
    /// <summary>
    /// Finds the size of the largest connected region in the given grid map
    /// </summary>
    /// <param name="gridMap">The grid map to analyze</param>
    /// <returns>The size of the largest connected region</returns>
    int FindLargestConnectedRegionSize(GridMap gridMap);

    /// <summary>
    /// Finds all connected regions in the given grid map
    /// </summary>
    /// <param name="gridMap">The grid map to analyze</param>
    /// <returns>A list of region sizes</returns>
    IEnumerable<int> FindAllConnectedRegionSizes(GridMap gridMap);
}
