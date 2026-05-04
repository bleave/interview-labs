/*
WHAT:
  Given (item, dependsOn) pairs, return a valid execution order so every dependency
  appears before items that depend on it.

HOW (Iterative Topological Sort: Kahn's Algorithm):
  Model dependencies as edges: dependsOn -> item
    Example: ("build" depends on "compile") becomes: compile -> build

  Steps:
    1) Build adjacency list: prerequisite -> [dependents]
    2) Build indegree: indegree[node] = number of prerequisites
    3) Enqueue all nodes with indegree 0 (no prerequisites)
    4) While queue not empty:
         - dequeue node, add to result
         - for each dependent:
             indegree[dependent]--
             if indegree hits 0, enqueue
    5) If result count < total nodes => cycle exists

---------------------------------------------------------------------

DATA STRUCTURES USED:

Dictionary<TKey, TValue>
  - Key/value map, O(1) average lookup
  - Used for adjacency list (graph) and indegree tracking

Queue<T>
  - First-In-First-Out (FIFO)
  - Used to process nodes that are "ready" (indegree == 0)
  - Why?
      Kahn's algorithm repeatedly removes ready nodes from the front

HashSet<T> (not required here, but common in other graph traversals)
  - Unique values, O(1) average membership test
  - Often used for visited/cycle detection in DFS-style solutions

Difference (quick mental anchor):
  HashSet -> membership (have we seen this?)
  Queue   -> processing order (who's next to process?)

---------------------------------------------------------------------

EDGE CASES:
  - Nodes that appear only as dependencies still must be included.
  - Duplicate edges can inflate indegree; if your input may contain duplicates,
    you can dedupe adjacency lists with HashSet instead of List.
  - Cycles should be detected and reported.

COMPLEXITY:
  Time:  O(V + E)
  Space: O(V + E)
*/

using System;
using System.Collections.Generic;
using System.Linq;

public static class DependencyOrderToposort
{
    public static void Run()
    {
        // Sample input: item dependsOn prerequisite
        var dependencies = new (string item, string dependsOn)[]
        {
            ("build", "compile"),
            ("compile", "restore"),
            ("test", "build"),
            ("deploy", "test")
        };

        Console.WriteLine("Talk track: Kahn's algorithm (indegree + queue). Iterative topo sort. Cycle if result count < node count.");
        Console.WriteLine();

        var order = GetExecutionOrder(dependencies);

        Console.WriteLine("Execution Order:");
        foreach (var step in order)
            Console.WriteLine($" - {step}");

        Console.WriteLine();
       
        Console.WriteLine("Done.");
    }

    public static List<string> GetExecutionOrder(IEnumerable<(string item, string dependsOn)> dependencies)
    {
        if (dependencies is null) throw new ArgumentNullException(nameof(dependencies));

        // graph: prerequisite -> list of dependents
        var graph = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        // remainingPrereqs: node -> number of prerequisites
        var remainingPrereqs = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // Build graph + remainingPrereqs
        foreach (var (item, dependsOn) in dependencies)
        {
            // Ensure nodes exist even if they have no outgoing edges
            if (!graph.ContainsKey(dependsOn))                 
                graph[dependsOn] = []; //new List<string>();

            if (!graph.ContainsKey(item)) 
                graph[item] = [];

            if (!remainingPrereqs.ContainsKey(dependsOn)) 
                remainingPrereqs[dependsOn] = 0;
            
            if (!remainingPrereqs.ContainsKey(item)) 
                remainingPrereqs[item] = 0;

            // Edge: dependsOn -> item
            graph[dependsOn].Add(item);

            // item has one more prerequisite
            remainingPrereqs[item]++;
        }

        // Start with all nodes that have no prerequisites
        var queue = new Queue<string>(remainingPrereqs.Where(kvp => kvp.Value == 0).Select(kvp => kvp.Key));

        var result = new List<string>(capacity: remainingPrereqs.Count);

        // Process ready nodes
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            result.Add(node);

            // Reduce remainingPrereqs of dependents
            foreach (var dependent in graph[node])
            {
                remainingPrereqs[dependent]--;

                // If dependent is now free of prerequisites, it becomes ready
                if (remainingPrereqs[dependent] == 0)
                    queue.Enqueue(dependent);
            }
        }

        // If we didn't process all nodes, a cycle exists
        if (result.Count != remainingPrereqs.Count)
            throw new InvalidOperationException("Cycle detected. No valid execution order exists.");

        return result;
    }
}
