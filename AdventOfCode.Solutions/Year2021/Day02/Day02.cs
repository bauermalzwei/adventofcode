using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021.Day02;


class Solution : SolutionBase
{

    //forward 5
    //down 5
    //forward 8
    //up 3
    //down 8
    //forward 2

    string[] lines;
    int[] number;
    string[] direction;

    public Solution() : base(02, 2021, "")
    {
        lines = Input.SplitByNewline();
        Console.WriteLine($"Number of lines: {lines.Length}");

    }
    protected override string SolvePartOne()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        var horizontal = 0;
        var depth = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            var commandLine = lines[i].Split(" ");
            var direction = commandLine[0];
            var number = Convert.ToInt32(commandLine[1]);

            if (direction.Equals("up"))
            {
                depth -= number;
            }
            else if (direction.Equals("down"))
            {
                depth += number;
            }
            else if (direction.Equals("forward"))
                horizontal += number;

        }
        Console.WriteLine($"Depth: {depth}");
        Console.WriteLine($"Horizontal: {horizontal}");
        var result = (depth * horizontal).ToString();
        sw.Stop();
        Console.WriteLine(sw.ElapsedTicks);
        return result;

    }

    //down X increases your aim by X units.
    //up X decreases your aim by X units.
    //forward X does two things:
    //It increases your horizontal position by X units.
    //It increases your depth by your aim multiplied by X.

//forward 5 adds 5 to your horizontal position, a total of 5. Because your aim is 0, your depth does not change.
//down 5 adds 5 to your aim, resulting in a value of 5.
//forward 8 adds 8 to your horizontal position, a total of 13. Because your aim is 5, your depth increases by 8*5=40.
//up 3 decreases your aim by 3, resulting in a value of 2.
//down 8 adds 8 to your aim, resulting in a value of 10.
//forward 2 adds 2 to your horizontal position, a total of 15. Because your aim is 10, your depth increases by 2*10=20 to a total of 60.

    protected override string SolvePartTwo()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        var horizontal = 0;
        var depth = 0;
        var aim = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            var commandLine = lines[i].Split(" ");
            var direction = commandLine[0];
            var number = Convert.ToInt32(commandLine[1]);

            if (direction.Equals("up"))
            {
                aim -= number;
                
            }
            else if (direction.Equals("down"))
            {
                aim += number;
            }
            else if (direction.Equals("forward"))
            {
                horizontal += number;
                depth += aim * number;
            }
                

        }
        Console.WriteLine($"Depth: {depth}");
        Console.WriteLine($"Horizontal: {horizontal}");
        var result = (depth * horizontal).ToString();
        sw.Stop();
        Console.WriteLine(sw.ElapsedTicks);
        return result;
    }



}
