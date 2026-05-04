using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpKatas
{
    public static class EricDependencyOrderTest
    {
        public static void Run() {

            var dependencies = new (string item, string dependsOn)[]
            {
                ("build", "compile"),
                ("compile", "restore"),
                ("test", "build"),
                ("deploy", "test")
            };

            var order = GetDependencies(dependencies);
            Console.WriteLine("Execution Order:");
            foreach (var step in order)
                Console.WriteLine($" - {step}");
        }
         
        public static List<string> GetDependencies(IEnumerable<(string item, string dependsOn)> deps)
        {
            var graph = new Dictionary<string, List<string>>();
            var remainingPreq = new Dictionary<string, int>();

            foreach (var (item, dependsOn) in deps)
            {
                // Ensure both nodes exist in graph
                graph.TryAdd(dependsOn, []);
                graph.TryAdd(item, []);

                // Ensure both nodes exist in remainingPreq
                remainingPreq.TryAdd(dependsOn, 0);
                remainingPreq.TryAdd(item, 0);

                // Record that item waits on dependsOn
                graph[dependsOn].Add(item);

                // Item has one more blocker
                remainingPreq[item]++;
            }

            //find the item in the queue that has no dependencies so we can start there
            var q = new Queue<string>(remainingPreq.Where(x => x.Value == 0).Select(x => x.Key));
            
            //create a new list to hold the result of the order of execution
            var result = new List<string>(remainingPreq.Count);

            //loop through the queue until we have processed all items
            while (q.Count > 0) 
            {
                //dequeue the next item and add it to the result list
                var n = q.Dequeue();
                //add the item to the result list
                result.Add(n);

                //loop through the dependencies of the item we just added to the result list and decrement the remainingPreq for each dependency
                foreach (var d in graph[n]) {
                    //decrement the remainingPreq for the dependency (decrement to check if we have processed all of the dependencies for the item)
                    if (--remainingPreq[d] == 0) q.Enqueue(d); //if the remainingPreq for the dependency is now 0 then we can add it to the queue to be processed next
                }
            }

            //if the result count does not equal the remainingPreq count then we have a cycle and we should throw an exception
            if (result.Count != remainingPreq.Count) throw new InvalidOperationException("Cycle detected. No valid execution order exists.");

            return result;
        }
    }
}