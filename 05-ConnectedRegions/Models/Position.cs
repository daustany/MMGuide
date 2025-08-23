namespace ConnectedRegions.Models;

/// <summary>
/// Represents a position in a 2D grid with row and column coordinates
/// </summary>
/// <param name="Row">The row coordinate (0-based)</param>
/// <param name="Column">The column coordinate (0-based)</param>
public record Position(int Row, int Column)
{
    /// <summary>
    /// Gets all adjacent positions (horizontally, vertically, and diagonally)
    /// </summary>
    /// <returns>An enumerable of all 8 adjacent positions</returns>
    public IEnumerable<Position> GetAdjacentPositions()
    {
        // Check all 8 directions: horizontal, vertical, and diagonal
        for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
        {
            for (int colOffset = -1; colOffset <= 1; colOffset++)
            {
                // Skip the current position itself
                if (rowOffset == 0 && colOffset == 0)
                    continue;

                yield return new Position(Row + rowOffset, Column + colOffset);
            }
        }
    }
}
