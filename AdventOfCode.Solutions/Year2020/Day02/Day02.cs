using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020.Day02
{

    class Solution : SolutionBase
    {
        string[] lines;
        int validPasswords = 0;
        public Solution() : base(02, 2020, "")
        {
             lines = Input.SplitByNewline();
        }

        protected override string SolvePartOne()
        {
            validPasswords = 0;
            foreach (var l in lines)
            {
                var arr = l.Split(":");
                var password = arr[1];
                var policy = arr[0];
                if (IsCountPolicyFullfilled(password, policy))
                    validPasswords++;
            }

            
            return validPasswords.ToString();

        }

//        Each policy actually describes two positions in the password,  
//        where 1 means the first character, 2 means the second character, and so on.
//        (Be careful; Toboggan Corporate Policies have no concept of "index zero"!) 
//        Exactly one of these positions must contain the given letter.
//       Other occurrences of the letter are irrelevant for the purposes of policy enforcement.

//          Given the same example list from above:

            //1-3 a: abcde is valid: position 1 contains a and position 3 does not.
            //1-3 b: cdefg is invalid: neither position 1 nor position 3 contains b.
            //2-9 c: ccccccccc is invalid: both position 2 and position 9 contain c.

        protected override string SolvePartTwo()
        {
            validPasswords = 0;
            foreach (var l in lines)
            {
                var arr = l.Split(":");
                var password = arr[1];
                var policy = arr[0];
                if (IsPositionPolicyFullfilled(password, policy))
                    validPasswords++;
            }


            return validPasswords.ToString();
        }

        private bool IsPositionPolicyFullfilled(string password, string policy)
        {
            var arr = policy.Split(" ");
            var character = Convert.ToChar(arr[1]);
            var allowedPositions = arr[0].Split("-");
            var firstPos = Convert.ToInt16(allowedPositions[0]);
            var secondPos = Convert.ToInt16(allowedPositions[1]);
            if ((password[firstPos] == character) && (password[secondPos] == character))
                return false;
            else if ((password[firstPos] == character) || (password[secondPos] == character))
                return true;
            return false;
        }

        //For example, suppose you have the following list:
        //1-3 a: abcde
        //1-3 b: cdefg
        //2-9 c: ccccccccc
        //Each line gives the password policy and then the password.
        //The password policy indicates the lowest and highest number of times a given letter must appear for the password to be valid.
        //For example, 1-3 a means that the password must contain a at least 1 time and at most 3 times.

        private bool IsCountPolicyFullfilled(string password, string policy)
        {
            var arr = policy.Split(" ");
            var character = Convert.ToChar(arr[1]);
            var limits = arr[0].Split("-");
            var minCount = Convert.ToInt16(limits[0]);
            var maxCount = Convert.ToInt16(limits[1]);

            var count = password.Count(c => c == character);
            if ((count >= minCount) && (count <= maxCount))
                return true;
            else
                return false;

        }
    }
}
