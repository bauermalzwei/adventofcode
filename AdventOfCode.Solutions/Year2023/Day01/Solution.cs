using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using System.Text;
using System;

namespace AdventOfCode.Solutions.Year2023.Day01;
class Solution : SolutionBase
{
    public Solution() : base(01, 2023, "") { }
    //The newly-improved calibration document consists of lines of text;
    //each line originally contained a specific calibration value that the Elves now need to recover.
    //On each line, the calibration value can be found by combining the first digit and the last digit(in that order)
    //to form a single two-digit number.
    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();
        var numbers = new List<int>();
        foreach (var l in lines)
        {
            RetrieveFirstAndLast(numbers, l);

        }

        var result = numbers.Sum().ToString();

        return result;
    }


    protected override string SolvePartTwo()
    {

        // our calculation isn't quite right. It looks like some of the digits are actually spelled out with letters: one, two, three, four, five, six, seven, eight, and nine also count as valid "digits".
        // issue -> overlap -> twone -> matches "two" and "one"
        // solution : replace all words in a way that the original overalp still exists e.g. two2two and one1one will result in:
        // twone1one -> two2twone1one
        // then get just the digits as before


        string cleanedInput = Input;

        foreach (var key in translations.Keys)
        {
            cleanedInput = cleanedInput.Replace(key, translations[key]);
        }
        var lines = cleanedInput.SplitByNewline();
        var numbers = new List<int>();
        foreach (var l in lines)
        {
            RetrieveFirstAndLast(numbers, l);
        }

        var result = numbers.Sum().ToString();
        return result;
    }


    Dictionary<string, string> translations = new(){
      {"one", "one1one"},
      {"two", "two2two"},
      {"three", "three3three"},
      {"four", "four4four"},
      {"five", "five5five"},
      {"six", "six6six"},
      {"seven", "seven7seven"},
      {"eight", "eight8eight"},
      {"nine", "nine9nine"}
    };

    private static void RetrieveFirstAndLast(List<int> numbers, string l)
    {
        var first = l.Where(c => Char.IsDigit(c)).FirstOrDefault();

        var last = l.Where(c => Char.IsDigit(c)).LastOrDefault();
        var n = new char[] { first, last };
        numbers.Add(Convert.ToInt32(new string(n)));
    }

}
