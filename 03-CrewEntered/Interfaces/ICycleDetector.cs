using System.Collections.Generic;

namespace CrewEntered.Interfaces
{
    /// <summary>
    /// Interface for cycle detection algorithms in directed graphs.
    /// Provides methods to detect circular dependencies in symbol dependency graphs.
    /// </summary>
    public interface ICycleDetector
    {
        /// <summary>
        /// Detects if there are any cycles in the given dependency graph.
        /// </summary>
        /// <param name="dependencyGraph">Dictionary representing the dependency graph where 
        /// key is a symbol and value is a list of symbols it depends on</param>
        /// <returns>True if cycles are detected, False if no cycles exist</returns>
        bool HasCycle(Dictionary<char, List<char>> dependencyGraph);

        /// <summary>
        /// Performs depth-first search to detect cycles starting from a specific node.
        /// </summary>
        /// <param name="node">Starting node for cycle detection</param>
        /// <param name="dependencyGraph">The dependency graph</param>
        /// <param name="visited">Set of visited nodes</param>
        /// <param name="recursionStack">Set of nodes in current recursion stack</param>
        /// <returns>True if cycle is detected, False otherwise</returns>
        bool DetectCycleDFS(char node, Dictionary<char, List<char>> dependencyGraph,
                           HashSet<char> visited, HashSet<char> recursionStack);
    }
}
