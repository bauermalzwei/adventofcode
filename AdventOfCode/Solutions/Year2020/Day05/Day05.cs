using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{

    class Day05 : ASolution
    {

        string[] boardingPasses;
        List<int> SeatIds = new List<int>();
        public Day05() : base(05, 2020, "")
        {
            boardingPasses = Input.SplitByNewline();
        }

        //Instead of zones or groups, this airline uses binary space partitioning to seat people.
        //A seat might be specified like FBFBBFFRLR, where F means "front", B means "back", L means "left", and R means "right".

        //The first 7 characters will either be F or B; these specify exactly one of the 128 rows
        //on the plane(numbered 0 through 127). Each letter tells you which half of a region the given seat is in. Start with the whole list of rows; the first letter indicates whether the seat is in the front(0 through 63) or the back(64 through 127). The next letter indicates which half of that region the seat is in, and so on until you're left with exactly one row.

        //For example, consider just the first seven characters of FBFBBFFRLR:

        //Start by considering the whole range, rows 0 through 127.
        //F means to take the lower half, keeping rows 0 through 63.
        //B means to take the upper half, keeping rows 32 through 63.
        //F means to take the lower half, keeping rows 32 through 47.
        //B means to take the upper half, keeping rows 40 through 47.
        //B keeps rows 44 through 47.
        //F keeps rows 44 through 45.
        //The final F keeps the lower of the two, row 44.
        //The last three characters will be either L or R; these specify exactly one of the 8 columns of seats on the plane(numbered 0 through 7). The same process as above proceeds again, this time with only three steps.L means to keep the lower half, while R means to keep the upper half.

        //For example, consider just the last 3 characters of FBFBBFFRLR:

        //Start by considering the whole range, columns 0 through 7.
        //R means to take the upper half, keeping columns 4 through 7.
        //L means to take the lower half, keeping columns 4 through 5.
        //The final R keeps the upper of the two, column 5.
        //So, decoding FBFBBFFRLR reveals that it is the seat at row 44, column 5.

        //Every seat also has a unique seat ID: multiply the row by 8, then add the column.In this example, the seat has ID 44 * 8 + 5 = 357.

        protected override string SolvePartOne()
        {
            //string[] input = {
            //                   "FBFBBFFRLR", //: row 44, column 5, seat ID 357.
            //                   "BFFFBBFRRR", //: row 70, column 7, seat ID 567.
            //                   "FFFBBBFRRR", //: row 14, column 7, seat ID 119.
            //                   "BBFFBBFRLL"  //: row 102, column 4, seat ID 820.
            //                 };

            int highestSeatId = 0;
            foreach(var i in boardingPasses)
            {
                var row = GetBinarySpacePartitionResult(i.Substring(0, 7), 127, 'B');
                var seat = GetBinarySpacePartitionResult(i.Substring(7, 3), 8, 'R');
                var seatId = ((row * 8) + seat);
                SeatIds.Add(seatId);
                Console.WriteLine($"Row: {row}, Seat: {seat}, SeatId: {seatId}.");
                if (seatId > highestSeatId) highestSeatId = seatId;

            }
            return highestSeatId.ToString();
        }

        protected override string SolvePartTwo()
        {
            //find missing seatId
            //not very first/last
            //ids with +1/-1 must exist
            var ids = SeatIds.ToArray();
            Array.Sort(ids);
            var mySeat = 0;
            var offset = ids[0];

            for (int i = 0; i < ids.Length; i++)
            {
                if (ids[i] != i+offset)
                {
                    var current = ids[i];
                    var last = ids[i - 1];
                    mySeat = current - 1;
                    Console.WriteLine($"Last: {last}- Current: {current}.");
                    Console.WriteLine($"missing seat: {mySeat}. ");
                    break;
                }
            }
 
            return mySeat.ToString();

        }

        private int GetBinarySpacePartitionResult(string input, int max, char upperHalfIndicator)
        {

            
            int result = 0;
            int maxValue = max;
            int minValue = 0;
            int rowCount = max + 1;
            for (int i = 0; i < input.Length; i++)
            {
                rowCount = rowCount / 2;
                if (input[i] == upperHalfIndicator)
                {
                    //take upper half
                    minValue = minValue + rowCount;
                    if (rowCount == 1) result = minValue;
                    
                }
                else
                {
                    //take lower half
                    //keep old min, set new max
                    maxValue = minValue + rowCount - 1;
                    if (rowCount == 1) result = Convert.ToInt32(maxValue);
                }
            }

            return result;
        }
    }
}
