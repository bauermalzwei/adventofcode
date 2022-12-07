using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020.Day06;


class Solution : SolutionBase
{
    string[] groups;
    public Solution() : base(06, 2020, "")
    {
        groups ??= Input.SplitByEmptyLine();
    }

        //In this group, there are 6 questions to which anyone answered "yes": a, b, c, x, y, and z.
        //(Duplicate answers to the same question don't count extra; each question counts at most once.)
        //Another group asks for your help, then another, 
        //and eventually you've collected answers from every group on the plane (your puzzle input).
        //Each group's answers are separated by a blank line, and within each group, each person's answers are on a single line. For example:

        //abc

        //a
        //b
        //c

        //ab
        //ac

        //a
        //a
        //a
        //a

        //b
        //This list represents answers from five groups:

        //The first group contains one person who answered "yes" to 3 questions: a, b, and c.
        //The second group contains three people; combined, they answered "yes" to 3 questions: a, b, and c.
        //The third group contains two people; combined, they answered "yes" to 3 questions: a, b, and c.
        //The fourth group contains four people; combined, they answered "yes" to only 1 question, a.
        //The last group contains one person who answered "yes" to only 1 question, b.
        //In this example, the sum of these counts is 3 + 3 + 3 + 1 + 1 = 11.

        //For each group, count the number of questions to which anyone answered "yes". What is the sum of those counts?

    protected override string SolvePartOne()
    {
        var sum = 0;
        foreach (var g in groups)
        {
            Console.WriteLine($"Group: {g}");
            var answers = new List<string>();
            var personsAnswers = g.SplitByNewline();
            foreach (var a in personsAnswers)
            {
                Console.WriteLine($"Answer:{a}");
                //get each answer individually
                var arr = a.ToCharArray();
                foreach (var item in arr)
                {
                    answers.Add(item.ToString());
                }
            }
            var count = answers.Distinct().Count();
            Console.WriteLine($"Answers:{count}");
            sum += count;
            Console.WriteLine($"Sum: {sum}");

        }
        return sum.ToString();
    }

    //As you finish the last group's customs declaration, you notice that you misread one word in the instructions:
    //You don't need to identify the questions to which anyone answered "yes"; you need to identify the questions to which everyone answered "yes"!
    //Using the same example as above:

    //abc

    //a
    //b
    //c

    //ab
    //ac

    //a
    //a
    //a
    //a

    //b
    //This list represents answers from five groups:

    //In the first group, everyone (all 1 person) answered "yes" to 3 questions: a, b, and c.
    //In the second group, there is no question to which everyone answered "yes".
    //In the third group, everyone answered yes to only 1 question, a.Since some people did not answer "yes" to b or c, they don't count.
    //In the fourth group, everyone answered yes to only 1 question, a.
    //In the fifth group, everyone (all 1 person) answered "yes" to 1 question, b.
    //In this example, the sum of these counts is 3 + 0 + 1 + 1 + 1 = 6.

    protected override string SolvePartTwo()
    {
        var sum = 0;
        foreach (var g in groups)
        {
            Console.WriteLine($"Group: {g}");
            //entry per answer, if answer already in there, increase count
            var answers = new Dictionary<string, int>();
            int personCount = g.SplitByNewline().Count();
            foreach (var a in g.SplitByNewline())
            {
                Console.WriteLine($"Answer:{a}");
                //get each answer individually
                var arr = a.ToCharArray();
                foreach (var item in arr)
                {
                    if (answers.ContainsKey(item.ToString()))
                    {
                        answers[item.ToString()] += 1;
                    }
                    else
                        answers.Add(item.ToString(), 1);
                }
            }
            //find entry in dictionary that contains same count as ppl who answered
            if (answers.ContainsValue(personCount))
            {
                var identicalAnswers = answers.Where(k => k.Value == personCount).Count();
                Console.WriteLine($"There are {personCount} ppl in the group. They answered {identicalAnswers} identically.");
                sum += identicalAnswers;
            }
            Console.WriteLine($"Sum: {sum}");

        }
        return sum.ToString();
    }
}
