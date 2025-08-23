namespace SmoothLanding.Models;

/// <summary>
/// Represents the detailed processing information for a single sequence.
/// Encapsulates all data related to processing one array.
/// </summary>
public class SequenceProcessingDetail
{
    public int[] OriginalSequence { get; set; } = Array.Empty<int>();
    public int[] CountsArray { get; set; } = Array.Empty<int>();
    public int Median { get; set; }
}

/// <summary>
/// Represents the complete result of the puzzle solving process.
/// Contains the final sum and detailed processing information for all sequences.
/// </summary>
public class PuzzleResult
{
    public int FinalSum { get; set; }
    public List<SequenceProcessingDetail> ProcessingDetails { get; set; } = new();
}
