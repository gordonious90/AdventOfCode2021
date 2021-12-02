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
    }
}
