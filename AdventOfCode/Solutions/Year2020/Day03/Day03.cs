using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day03 : ASolution
    {
        string[] lines;
        public Day03() : base(03, 2020, "")
        {
            lines = Input.SplitByNewline(); 
        }

        protected override string SolvePartOne()
        {
            return CountEncounteredTree(lines, 3, 1).ToString();
        }

        protected override string SolvePartTwo()
        {
            var slopesToTest = new List<Point>() {
                new Point(1,1),
                new Point(3,1),
                new Point(5,1),
                new Point(7,1),
                new Point (1,2)
            };
            long result = 1;
            foreach (var slope in slopesToTest)
            {
                var trees = CountEncounteredTree(lines, slope.X, slope.Y);
                result = result * trees;
            }
            return result.ToString();
        }

        private int CountEncounteredTree(string[] lines, int rightMove, int downMove)
        {
            int trees = 0;
            int position = 0;
            for (int i = downMove; i < lines.Length; i += downMove)
            {
                var slope = lines[i];
                position += rightMove;
                while (position >= slope.Length)
                {
                    slope = slope + slope;
                }
                if (slope[position] == '#')
                    trees++;
            }

            return trees;
        }
    }
}
