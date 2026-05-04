using CSharpKatas;
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Select demo to run:");
        Console.WriteLine("1 - Dependency Order");
        Console.WriteLine("1.5 - Dependency Order Fast");
        Console.WriteLine("2 - Rate Limiter");
        Console.WriteLine("3 - Group Anagrams");

        var input = Console.ReadLine();

        switch (input)
        {
            case "1":
                EricDependicenyOrderTest1.Run();
                break;
            case "1.5":
                TopoSortFast.Run();
                break;
            case "2":
                RateLimiterSemaphoreSlim.Run();
                break;
            case "3":
                GroupAnagrams.Run();
                break;
            case "4":
                ArtifactRemovals_Reachability.Run();
                break;
            default:
                Console.WriteLine("Invalid selection.");
                break;
        }
    }
}
