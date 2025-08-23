using StoneSplitting.Models;

namespace StoneSplitting.Services;

/// <summary>
/// Service responsible for calculating the maximum number of splits for a stone pile.
/// Implements the Single Responsibility Principle by focusing only on split calculations.
/// </summary>
public class SplitCalculator : ISplitCalculator
{
    /// <summary>
    /// Calculates the maximum number of splits possible for a given pile.
    /// </summary>
    /// <param name="pile">The stone pile to analyze</param>
    /// <returns>SplitResult containing the maximum splits and original pile</returns>
    public SplitResult CalculateMaxSplits(StonePile pile)
    {
        if (pile?.SplittingNumbers == null || !pile.SplittingNumbers.Any())
        {
            return new SplitResult(0, pile!);
        }

        var maxSplits = CalculateMaxSplitsForSize(pile.InitialSize, pile.SplittingNumbers);
        return new SplitResult(maxSplits, pile);
    }

    /// <summary>
    /// Calculate maximum splits for a given pile size using memoization.
    /// </summary>
    private int CalculateMaxSplitsForSize(long pileSize, IReadOnlyList<int> splittingNumbers)
    {
        var memo = new Dictionary<long, int>();
        return CalculateMaxSplitsRecursive(pileSize, splittingNumbers, memo);
    }

    /// <summary>
    /// Recursive method with memoization and optimized base cases.
    /// </summary>
    private int CalculateMaxSplitsRecursive(long pileSize, IReadOnlyList<int> splittingNumbers, Dictionary<long, int> memo)
    {
        // Base case: piles of size 1 or less cannot be split
        if (pileSize <= 1)
            return 0;

        // Check memoization
        if (memo.TryGetValue(pileSize, out int cached))
            return cached;

        // Try to limit the recursion depth for very large numbers
        if (pileSize > 1000000000L) // For very large numbers, use heuristic
        {
            memo[pileSize] = CalculateHeuristicSplits(pileSize, splittingNumbers);
            return memo[pileSize];
        }

        int maxSplits = 0;

        // Try each splitting number
        foreach (int splittingNumber in splittingNumbers)
        {
            // Skip if splitting number is invalid
            if (splittingNumber <= 1 || splittingNumber >= pileSize)
                continue;

            // Check if pile can be evenly divided
            if (pileSize % splittingNumber != 0)
                continue;

            // When we divide pile by splittingNumber, we get pileSize/splittingNumber piles, 
            // each of size splittingNumber
            long numberOfNewPiles = pileSize / splittingNumber;
            long newPileSize = splittingNumber;

            // Calculate splits: 1 for this split + splits from recursively splitting the new piles
            int splitsFromThisDivision = 1;
            int splitsFromOneNewPile = CalculateMaxSplitsRecursive(newPileSize, splittingNumbers, memo);

            // Since we create 'numberOfNewPiles' piles of size 'newPileSize', 
            // and each can be further split independently
            int totalSplits = splitsFromThisDivision + ((int)numberOfNewPiles * splitsFromOneNewPile);

            maxSplits = Math.Max(maxSplits, totalSplits);
        }

        memo[pileSize] = maxSplits;
        return maxSplits;
    }

    /// <summary>
    /// Heuristic calculation for very large numbers to avoid deep recursion.
    /// </summary>
    private int CalculateHeuristicSplits(long pileSize, IReadOnlyList<int> splittingNumbers)
    {
        // For very large numbers, estimate based on prime factorization and available splitting numbers
        int splits = 0;
        long currentSize = pileSize;

        // Try to use the most effective splitting numbers
        var validDivisors = splittingNumbers
            .Where(x => x > 1 && x < pileSize && pileSize % x == 0)
            .OrderBy(x => x)
            .ToList();

        if (!validDivisors.Any())
            return 0;

        // Use the smallest valid divisor repeatedly
        int smallestDivisor = validDivisors.First();

        while (currentSize > 1 && currentSize % smallestDivisor == 0 && splits < 50) // Limit to prevent infinite loops
        {
            splits++;
            currentSize /= smallestDivisor;
        }

        return splits;
    }
}