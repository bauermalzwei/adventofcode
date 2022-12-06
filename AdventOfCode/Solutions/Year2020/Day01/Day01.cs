using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day01 : ASolution
    {
        private int[] numbers;
        public Day01() : base(01, 2020, "")
        {
            string[] lines = Input.SplitByNewline();
            numbers = lines.Select(int.Parse).ToArray();

        }

        protected override string SolvePartOne()
        {

            foreach (var i in numbers)
            {
                foreach (var k in numbers)
                {
                    if (i + k == 2020)
                        return (i * k).ToString();
                }
            }
            return null;
        }

        protected override string SolvePartTwo()
        {
            foreach (var i in numbers)
            {
                foreach (var k in numbers)
                {
                    foreach (var j in numbers)
                    {
                        if (i + k + j == 2020)
                            return (i * k * j).ToString();
                    }
                }
            }
            return null;
        }
    }
}
