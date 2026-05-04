/*
WHAT:
  Given:
    - dependencies: artifact -> list of artifacts it depends on
    - runtimeUsedArtifacts: artifacts known to be required at runtime
    - allArtifacts: everything currently in the build/output

  Return the set of removable artifacts:
    removable = allArtifacts - (runtimeUsedArtifacts + their transitive dependencies)

HOW:
  Graph reachability from a set of roots (runtimeUsedArtifacts).
  Use an explicit Stack to do iterative DFS (avoid recursion):
    - Pop an artifact
    - If we've already visited it, continue (prevents cycles + repeated work)
    - Otherwise add to keep-set, then push its dependencies

EDGE CASES:
  - Cycles in dependency graph: handled by visited/keep set
  - Missing dependency entries: treat as no dependencies
  - Case differences in names: use OrdinalIgnoreCase sets/dicts if appropriate

COMPLEXITY:
  Let V = number of artifacts, E = number of dependency edges
  Time:  O(V + E) over the reachable subgraph
  Space: O(V) for keep/stack in worst case
*/

using System;
using System.Collections.Generic;

public static class ArtifactRemovals_Reachability
{
    public static void Run()
    {
        var dependencies = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["Core.dll"] = new() { "Utils.dll" },
            ["Legacy.dll"] = new() { "Utils.dll" }
        };

        var runtimeUsedArtifacts = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Core.dll",
            "Utils.dll"
        };

        var allArtifacts = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Core.dll",
            "Utils.dll",
            "Legacy.dll",
            "BuildHelper.exe"
        };

        Console.WriteLine("Talk track: reachability from runtime-used roots; iterative DFS using stack; removable = all - keep.");
        Console.WriteLine();

        var removable = GetRemovableArtifacts(dependencies, runtimeUsedArtifacts, allArtifacts);

        Console.WriteLine("Removable artifacts:");
        foreach (var a in removable)
            Console.WriteLine($" - {a}");

        Console.WriteLine();
        Console.WriteLine("Done.");
    }

    public static HashSet<string> GetRemovableArtifacts(
        IDictionary<string, List<string>> dependencies,
        ISet<string> runtimeUsedArtifacts,
        ISet<string> allArtifacts)
    {
        if (dependencies is null) throw new ArgumentNullException(nameof(dependencies));
        if (runtimeUsedArtifacts is null) throw new ArgumentNullException(nameof(runtimeUsedArtifacts));
        if (allArtifacts is null) throw new ArgumentNullException(nameof(allArtifacts));

        // Keep = runtime-used + all their transitive dependencies
        var keep = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Explicit stack = iterative DFS (avoids recursion)
        var stack = new Stack<string>(runtimeUsedArtifacts);

        while (stack.Count > 0)
        {
            var artifact = stack.Pop();

            // visited check handles cycles + prevents repeated work
            if (!keep.Add(artifact))
                continue;

            // Push direct dependencies (if any)
            if (dependencies.TryGetValue(artifact, out var deps) && deps is not null)
            {
                foreach (var dep in deps)
                    stack.Push(dep);
            }
        }

        // Removable = allArtifacts - keep
        var removable = new HashSet<string>(allArtifacts, StringComparer.OrdinalIgnoreCase);
        removable.ExceptWith(keep);

        return removable;
    }
}
