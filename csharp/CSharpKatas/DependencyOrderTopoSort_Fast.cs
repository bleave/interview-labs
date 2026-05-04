using System;
using System.Collections.Generic;
using System.Linq;

public static class TopoSortFast
{
    public static void Run()
    {
        var dependencies = new (string item, string dependsOn)[]
        {
            ("build", "compile"),
            ("compile", "restore"),
            ("test", "build"),
            ("deploy", "test")
        };
        var order = GetExecutionOrder(dependencies);

        Console.WriteLine("Execution Order:");
        foreach (var step in order)
            Console.WriteLine($" - {step}");
    }

    // Kahn's algorithm: indegree + queue. Cycle if result count != node count.
    public static List<string> GetExecutionOrder(IEnumerable<(string item, string dependsOn)> deps)
    {
        if (deps is null) throw new ArgumentNullException(nameof(deps));

        var graph = new Dictionary<string, List<string>>();
        var remainingPrereqs = new Dictionary<string, int>();

        foreach (var (item, dependsOn) in deps)
        {
            if (!graph.ContainsKey(dependsOn)) graph[dependsOn] = [];
            if (!graph.ContainsKey(item)) graph[item] = [];

            if (!remainingPrereqs.ContainsKey(dependsOn)) remainingPrereqs[dependsOn] = 0;
            if (!remainingPrereqs.ContainsKey(item)) remainingPrereqs[item] = 0;

            graph[dependsOn].Add(item);
            remainingPrereqs[item]++;
        }

        var q = new Queue<string>(remainingPrereqs.Where(x => x.Value == 0).Select(x => x.Key));
        var result = new List<string>(remainingPrereqs.Count);

        while (q.Count > 0)
        {
            var n = q.Dequeue();
            result.Add(n);

            foreach (var d in graph[n])
                if (--remainingPrereqs[d] == 0) q.Enqueue(d);
        }

        if (result.Count != remainingPrereqs.Count) throw new InvalidOperationException("Cycle detected.");
        return result;
    }
}