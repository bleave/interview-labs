using System;
using System.Collections.Generic;
using System.Text;

//("build", "compile"),
//("compile", "restore"),
//("test", "build"),
//("deploy", "test")


namespace CSharpKatas
{
    static class EricDependicenyOrderTest1
    {
        public static void Run() {

            var dependencies = new (string item, string dependsOn)[] {
                ("build", "compile"),
                ("compile", "restore"),
                ("test", "build"),
                ("deploy", "test")
            };

            var list = GetList(dependencies);
            foreach (var item in list)
            {
                Console.WriteLine($"{item} >");
            }

        }

        public static List<string> GetList(IEnumerable<(string item, string dependencies)> deps) {

            var graph = new Dictionary<string, List<string>>();
            var preReq = new Dictionary<string, int>();

            //determine the graph and the preReq for each item
            foreach (var (item, dependencies) in deps)
            {
                graph.TryAdd(item, []);
                graph.TryAdd(dependencies, []);

                preReq.TryAdd(item, 0);
                preReq.TryAdd(dependencies, 0);

                graph[dependencies].Add(item);

                preReq[item]++;
            }

            //get the first item in the queue that has no dependencies so we can start there
            var q = new Queue<string>(preReq.Where(x => x.Value == 0).Select(x => x.Key));
            
            var result = new List<string>(preReq.Count);

            while (q.Count > 0) {

                var n = q.Dequeue();
                result.Add(n);

                foreach (var d in graph[n])
                {
                    if (--preReq[d] == 0) q.Enqueue(d);
                }
            }

            if (result.Count != preReq.Count) throw new Exception("No valid execution order exists.");

            return result;

        }
    }
}
