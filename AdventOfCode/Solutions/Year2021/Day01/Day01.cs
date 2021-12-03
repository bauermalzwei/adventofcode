using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{

    class Day01 : ASolution
    {
        int[] numbers;
        public Day01() : base(01, 2021, "")
        {
            string[] lines = Input.SplitByNewline();
            numbers = lines.Select(int.Parse).ToArray();

        }
        protected override string SolvePartOne()
        {
            var counter = 0;

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] - numbers[i - 1] > 0)
                    counter++;
            }

            return counter.ToString();

        }
        protected override string SolvePartTwo()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var counter = 0;
            for (int i = 0; i < numbers.Length -3; i++)
            {
                var window = numbers[i] + numbers[i + 1] + numbers[i + 2];
                var window2 = numbers[i + 1] + numbers[i + 2] + numbers[i + 3];
                if (window2 - window > 0)
                    counter++;
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedTicks);

            return counter.ToString();
        }



    }
}
