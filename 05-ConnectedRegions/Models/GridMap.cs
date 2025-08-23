namespace ConnectedRegions.Models;

/// <summary>
/// Represents a 2D grid map containing binary values (0 or 1)
/// </summary>
public class GridMap
{
    private readonly int[,] _grid;

    /// <summary>
    /// Gets the number of rows in the grid
    /// </summary>
    public int Rows { get; }

    /// <summary>
    /// Gets the number of columns in the grid
    /// </summary>
    public int Columns { get; }

    /// <summary>
    /// Initializes a new instance of the GridMap class
    /// </summary>
    /// <param name="grid">2D array representing the grid</param>
    /// <exception cref="ArgumentNullException">Thrown when grid is null</exception>
    /// <exception cref="ArgumentException">Thrown when grid is empty</exception>
    public GridMap(int[,] grid)
    {
        _grid = grid ?? throw new ArgumentNullException(nameof(grid));

        Rows = _grid.GetLength(0);
        Columns = _grid.GetLength(1);

        if (Rows == 0 || Columns == 0)
            throw new ArgumentException("Grid cannot be empty", nameof(grid));
    }

    /// <summary>
    /// Gets the value at the specified position
    /// </summary>
    /// <param name="position">The position to get the value from</param>
    /// <returns>The value at the specified position</returns>
    public int GetValue(Position position) => _grid[position.Row, position.Column];

    /// <summary>
    /// Checks if the specified position is within the grid bounds
    /// </summary>
    /// <param name="position">The position to validate</param>
    /// <returns>True if the position is valid, false otherwise</returns>
    public bool IsValidPosition(Position position) =>
        position.Row >= 0 && position.Row < Rows &&
        position.Column >= 0 && position.Column < Columns;

    /// <summary>
    /// Checks if the cell at the specified position is part of a connected region (value = 1)
    /// </summary>
    /// <param name="position">The position to check</param>
    /// <returns>True if the cell is part of a connected region, false otherwise</returns>
    public bool IsConnectedCell(Position position) => IsValidPosition(position) && GetValue(position) == 1;
}
