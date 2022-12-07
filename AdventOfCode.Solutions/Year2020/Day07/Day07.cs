using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020.Day07;


class Solution : SolutionBase
{
    Dictionary<string, List<BagContent>> Bags; //parentbag/color, children (count + color)
    string[] Lines;
    private const string GOLDBAG = "shiny gold";
    public Solution() : base(07, 2020, "")
    {
        Lines ??= Input.SplitByNewline();
        Bags ??= GetAllBags(Lines);
    }


    //consider the following rules:

    //light red bags contain 1 bright white bag, 2 muted yellow bags.
    //dark orange bags contain 3 bright white bags, 4 muted yellow bags.
    //bright white bags contain 1 shiny gold bag.
    //muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
    //shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
    //dark olive bags contain 3 faded blue bags, 4 dotted black bags.
    //vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
    //faded blue bags contain no other bags.
    //dotted black bags contain no other bags.
    //These rules specify the required contents for 9 bag types.In this example, every faded blue bag is empty, every vibrant plum bag contains 11 bags (5 faded blue and 6 dotted black), and so on.

    //You have a shiny gold bag.If you wanted to carry it in at least one other bag,
    //how many different bag colors would be valid for the outermost bag? 
    //(In other words: how many colors can, eventually, contain at least one shiny gold bag?)

    //In the above rules, the following options would be available to you:

    //A bright white bag, which can hold your shiny gold bag directly.
    //A muted yellow bag, which can hold your shiny gold bag directly, plus some other bags.
    //A dark orange bag, which can hold bright white and muted yellow bags, either of which could then hold your shiny gold bag.
    //A light red bag, which can hold bright white and muted yellow bags, either of which could then hold your shiny gold bag.
    //So, in this example, the number of bag colors that can eventually contain at least one shiny gold bag is 4.
    protected override string SolvePartOne()
    {

        var goldBags = Bags.Select(b => b.Key).Where(k => findMatchingBag(k, GOLDBAG, Bags)).Count();

        return goldBags.ToString();
    }

    protected override string SolvePartTwo()
    {
        return (CountAllBagsWithin(GOLDBAG, Bags) - 1).ToString();
    }

    private bool findMatchingBag(string bagToFind, string color, Dictionary<string, List<BagContent>> bags)
    {
        var bag = bags[bagToFind];

        foreach (var b in bag)
        {
            if (b.Color == color)
                return true;

            if (findMatchingBag(b.Color, color, bags))
                return true;
        }

        return false;
    }

    private int CountAllBagsWithin(string bagToFind, Dictionary<string, List<BagContent>> bags)
    
    {
        var bag = bags[bagToFind];

        return 1 + bag.Select(b => b.Count * CountAllBagsWithin(b.Color, bags)).Sum();
    }
    private Dictionary<string, List<BagContent>> GetAllBags(string[] inputLines)
    {
        var bags = new Dictionary<string, List<BagContent>>();
        //fill the dictionary with parent and child bags
        foreach (var l in inputLines)
        {
            var line = l.Replace("bags", "bag"); //plural not relevant, easier for filtering
            //sring contains "." -> not needed
            line = line.Replace(".", "");
            //split and get color
            var parts = line.Split("contain");
            bags.Add(parts[0].Replace("bag","").Trim(), ParseContent(parts[1])); //get rid of "bag" keyword for Dictionary key 
        }
        return bags;
    }
    private List<BagContent> ParseContent(string rule)
    {
        var bags = rule.Split(",", StringSplitOptions.RemoveEmptyEntries);
        var bagContents = new List<BagContent>();
        foreach (var bag in bags)
        {
            //get number and text seperately
            var match = Regex.Match(bag, @"([0-9]+)(\s\w.+)\s");
            if (match.Success)
            {
                //matches three groups, first being everything(?), second being the number, third being the text
                bagContents.Add(new BagContent
                {
                    Count = Convert.ToInt32(match.Groups[1].Captures[0].Value.Trim()),
                    Color = (match.Groups[2].Captures[0].Value.Trim())
                });
            }

        }
        return bagContents;
    }

    class BagContent
    {
        public int Count { get; set; }
        public string Color { get; set; }
    }

}
