using System;
using System.Collections.Generic;

public static class ArtifactRemovalsFast
{
    // Reachability from runtime-used roots; keep = roots + transitive deps; removable = all - keep
    public static HashSet<string> GetRemovableArtifacts(
        IDictionary<string, List<string>> deps,
        ISet<string> runtimeRoots,
        ISet<string> all)
    {
        if (deps is null) throw new ArgumentNullException(nameof(deps));
        if (runtimeRoots is null) throw new ArgumentNullException(nameof(runtimeRoots));
        if (all is null) throw new ArgumentNullException(nameof(all));

        var keep = new HashSet<string>();
        var stack = new Stack<string>(runtimeRoots);

        while (stack.Count > 0)
        {
            var a = stack.Pop();
            if (!keep.Add(a)) continue;

            if (deps.TryGetValue(a, out var direct))
                for (int i = 0; i < direct.Count; i++)
                    stack.Push(direct[i]);
        }

        var removable = new HashSet<string>(all);
        removable.ExceptWith(keep);
        return removable;
    }
}