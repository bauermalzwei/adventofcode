using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Solutions;

namespace AdventOfCode.Solutions.Year2020.Day04
{

    class Solution : SolutionBase
    {

        string[] passports;
        string[] requiredFields = { "byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:" }; //cid is not required
        //for part 2

        //byr(Birth Year) - four digits; at least 1920 and at most 2002.
        //iyr(Issue Year) - four digits; at least 2010 and at most 2020.
        //eyr(Expiration Year) - four digits; at least 2020 and at most 2030.
        //hgt(Height) - a number followed by either cm or in:
        //If cm, the number must be at least 150 and at most 193.
        //If in, the number must be at least 59 and at most 76.
        //hcl(Hair Color) - a # followed by exactly six characters 0-9 or a-f.
        //ecl(Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
        //pid(Passport ID) - a nine-digit number, including leading zeroes.
        //cid(Country ID) - ignored, missing or not.
        Dictionary<string, string> FieldRules = new Dictionary<string, string> 
                                                { 
                                                    { "byr", @"\b(19[2-9][0-9]|200[0-2])\b" },
                                                    { "iyr", @"\b(20[1][0-9]|202[0])\b" },
                                                    { "eyr", @"\b(20[2][0-9]|203[0])\b" },
                                                    { "hgt", @"\b(1[5-8][0-9]cm|19[0-3]cm|59in|6[0-9]in|7[0-6]in)\b" },
                                                    { "hcl", @"\#[0-9a-f]{6}"},
                                                    { "ecl", @"\b(amb|blu|brn|gry|grn|hzl|oth)\b"},
                                                    { "pid", @"\b[0-9]{9}\b" },
                                                    { "cid", @".*" } //just accept any value
                                                };
 
        public Solution() : base(04, 2020, "")
        {
            passports = Input.SplitByEmptyLine();
        }


        //detecting which passports have all required fields.The expected fields are as follows:
        //byr(Birth Year)
        //iyr(Issue Year)
        //eyr(Expiration Year)
        //hgt(Height)
        //hcl(Hair Color)
        //ecl(Eye Color)
        //pid(Passport ID)
        //cid(Country ID)

        //Passport data is validated in batch files(your puzzle input).
        //Each passport is represented as a sequence of key:value pairs separated by spaces or newlines.Passports are separated by blank lines.
        //Here is an example batch file containing four passports:

        //ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
        //byr:1937 iyr:2017 cid:147 hgt:183cm

        //iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
        //hcl:#cfa07d byr:1929

        //hcl:#ae17e1 iyr:2013
        //eyr:2024
        //ecl:brn pid:760753108 byr:1931
        //hgt:179cm

        //hcl:#cfa07d eyr:2025 pid:166559648
        //iyr:2011 ecl:brn hgt:59in

        //The first passport is valid - all eight fields are present.The second passport is invalid - it is missing hgt (the Height field).
        //The third passport is interesting; the only missing field is cid, so it looks like data from North Pole Credentials, not a passport at all!
        //Surely, nobody would mind if you made the system temporarily ignore missing cid fields.Treat this "passport" as valid.
        //The fourth passport is missing two fields, cid and byr.
        //Missing cid is fine, but missing any other field is not, so this passport is invalid.
        //According to the above rules, your improved system would report 2 valid passports.

        protected override string SolvePartOne()
        {
            var validPassports = 0;
            foreach (var p in passports)
            {
                //get the fields
                var fields = p.Split(new[] { " ", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (fields.Length == 8) // if 8 fields its instant complete because only 8 fields exists. Not checking for duplicates here
                    validPassports++;
                else
                {
                    if (ContainsAllValidFields(fields))
                        validPassports++;
                }
                
            }

            return validPassports.ToString();
        }

        //You can continue to ignore the cid field, but each other field has strict rules about what values are valid for automatic validation:


        protected override string SolvePartTwo()
        {
            var validPassports = 0;
            foreach (var p in passports)
            {
                //get the fields
                var fields = p.Split(new[] { " ", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (fields.Length == 8 && ValidateFieldRules(fields)) // if 8 fields its instant complete because only 8 fields exists. Not checking for duplicates here
                    validPassports++;
                else
                {
                    if (ContainsAllValidFields(fields) && ValidateFieldRules(fields))
                        validPassports++;
                }

            }

            return validPassports.ToString();
        }
        private bool ValidateFieldRules(string[] fields)
        {
            var isValid = true;
            //split fields into fieldname + content
            //find matching RegEx rule
            foreach (var f in fields)
            {
                var pair = f.Split(":");
                var key = pair[0];
                var value = pair[1]; //for debugging
                var rule = FieldRules[key];
                if (!Regex.IsMatch(value, rule))
                    return false;
                
            }

            return isValid;
        }
        private bool ContainsAllValidFields(string[] fields)
        {
            var isValid = true;
            foreach (var rf in requiredFields)
            {
                isValid = fields.Where(s => s.Contains(rf)).Any();
                if (!isValid)
                    return isValid;
            }
            return isValid;
        }

    }
}
