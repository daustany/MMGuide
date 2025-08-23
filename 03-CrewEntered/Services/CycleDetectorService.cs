using System.Collections.Generic;
using CrewEntered.Interfaces;

namespace CrewEntered.Services
{
    /// <summary>
    /// Service class for detecting cycles in directed graphs using Depth-First Search (DFS).
    /// Implements the ICycleDetector interface to provide cycle detection functionality
    /// for symbol dependency analysis.
    /// </summary>
    public class CycleDetectorService : ICycleDetector
    {
        /// <summary>
        /// Detects if there are any cycles in the given dependency graph using DFS algorithm.
        /// A cycle exists if there's a path from a node back to itself through its dependencies.
        /// </summary>
        /// <param name="dependencyGraph">Dictionary representing the dependency graph where 
        /// key is a symbol and value is a list of symbols it depends on</param>
        /// <returns>True if cycles are detected, False if no cycles exist</returns>
        public bool HasCycle(Dictionary<char, List<char>> dependencyGraph)
        {
            // Keep track of visited nodes to avoid redundant processing
            var visited = new HashSet<char>();
            // Keep track of nodes in current recursion stack to detect back edges
            var recursionStack = new HashSet<char>();

            // Check each node in the graph for cycles
            foreach (var node in dependencyGraph.Keys)
            {
                // If node hasn't been visited, start DFS from this node
                if (!visited.Contains(node))
                {
                    // If cycle is detected starting from this node, return true
                    if (DetectCycleDFS(node, dependencyGraph, visited, recursionStack))
                    {
                        return true;
                    }
                }
            }

            // No cycles found in the entire graph
            return false;
        }

        /// <summary>
        /// Performs depth-first search to detect cycles starting from a specific node.
        /// Uses recursion stack to identify back edges which indicate cycles.
        /// </summary>
        /// <param name="node">Starting node for cycle detection</param>
        /// <param name="dependencyGraph">The dependency graph</param>
        /// <param name="visited">Set of visited nodes</param>
        /// <param name="recursionStack">Set of nodes in current recursion stack</param>
        /// <returns>True if cycle is detected, False otherwise</returns>
        public bool DetectCycleDFS(char node, Dictionary<char, List<char>> dependencyGraph,
                                  HashSet<char> visited, HashSet<char> recursionStack)
        {
            // Mark current node as visited and add to recursion stack
            visited.Add(node);
            recursionStack.Add(node);

            // Check if the node has any dependencies
            if (dependencyGraph.ContainsKey(node))
            {
                // Explore all dependencies of the current node
                foreach (var dependency in dependencyGraph[node])
                {
                    // If dependency is not visited, recursively check for cycles
                    if (!visited.Contains(dependency))
                    {
                        if (DetectCycleDFS(dependency, dependencyGraph, visited, recursionStack))
                        {
                            return true; // Cycle found in deeper recursion
                        }
                    }
                    // If dependency is in recursion stack, we found a back edge (cycle)
                    else if (recursionStack.Contains(dependency))
                    {
                        return true; // Cycle detected
                    }
                }
            }

            // Remove node from recursion stack before backtracking
            recursionStack.Remove(node);
            return false; // No cycle found from this node
        }
    }
}
