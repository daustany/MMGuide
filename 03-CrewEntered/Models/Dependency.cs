namespace CrewEntered.Models
{
    /// <summary>
    /// Represents a dependency relationship between two symbols.
    /// In the context of this problem, 'Prerequisite' must be activated before 'Dependent'.
    /// </summary>
    public class Dependency
    {
        /// <summary>
        /// The symbol that depends on another symbol (must be pressed after Prerequisite).
        /// </summary>
        public char Dependent { get; set; }

        /// <summary>
        /// The symbol that must be pressed before the Dependent symbol.
        /// </summary>
        public char Prerequisite { get; set; }

        /// <summary>
        /// Initializes a new instance of the Dependency class.
        /// </summary>
        /// <param name="dependent">The dependent symbol</param>
        /// <param name="prerequisite">The prerequisite symbol</param>
        public Dependency(char dependent, char prerequisite)
        {
            Dependent = dependent;
            Prerequisite = prerequisite;
        }

        /// <summary>
        /// Returns a string representation of the dependency.
        /// </summary>
        /// <returns>String in format "dependent depends on prerequisite"</returns>
        public override string ToString()
        {
            return $"'{Dependent}' depends on '{Prerequisite}'";
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current dependency.
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>True if objects are equal, false otherwise</returns>
        public override bool Equals(object? obj)
        {
            if (obj is Dependency other)
            {
                return Dependent == other.Dependent && Prerequisite == other.Prerequisite;
            }
            return false;
        }

        /// <summary>
        /// Returns a hash code for the dependency.
        /// </summary>
        /// <returns>Hash code based on dependent and prerequisite symbols</returns>
        public override int GetHashCode()
        {
            return (Dependent, Prerequisite).GetHashCode();
        }
    }
}
