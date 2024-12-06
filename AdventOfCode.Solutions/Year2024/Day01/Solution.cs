using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode.Solutions.Year2024.Day01;

class Solution : SolutionBase
{
    public Solution() : base(01, 2024, "") { }

    private List<int> left;
    private List<int> right;

    protected override string SolvePartOne()
    {
        var sw = new Stopwatch();
        sw.Start();
        left = new List<int>();
        right = new List<int>();
        var lines = Input.SplitByNewline();
        foreach (var l in lines)
        {
            var pair = l.Split("   ", StringSplitOptions.RemoveEmptyEntries);
            left.Add(int.Parse(pair[0]));
            right.Add(int.Parse(pair [1]));
        }


        left.Sort();
        right.Sort();
        var distance = 0;

        for (int i = 0; i < left.Count; i++)
        {
            
            distance += Math.Abs(right[i] - left[i]) ;
        }
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
        return distance.ToString() ;
    }

    protected override string SolvePartTwo()
    {
        var sw = new Stopwatch();
        sw.Start();

        var similarityScore = 0;
        var occurances = right.GroupBy(x => x)
              .Where(g => g.Count() >= 1)
              .ToDictionary(x => x.Key, y => y.Count());

        foreach (var l in left)
        {
            // multiply each number by occurances

            int occ;
            if (occurances.TryGetValue(l, out occ))
                similarityScore += l * occ;
     

        }
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
        return similarityScore.ToString();
    }
}
