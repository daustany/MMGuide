namespace StoneSplitting.Models;

/// <summary>
/// Represents a pile of stones with its initial size and available splitting numbers.
/// </summary>
public record StonePile
{
    /// <summary>
    /// The initial number of stones in the pile.
    /// </summary>
    public long InitialSize { get; init; }

    /// <summary>
    /// The set of distinct integers that can be used for splitting.
    /// </summary>
    public IReadOnlyList<int> SplittingNumbers { get; init; } = Array.Empty<int>();

    /// <summary>
    /// Creates a new instance of StonePile.
    /// </summary>
    /// <param name="initialSize">The initial size of the pile</param>
    /// <param name="splittingNumbers">The available numbers for splitting</param>
    public StonePile(long initialSize, IEnumerable<int> splittingNumbers)
    {
        InitialSize = initialSize;
        SplittingNumbers = splittingNumbers.ToList().AsReadOnly();
    }
}

/// <summary>
/// Represents the result of split calculation for a single pile.
/// </summary>
public record SplitResult
{
    /// <summary>
    /// The maximum number of splits achieved for this pile.
    /// </summary>
    public int MaxSplits { get; init; }

    /// <summary>
    /// The original pile that was split.
    /// </summary>
    public StonePile OriginalPile { get; init; }

    /// <summary>
    /// Creates a new instance of SplitResult.
    /// </summary>
    /// <param name="maxSplits">The maximum splits achieved</param>
    /// <param name="originalPile">The original pile</param>
    public SplitResult(int maxSplits, StonePile originalPile)
    {
        MaxSplits = maxSplits;
        OriginalPile = originalPile;
    }
}
