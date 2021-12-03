using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    class Program
    {
        private static string _folder;

        static void Main(string[] args)
        {
            _folder = System.IO.Path.GetFullPath(@"..\..\..\");

            Console.WriteLine("Welcome to Gordy's Advent of Code 2021!");
            Console.WriteLine("==================================");
            Console.WriteLine("Which day should I run?");

            switch (Console.ReadLine())
            {
                case "1":
                    RunDay1();
                    break;
                case "2":
                    RunDay2();
                    break;
                case "3":
                    RunDay3();
                    break;
            }
        }

        private static List<string> ReadFileLineByLine(string path)
        {
            return File.ReadLines(path).ToList();
        }

        private static void RunDay1()
        {
            Console.WriteLine("Part 1 or Part 2?");

            bool isPart1 = true;

            switch (Console.ReadLine())
            {
                case "2": 
                    isPart1 = false;
                    break;
            }

            var lines = ReadFileLineByLine(_folder + "Adv1.txt").ConvertAll(int.Parse);

            var counter = 0;

            int prevRecord = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                if (i == lines.Count - 2 && !isPart1)
                    break;

                int total = isPart1 ? lines[i] : lines[i] + lines[i + 1] + lines[i + 2];

                if (prevRecord != 0 && total > prevRecord)
                    counter++;

                prevRecord = total;                            
            }

            Console.Write("Result: There are " + counter + " measurements larger than previous.");

            Console.Write("Press any key to close the Calculator console app...");
            Console.ReadKey();
        }

        private static void RunDay2()
        {
            Console.WriteLine("Part 1 or Part 2?");

            bool isPart1 = true;

            switch (Console.ReadLine())
            {
                case "2":
                    isPart1 = false;
                    break;
            }

            var lines = ReadFileLineByLine(_folder + "Adv2.txt");

            int depth = 0;
            int position = 0;
            int aim = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                var numeric = int.Parse(Regex.Match(lines[i], @"\d+").Value);

                if (lines[i].StartsWith("forward"))
                {
                    position += numeric;
                    if (!isPart1)
                        depth += aim * numeric;
                }
                else if (lines[i].StartsWith("down"))
                    aim += numeric;
                else if (lines[i].StartsWith("up"))
                    aim -= numeric;                
            }

            if (isPart1)
                depth = aim;

            var result = position * depth;
            var outPut = "Position: " + position + " , Depth: " + depth + ". Result = " + result;

            if (!isPart1)
                outPut += " (Aim : " + aim + ")";

            Console.Write(outPut);                
        }

        private static void RunDay3()
        {
            Console.WriteLine("Part 1 or Part 2?");

            switch (Console.ReadLine())
            {
                case "2":
                    RunDay3Part2();
                    return;
            }

            var lines = ReadFileLineByLine(_folder + "Adv3.txt");
            var maxLength = lines.FirstOrDefault().Length;
            string gammaRateBinary = "";
            string epsilonRateBinary = "";

            for (int x = 0; x < maxLength; x++)
            {
                gammaRateBinary += GetCommonBit(lines, x);
                epsilonRateBinary += GetCommonBit(lines, x, false);
            }
            
            var gammaRate = Convert.ToInt32(gammaRateBinary, 2);
            var epsilonRate = Convert.ToInt32(epsilonRateBinary, 2);

            var output = string.Format("Gamma Rate: {0} Dec: {1}, Epsilon Rate: {2} Dec: {3}, Result: {4}", 
                                                gammaRateBinary, gammaRate, epsilonRateBinary, epsilonRate, gammaRate * epsilonRate);

            Console.Write(output);
        }

        private static void RunDay3Part2()
        {
            List<string> lines = ReadFileLineByLine(_folder + "Adv3.txt");

            var oxygenRatingBinary = GetReducingListCommonBit(lines);
            var coScrubberRatingBinary = GetReducingListCommonBit(lines, false);

            var oxygenGenRating = Convert.ToInt32(oxygenRatingBinary, 2);
            var coScrubberRating = Convert.ToInt32(coScrubberRatingBinary, 2);

            var output = string.Format("Oxygen Generator Rating: {0} Dec: {1}, CO2 Scrubber Rating: {2} Dec: {3}, Result = {4}",
                                                oxygenRatingBinary, oxygenGenRating, coScrubberRatingBinary, coScrubberRating, oxygenGenRating * coScrubberRating);

            Console.Write(output);
        }

        private static string GetCommonBit(List<string>lines, int stringIndex, bool mostCommon = true)
        {
            int zeroBits = 0;
            int oneBits = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i][stringIndex];

                if (line.Equals('0'))
                    zeroBits++;
                else if (line.Equals('1'))
                    oneBits++;
            }

            if (zeroBits > oneBits)
                return mostCommon ? "0" : "1";
            if (oneBits > zeroBits)
                return mostCommon ? "1" : "0";

            //Equally common
            return mostCommon ? "1" : "0";
        }

        private static string GetReducingListCommonBit(List<String> lines, bool mostCommon = true)
        {
            string candidate = "";
            int currentStringIndex = 0;
            List<string> candidates = lines;
            List<string> newCandidates = new List<string>();

            while (newCandidates.Count != 1)
            {
                newCandidates = new List<string>();

                candidate += GetCommonBit(candidates, currentStringIndex, mostCommon);

                for (int i = 0; i < candidates.Count; i++)
                {
                    if (candidates[i].StartsWith(candidate))
                        newCandidates.Add(candidates[i]);
                }

                candidates = newCandidates;

                currentStringIndex++;
            }

            return newCandidates.First();
        }
    }
}
