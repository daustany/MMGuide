using ConnectedRegions.Interfaces;
using ConnectedRegions.Models;

namespace ConnectedRegions.Services;

/// <summary>
/// Service for finding connected regions in a grid using depth-first search (DFS)
/// </summary>
public class ConnectedRegionFinder : IConnectedRegionFinder
{
    /// <summary>
    /// Finds the size of the largest connected region in the given grid map
    /// </summary>
    /// <param name="gridMap">The grid map to analyze</param>
    /// <returns>The size of the largest connected region</returns>
    /// <exception cref="ArgumentNullException">Thrown when gridMap is null</exception>
    public int FindLargestConnectedRegionSize(GridMap gridMap)
    {
        ArgumentNullException.ThrowIfNull(gridMap);

        var regionSizes = FindAllConnectedRegionSizes(gridMap);
        return regionSizes.DefaultIfEmpty(0).Max();
    }

    /// <summary>
    /// Finds all connected regions in the given grid map
    /// </summary>
    /// <param name="gridMap">The grid map to analyze</param>
    /// <returns>A list of region sizes</returns>
    /// <exception cref="ArgumentNullException">Thrown when gridMap is null</exception>
    public IEnumerable<int> FindAllConnectedRegionSizes(GridMap gridMap)
    {
        ArgumentNullException.ThrowIfNull(gridMap);

        var visited = new bool[gridMap.Rows, gridMap.Columns];
        var regionSizes = new List<int>();

        // Traverse each cell in the grid
        for (int row = 0; row < gridMap.Rows; row++)
        {
            for (int col = 0; col < gridMap.Columns; col++)
            {
                var position = new Position(row, col);

                // If the cell is part of a connected region and hasn't been visited yet
                if (gridMap.IsConnectedCell(position) && !visited[row, col])
                {
                    // Start DFS to find the entire connected region
                    int regionSize = ExploreConnectedRegion(gridMap, position, visited);
                    regionSizes.Add(regionSize);
                }
            }
        }

        return regionSizes;
    }

    /// <summary>
    /// Explores a connected region using depth-first search (DFS) starting from the given position
    /// </summary>
    /// <param name="gridMap">The grid map to explore</param>
    /// <param name="startPosition">The starting position for the exploration</param>
    /// <param name="visited">2D array tracking visited cells</param>
    /// <returns>The size of the connected region</returns>
    private static int ExploreConnectedRegion(GridMap gridMap, Position startPosition, bool[,] visited)
    {
        var stack = new Stack<Position>();
        stack.Push(startPosition);
        int regionSize = 0;

        // Use iterative DFS to avoid stack overflow for large regions
        while (stack.Count > 0)
        {
            var currentPosition = stack.Pop();

            // Skip if already visited or out of bounds
            if (!gridMap.IsValidPosition(currentPosition) ||
                visited[currentPosition.Row, currentPosition.Column])
                continue;

            // Skip if not part of a connected region
            if (!gridMap.IsConnectedCell(currentPosition))
                continue;

            // Mark as visited and increment region size
            visited[currentPosition.Row, currentPosition.Column] = true;
            regionSize++;

            // Add all adjacent positions to the stack for exploration
            foreach (var adjacentPosition in currentPosition.GetAdjacentPositions())
            {
                if (gridMap.IsValidPosition(adjacentPosition) &&
                    !visited[adjacentPosition.Row, adjacentPosition.Column] &&
                    gridMap.IsConnectedCell(adjacentPosition))
                {
                    stack.Push(adjacentPosition);
                }
            }
        }

        return regionSize;
    }
}
