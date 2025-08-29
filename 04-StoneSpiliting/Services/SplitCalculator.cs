using StoneSplitting.Models;

namespace StoneSplitting.Services;

/// <summary>
/// Service responsible for calculating the maximum number of splits for a stone pile.
/// Implements the EXACT algorithm as described in the problem statement.
/// </summary>
public class SplitCalculator : ISplitCalculator
{
    /// <summary>
    /// Calculates the maximum number of splits possible for a given pile.
    /// </summary>
    public SplitResult CalculateMaxSplits(StonePile pile)
    {
        if (pile?.SplittingNumbers == null || !pile.SplittingNumbers.Any())
        {
            return new SplitResult(0, pile!);
        }

        var memo = new Dictionary<long, int>();
        var maxSplits = CalculateMaxSplitsRecursive(pile.InitialSize, pile.SplittingNumbers, memo);

        return new SplitResult(maxSplits, pile);
    }

    /// <summary>
    /// Recursive function implementing the exact algorithm:
    /// 1. Try each divisor x where x != n and n is divisible by x
    /// 2. Split into n/x piles of size x
    /// 3. Recursively split each resulting pile
    /// 4. Return maximum splits across all choices
    /// </summary>
    private int CalculateMaxSplitsRecursive(long pileSize, IReadOnlyList<int> splittingNumbers, Dictionary<long, int> memo)
    {
        // Base case: piles of size 1 or less cannot be split
        if (pileSize <= 1)
            return 0;

        // Check memoization cache
        if (memo.TryGetValue(pileSize, out int cachedResult))
            return cachedResult;

        int maxSplits = 0;

        // Try each number x in the set where x != n and n is divisible by x
        foreach (int x in splittingNumbers)
        {
            // x must be a valid divisor: x != pileSize and pileSize % x == 0
            if (x <= 0 || x >= pileSize || pileSize % x != 0)
                continue;

            // Split into pileSize/x piles of size x
            long numberOfPiles = pileSize / x;
            long sizeOfEachPile = x;

            // This gives us 1 split operation
            int splitsFromThisDivision = 1;

            // Each resulting pile can be further split recursively
            int splitsPerPile = CalculateMaxSplitsRecursive(sizeOfEachPile, splittingNumbers, memo);

            // Total splits = 1 (this division) + (numberOfPiles * splitsPerPile)
            // We need to handle the case where this calculation might be very large
            long totalSplitsLong = splitsFromThisDivision + (numberOfPiles * splitsPerPile);

            // Convert to int, capping at int.MaxValue if necessary
            int totalSplits = totalSplitsLong > int.MaxValue ? int.MaxValue : (int)totalSplitsLong;

            maxSplits = Math.Max(maxSplits, totalSplits);
        }

        // Memoize and return result
        memo[pileSize] = maxSplits;
        return maxSplits;
    }
}