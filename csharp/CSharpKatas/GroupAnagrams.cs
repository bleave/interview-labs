/*
WHAT:
  Given a list of words, group words that are anagrams of each other.
  Anagrams contain the same letters in a different order.

Example:
  ["eat","tea","tan","ate","nat","bat"]
  => [["eat","tea","ate"], ["tan","nat"], ["bat"]]

HOW:
  Use a Dictionary<string, List<string>> where the key represents the "signature" of a word.
  A simple signature is the letters sorted alphabetically.
    "eat" -> "aet"
    "tea" -> "aet"
    "ate" -> "aet"

  1) For each word:
       - compute key (sorted letters)
       - add word to dict[key]
  2) Return dict.Values

EDGE CASES:
  - Empty input => empty output
  - Duplicates => they stay in the same group
  - Case sensitivity: decide and be consistent (here: case-insensitive by default)

COMPLEXITY:
  Let N = number of words, K = average word length
  Sorting key costs O(K log K)
  Total time: O(N * K log K)
  Space: O(N * K) for storing groups/keys
*/

using System;
using System.Collections.Generic;
using System.Linq;

public static class GroupAnagrams
{
    public static void Run()
    {
        Console.WriteLine("Talk track: normalize word -> compute signature (sorted letters) -> group in dictionary by signature.");
        Console.WriteLine();

        var words = new[] { "eat", "tea", "tan", "ate", "nat", "bat", "tab", "Tea" };

        var groups = Group(words);

        Console.WriteLine("Groups:");
        var i = 1;
        foreach (var g in groups)
        {
            Console.WriteLine($"{i++}: [{string.Join(", ", g)}]");
        }

        Console.WriteLine();
        Console.WriteLine("Done.");
    }

    public static List<List<string>> Group(IEnumerable<string> words)
    {
        if (words is null) throw new ArgumentNullException(nameof(words));

        // key -> list of original words
        var map = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        foreach (var word in words)
        {
            if (word is null) continue; // skip nulls (or throw; depends on requirements)

            // Normalize for grouping: treat "Tea" same as "tea"
            var normalized = word.ToLowerInvariant();

            // Signature key: sorted letters
            var key = GetSignature(normalized);

            if (!map.TryGetValue(key, out var list))
            {
                list = new List<string>();
                map[key] = list;
            }

            // Keep original word (so output preserves original casing)
            list.Add(word);
        }

        return map.Values.Select(list => list.ToList()).ToList();
    }

    private static string GetSignature(string word)
    {
        // Convert to char array, sort, then create a string back
        var chars = word.ToCharArray();
        Array.Sort(chars);
        return new string(chars);
    }
}
