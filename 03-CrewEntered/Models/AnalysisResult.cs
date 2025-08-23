namespace CrewEntered.Models
{
    /// <summary>
    /// Represents the result of analyzing a line of dependencies.
    /// Contains information about whether the dependencies are valid and any additional context.
    /// </summary>
    public class AnalysisResult
    {
        /// <summary>
        /// The result value: 1 if dependencies are valid (no cycles), 0 if invalid (cycles detected).
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// The line number being analyzed (for debugging and logging purposes).
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// Optional message describing the analysis result or any issues found.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Indicates whether the dependency analysis was successful (no cycles found).
        /// </summary>
        public bool IsValid => Value == 1;

        /// <summary>
        /// Initializes a new instance of the AnalysisResult class.
        /// </summary>
        /// <param name="value">The result value (0 or 1)</param>
        /// <param name="lineNumber">The line number being analyzed</param>
        /// <param name="message">Optional description message</param>
        public AnalysisResult(int value, int lineNumber, string message = "")
        {
            Value = value;
            LineNumber = lineNumber;
            Message = message ?? string.Empty;
        }

        /// <summary>
        /// Returns a string representation of the analysis result.
        /// </summary>
        /// <returns>String describing the result</returns>
        public override string ToString()
        {
            var status = IsValid ? "Valid" : "Invalid (Cycle Detected)";
            return $"Line {LineNumber}: {status} - Result: {Value}" +
                   (string.IsNullOrEmpty(Message) ? "" : $" ({Message})");
        }
    }
}
