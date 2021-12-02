using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    class Program
    {
        const string _folder = @"C:\Users\anrel\Documents\Advent of Code\AdventOfCode2021\AdventOfCode2021\";

        static void Main(string[] args)
        {
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

        private static List<int> ReadFileLineByLine(string path)
        {
            var entries = new System.Collections.Generic.List<int>();

            // Read the file and display it line by line.  
            foreach (string line in System.IO.File.ReadLines(path))
            {
                entries.Add(int.Parse(line));
            }

            return entries;
        }

        private static List<string> ReadFileLineByLineString(string path)
        {
            var entries = new System.Collections.Generic.List<string>();

            // Read the file and display it line by line.  
            foreach (string line in System.IO.File.ReadLines(path))
            {
                entries.Add(line);
            }

            return entries;
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

            var lines = ReadFileLineByLine(_folder + "Adv1.txt");

            var counter = 0;

            int prevRecord = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                if (isPart1)
                {
                    if (prevRecord != 0 && lines[i] > prevRecord)
                        counter++;

                    prevRecord = lines[i];
                }
                else
                {
                    if (i == lines.Count - 2)
                        break;

                    var total = lines[i] + lines[i + 1] + lines[i + 2];

                    if (prevRecord != 0 && total > prevRecord)
                        counter++;

                    prevRecord = total;
                }                
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

            var lines = ReadFileLineByLineString(_folder + "Adv2.txt");

            int depth = 0;
            int position = 0;
            int aim = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                var numeric = Regex.Match(lines[i], @"\d+").Value;
                var change = int.Parse(numeric);

                if (lines[i].StartsWith("forward"))
                {
                    position += change;
                    if (!isPart1)
                        depth += aim * change;
                }
                else if (lines[i].StartsWith("down"))
                    aim += change;
                else if (lines[i].StartsWith("up"))
                    aim -= change;                
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
