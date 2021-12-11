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
                case "4":
                    RunDay4();
                    break;
                case "5":
                    RunDay5();
                    break;
                case "6":
                    RunDay6();
                    break;
                case "7":
                    RunDay7();
                    break;
                case "8":
                    RunDay8();
                    break;
                case "9":
                    RunDay9();
                    break;
                case "10":
                    RunDay10();
                    break;
                case "11":
                    RunDay11();
                    break;
            }
        }

        private static List<string> ReadFileLineByLine(string path)
        {
            return File.ReadLines(path).ToList();
        }

        #region prevDays
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

        

        private static void RunDay4()
        {
            Console.WriteLine("Part 1 or Part 2?");

            bool isPart1 = true;

            switch (Console.ReadLine())
            {
                case "2":
                    isPart1 = false;
                    break;
            }

            var lines = ReadFileLineByLine(_folder + "Adv4.txt");
            var drawOrder = lines[0].Split(',').ToList().ConvertAll(int.Parse);
            var cards = GetScoreCards(lines);
            var curResults = new List<int>();
            BingoCard finishedCard = null;

            if (isPart1)
                finishedCard = cards[GetFastestCardId(drawOrder, cards, out curResults)];
            else
            {
                var curCards = cards;
                int cardFinishIndex = 0;

                while (curCards.Count > 0)
                {
                    if (curCards.Count == 1)
                        finishedCard = curCards.First();

                    cardFinishIndex = GetFastestCardId(drawOrder, curCards, out curResults);

                    curCards.RemoveAt(cardFinishIndex);
                }
            }
            
            var output = string.Format("Correct Answer = {0}", GetAnswer(finishedCard.Numbers, curResults));

            Console.Write(output);
        }

        private static List<BingoCard> GetScoreCards(List<string> lines)
        {
            var cards = new List<BingoCard>();

            var card = new BingoCard { Numbers = new List<BingoNumber>() };
            int yCounter = 0;

            for (int i = 2; i < lines.Count; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    yCounter = 0;
                    cards.Add(card);
                    card = new BingoCard { Numbers = new List<BingoNumber>() };
                    continue;
                }

                var vals = Regex.Split(lines[i], @"\D+").Where(val => !string.IsNullOrEmpty(val)).ToArray();

                var numbers = Array.ConvertAll(vals, s => int.Parse(s));

                for (int x = 0; x < numbers.Length; x++)
                    card.Numbers.Add(new BingoNumber { PositionX = x, PositionY = yCounter, Value = numbers[x] });

                yCounter++;                
            }

            cards.Add(card);

            return cards;
        }

        private static int GetFastestCardId(List<int> drawOrder, List<BingoCard> cards, out List<int> curResults)
        {
            curResults = new List<int>();

            for (int i = 0; i < drawOrder.Count; i++)
            {
                curResults.Add(drawOrder[i]);

                //No point checking for bingo before 5 results
                if (curResults.Count < 5)
                    continue;

                for (int x = 0; x < cards.Count; x++)
                {
                    if (CheckForBingo(cards[x].Numbers, curResults))
                        return x;                    
                }
            }

            throw new Exception("No bingo result for any card");
        }

        private static bool CheckForBingo(List<BingoNumber> numbers, List<int> results)
        {
            var diagonalResults = new List<BingoNumber>();
            var reverseDiagonalResults = new List<BingoNumber>();

            for (int i = 0; i < 5; i++)
            {
                var xResults = numbers.Where(num => num.PositionX == i).ToList();
                var yResults = numbers.Where(num => num.PositionY == i).ToList();
                diagonalResults.Add(numbers.FirstOrDefault(num => num.PositionX == i && num.PositionY == i));
                reverseDiagonalResults.Add(numbers.FirstOrDefault(num => num.PositionX == 4-i && num.PositionY == 4-i));

                if (CheckResults(xResults, results) || CheckResults(yResults, results))
                    return true;
            }

            if (CheckResults(diagonalResults, results) || CheckResults(reverseDiagonalResults, results))
                return true;

            return false;
        }

        private static bool CheckResults(List<BingoNumber> numbers, List<int> results)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                if (!results.Any(r => r == numbers[i].Value))
                    return false;
            }

            return true;
        }

        private static int GetAnswer(List<BingoNumber> numbers, List<int> results)
        {
            var unusedNumbers = numbers.Select(num => num.Value).Except(results).ToList();
            var total = unusedNumbers.Take(unusedNumbers.Count).Sum();
            return total * results.Last();
        }        

        private static void RunDay5()
        {
            Console.WriteLine("Part 1 or Part 2?");

            bool isPart1 = true;

            switch (Console.ReadLine())
            {
                case "2":
                    isPart1 = false;
                    break;
            }

            List<VentLine> ventLines = new List<VentLine>();

            var lines = ReadFileLineByLine(_folder + "Adv5.txt");

            for (int i = 0; i < lines.Count; i++)
            {
                var vals = Regex.Split(lines[i], @"\D+").Where(val => !string.IsNullOrEmpty(val)).ToArray();
                var numbers = Array.ConvertAll(vals, s => int.Parse(s));

                if (!IsValidCoordinate(numbers[0], numbers[1], numbers[2], numbers[3], !isPart1))
                        continue;

                var ventLine = new VentLine { StartX = numbers[0], StartY = numbers[1], FinishX = numbers[2], FinishY = numbers[3] };

                ventLines.Add(ventLine);
            }

            List<CoordinateCheck> coordinates = new List<CoordinateCheck>();

            for (int i = 0; i < ventLines.Count; i++)
            {
                coordinates = GetCoordinates(ventLines[i], coordinates);
            }

            var dangers = coordinates.Where(co => co.Count >= 2).ToList();

            var output = string.Format("Dangers = {0}\n", dangers.Count());
                
            Console.Write(output);
        }

        private static bool IsValidCoordinate(int startX, int startY, int finishX, int finishY, bool countDiagonals = false)
        {
            if (startX == finishX || startY == finishY)
                return true;

            if (!countDiagonals)
                return false;

            return IsDiagonalCoordinate(startX, startY, finishX, finishY);
        }

        private static bool IsDiagonalCoordinate(int startX, int startY, int finishX, int finishY)
        {
            var xDif = Math.Abs(finishX - startX);
            var yDif = Math.Abs(finishY - startY);

            if (xDif == yDif)
                return true;

            return false;
        }

        private static List<CoordinateCheck> GetCoordinates(VentLine ventLine, List<CoordinateCheck> coords)
        {
            var xInc = ventLine.StartX < ventLine.FinishX ? 1 : -1;
            var yInc = ventLine.StartY < ventLine.FinishY ? 1 : -1;

            xInc = ventLine.StartX == ventLine.FinishX ? 0 : xInc;
            yInc = ventLine.StartY == ventLine.FinishY ? 0 : yInc; 

            int xCounter = ventLine.StartX;
            int yCounter = ventLine.StartY;

            int xGoal = ventLine.FinishX;
            int yGoal = ventLine.FinishY; 

            while (xCounter != xGoal || yCounter != yGoal)
            {
                if (coords.Exists(co => co.X == xCounter && co.Y == yCounter))
                    coords.FirstOrDefault(co => co.X == xCounter && co.Y == yCounter).Count++;
                else
                    coords.Add(new CoordinateCheck { X = xCounter, Y = yCounter, Count = 1 });

                xCounter += xInc; 
                yCounter += yInc; 
            }

            if (coords.Any(co => co.X == xCounter && co.Y == yCounter))
                coords.FirstOrDefault(co => co.X == xCounter && co.Y == yCounter).Count++;
            else
                coords.Add(new CoordinateCheck { X = xCounter, Y = yCounter, Count = 1 });

            return coords;
        }        

        private static void RunDay6()
        {
            Console.WriteLine("Part 1 or Part 2?");

            bool isPart1 = true;

            switch (Console.ReadLine())
            {
                case "2":
                    isPart1 = false;
                    break;
            }

            var lines = ReadFileLineByLine(_folder + "Adv6.txt");

            var fish = Array.ConvertAll(lines[0].Split(','), int.Parse);

            int numberOfDays = 80;

            if (!isPart1)
                numberOfDays = 256;

            var ages = new long[9];

            for (int i = 0; i < fish.Length; i++)
            {
                ages[fish[i]]++;
            }

            for (int i = 1; i <= numberOfDays; i++)
            {
                var newAges = new long[9];

                for (int x = 0; x <ages.Length; x++) {
                    if (x == 8)
                        break;

                    if (x == 0)
                    {
                        newAges[6] = ages[0];
                        newAges[8] = ages[0];
                    }

                    newAges[x] += ages[x + 1];
                }

                ages = newAges;
            }

            Console.Write(string.Format("Total Fish now: {0}", ages.Sum()));
        }

        

        private static void RunDay7()
        {
            Console.WriteLine("Part 1 or Part 2?");

            bool part1 = true;

            switch (Console.ReadLine())
            {
                case "2":
                    part1 = false;
                    break;
            }

            var lines = ReadFileLineByLine(_folder + "Adv7.txt");

            var crabs = Array.ConvertAll(lines[0].Split(','), int.Parse).OrderBy(c => c).ToArray();

            var minVal = crabs.First();
            var maxVal = crabs.Last();
            int? shortestPos = null;
            int? shortestFuel = null;

            for (int i = minVal; i < maxVal; i++)
            {
                int fuelCounter = 0;

                for (int x = 0; x < crabs.Length; x++)
                {
                    if (part1)
                        fuelCounter += Math.Abs(crabs[x] - i);
                    else                    
                        fuelCounter += (Math.Abs(crabs[x] - i) * (Math.Abs(crabs[x] - i) + 1)) / 2;                                 
                }
                                    
                if (shortestFuel == null || fuelCounter < shortestFuel)
                {
                    shortestFuel = fuelCounter;
                    shortestPos = i;
                }                    
            }

            if (shortestPos == null || shortestFuel == null)
                throw new Exception();

            var output = string.Format("Total Fuel: {0}, Most Efficient Position {1}", shortestFuel, shortestPos);
            Console.Write(output);
        }        

        private static void RunDay8()
        {
            Console.WriteLine("Part 1 or Part 2?");

            bool part1 = true;

            switch (Console.ReadLine())
            {
                case "2":
                    part1 = false;
                    break;
            }

            var lines = ReadFileLineByLine(_folder + "Adv8.txt");

            int sumTotal = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                var split = lines[i].Split('|');
                var signalWires = split[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var digitCodes = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var wireNumbers = new List<WireNumber>();

                for (int x = 0; x < signalWires.Length; x++)
                {
                    var wireNumber = new WireNumber { SignalWires = signalWires[x].ToArray() };

                    if (signalWires[x].Length == 2)
                        wireNumber.DisplayNumber = 1;

                    if (signalWires[x].Length == 3)
                        wireNumber.DisplayNumber = 7;

                    if (signalWires[x].Length == 4)
                        wireNumber.DisplayNumber = 4;

                    if (signalWires[x].Length == 7)
                        wireNumber.DisplayNumber = 8;

                    wireNumbers.Add(wireNumber);
                }

                for (int x = 0; x < wireNumbers.Count; x++)
                {
                    var wireNumber = wireNumbers[x];

                    if (wireNumber.DisplayNumber != null)
                        continue;

                    if (wireNumber.SignalWires.Length == 5)
                    {
                        var oneWires = wireNumbers.First(wn => wn.DisplayNumber == 1).SignalWires;
                        var count = ArrayMatchingCount(wireNumber.SignalWires, oneWires);

                        if (count == oneWires.Count())
                        {
                            wireNumber.DisplayNumber = 3;
                            continue;
                        }
                            
                        var fourWires = wireNumbers.First(wn => wn.DisplayNumber == 4).SignalWires;
                        count = ArrayMatchingCount(wireNumber.SignalWires, fourWires);

                        if (count == 2)
                            wireNumber.DisplayNumber = 2;
                        else if (count == 3)
                            wireNumber.DisplayNumber = 5;
                    }
                    else if (wireNumber.SignalWires.Length == 6)
                    {
                        var sevenWires = wireNumbers.First(wn => wn.DisplayNumber == 7).SignalWires;
                        var count = ArrayMatchingCount(wireNumber.SignalWires, sevenWires);

                        if (count < sevenWires.Count())
                        {
                            wireNumber.DisplayNumber = 6;
                            continue;
                        }

                        var fourWires = wireNumbers.First(wn => wn.DisplayNumber == 4).SignalWires;
                        count = ArrayMatchingCount(wireNumber.SignalWires, fourWires);

                        if (count == fourWires.Count())
                        {
                            wireNumber.DisplayNumber = 9;
                            continue;
                        }

                        wireNumber.DisplayNumber = 0;
                    }
                }

                var numberString = "";

                for (int x = 0; x < digitCodes.Length; x++)
                {
                    var arr = digitCodes[x].ToArray();

                    for (int y = 0; y < wireNumbers.Count; y++)
                    {
                        if (MatchingArray(arr, wireNumbers[y].SignalWires))
                            numberString += wireNumbers[y].DisplayNumber.ToString();
                    }
                }

                sumTotal += int.Parse(numberString);
            }

            Console.Write("Sum Total: " + sumTotal);
        }

        private static int ArrayMatchingCount<T>(T[] arr1, T[] arr2) 
        {
            int counter = 0;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr2.Contains(arr1[i]))
                    counter++;
            }

            return counter;
        }

        private static bool MatchingArray<T>(T[] arr1, T[] arr2)
        {
            if (arr1.Length != arr2.Length)
                return false;

            for (int i = 0; i < arr1.Length; i++)
            {
                if (!arr2.Contains(arr1[i]))
                    return false;
            }

            return true;
        }

        

        private static void RunDay9()
        {
            Console.WriteLine("Part 1 or Part 2?");

            bool part1 = true;

            switch (Console.ReadLine())
            {
                case "2":
                    part1 = false;
                    break;
            }

            var map = ReadFileLineByLine(_folder + "Adv9.txt");

            var maxY = map.Count - 1;
            var maxX = map.First().Length - 1;
            var lowestValues = new List<int>();
            var basinSizes = new List<int>();

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    var curDigit = (int)Char.GetNumericValue(map[y][x]);
                    if (!IsValueLowest(map, curDigit, y, x, maxX, maxY))
                        continue;
                    
                    lowestValues.Add(curDigit + 1);
                    
                    if (part1)
                        continue;

                    basinSizes.Add(GetBasinSize(map, x, y, maxX, maxY));
                }
            }

            var outPut = "Lowest Value Sum: " + lowestValues.Sum();

            if (!part1)
            {
                for (int i = 0; i < basinSizes.Count; i++)
                {
                    outPut += "\nBasin Size: " + basinSizes[i];
                }

                outPut += "\nAnswer: " + basinSizes.OrderByDescending(bs => bs).Take(3).Aggregate(1, (a, b) => a * b);
            }                

            Console.Write(outPut);
        }

        private static bool IsValueLowest(List<string> map, int curVal, int curY, int curX, int maxX, int maxY)
        {
            var startX = curX == 0 ? 0 : curX - 1;
            var endX = curX == maxX ? maxX : curX + 1;

            for (int x = startX; x <= endX; x++)
            {
                if (x == curX)
                    continue;

                var curDigit = (int)Char.GetNumericValue(map[curY][x]);

                if (curDigit <= curVal)
                    return false;
            }
            
            var startY = curY == 0 ? 0 : curY - 1;
            var endY = curY == maxY ? maxY : curY + 1;

            for (int y = startY; y <= endY; y++)
            {   
                if (y == curY)
                    continue;

                var curDigit = (int)Char.GetNumericValue(map[y][curX]);

                if (curDigit <= curVal)
                    return false;
            }

            return true;
        }

        private static int GetBasinSize(List<string> map, int initialX, int initialY, int maxX, int maxY)
        {
            List<int[]> coordsToCheck = new List<int[]>();
            List<int[]> coordsChecked = new List<int[]>();

            coordsToCheck.Add(new int[] { initialX, initialY });

            int basinSize = 1;

            while(coordsToCheck.Count > 0)
            {
                var curX  = coordsToCheck.FirstOrDefault()[0];
                var curY = coordsToCheck.FirstOrDefault()[1];

                var startX = curX == 0 ? 0 : curX - 1;
                var endX = curX == maxX ? maxX : curX + 1;

                for (int x = startX; x <= endX; x++)
                {
                    if (x == curX || CoordsExist(x, curY, coordsChecked) || CoordsExist(x, curY, coordsToCheck))
                        continue;

                    var curDigit = (int)Char.GetNumericValue(map[curY][x]);

                    if (curDigit == 9)
                        continue;

                    coordsToCheck.Add(new int[] { x, curY });
                    basinSize++;
                }

                var startY = curY == 0 ? 0 : curY - 1;
                var endY = curY == maxY ? maxY : curY + 1;

                for (int y = startY; y <= endY; y++)
                {
                    if (y == curY || CoordsExist(curX, y, coordsChecked) || CoordsExist(curX, y, coordsToCheck))
                        continue;

                    var curDigit = (int)Char.GetNumericValue(map[y][curX]);

                    if (curDigit == 9)
                        continue;

                    coordsToCheck.Add(new int[] { curX, y });
                    basinSize++;
                }

                coordsChecked.Add(coordsToCheck.First());
                coordsToCheck.Remove(coordsToCheck.First());
            }

            return basinSize;
        }

        private static bool CoordsExist(int x, int y, List<int[]> coordsList)
        {
            for (int i = 0; i < coordsList.Count; i++)
            {
                if (coordsList[i][0] == x && coordsList[i][1] == y)
                    return true;
            }

            return false;
        }

        

        private static void RunDay10()
        {
            Console.WriteLine("Part 1 or Part 2?");

            bool part1 = true;

            switch (Console.ReadLine())
            {
                case "2":
                    part1 = false;
                    break;
            }

            var lines = ReadFileLineByLine(_folder + "Adv10.txt");
            List<char> illegalChars = new List<char>();
            List<long> incompleteScores = new List<long>();

            for (int i = 0; i < lines.Count; i++)
            {
                var illegalChar = GetIllegalCharacterFromLine(lines[i]);

                if (illegalChar != null)
                    illegalChars.Add(illegalChar.Value);

                if (illegalChar == null && !part1)
                    incompleteScores.Add(GetIncompleteScore(lines[i]));
            }

            var output = "Score: " + GetAnswerFromIllegalChars(illegalChars);

            if (!part1)
                output += "\nIncomplete Socre: " + incompleteScores.OrderBy(i => i).ToList()[incompleteScores.Count / 2];
            
            Console.Write(output);
        }

        private static char? GetIllegalCharacterFromLine(string line)
        {
            var remainingLine = line;            
            var matchingPairs = GetMatchingPairs();

            while (remainingLine.Length > 0)
            {
                var openingChar = remainingLine[0];
                var openingIndex = 0;
                var expectedClosingChar = matchingPairs.First(mp => mp.Key == openingChar).Value;
                bool matchFound = false;

                for (int i = 1; i <remainingLine.Length; i++)
                {
                    if (remainingLine[i] == expectedClosingChar)
                    {
                        remainingLine = remainingLine.Remove(i,1);
                        remainingLine = remainingLine.Remove(openingIndex,1);
                        matchFound = true;
                        break;
                    }
                    else if (matchingPairs.Any(mp => mp.Key == remainingLine[i]))
                    {
                        openingChar = remainingLine[i];
                        openingIndex = i;
                        expectedClosingChar = matchingPairs.First(mp => mp.Key == openingChar).Value;
                    }
                    else if (matchingPairs.Any(mp => mp.Value == remainingLine[i]))
                    {
                        return remainingLine[i]; //Corrupt char
                    }                    
                }

                if (!matchFound)
                    return null; //Incomplete line
            }

            return null; // Non-corrupt line
        }

        private static long GetIncompleteScore(string line)
        {
            var remainingLine = line;
            var matchingPairs = GetMatchingPairs();
            long incompleteScore = 0;

            while (remainingLine.Length > 0)
            {
                var openingChar = remainingLine[0];
                var openingIndex = 0;
                var expectedClosingChar = matchingPairs.First(mp => mp.Key == openingChar).Value;
                bool matchFound = false;

                for (int i = 1; i < remainingLine.Length; i++)
                {
                    if (remainingLine[i] == expectedClosingChar)
                    {
                        remainingLine = remainingLine.Remove(i, 1);
                        remainingLine = remainingLine.Remove(openingIndex, 1);
                        matchFound = true;
                        break;
                    }
                    else if (matchingPairs.Any(mp => mp.Key == remainingLine[i]))
                    {
                        openingChar = remainingLine[i];
                        openingIndex = i;
                        expectedClosingChar = matchingPairs.First(mp => mp.Key == openingChar).Value;
                    }
                    else if (matchingPairs.Any(mp => mp.Value == remainingLine[i]))
                    {
                        return 0; //Corrupt char
                    }
                }

                if (!matchFound)
                {
                    incompleteScore *= 5;
                    incompleteScore += GetIncompleteValue(expectedClosingChar);
                    remainingLine = remainingLine.Remove(openingIndex, 1);
                }
                    
            }

            return incompleteScore;
        }

        private static int GetAnswerFromIllegalChars(List<char> chars)
        {
            int score = 0;

            for (int i = 0; i < chars.Count; i++)
            {
                switch (chars[i])
                {
                    case ')':
                        score += 3;
                        break;
                    case ']':
                        score += 57;
                        break;
                    case '}':
                        score += 1197;
                        break;
                    case '>':
                        score += 25137;
                        break;
                    default:
                        throw new Exception("Char doesn't exist in list" + chars[i].ToString());
                }
            }

            return score;
        }

        private static int GetIncompleteValue(char incompleteChar)
        {
            switch (incompleteChar)
            {
                case ')':
                    return 1;
                case ']':
                    return 2;
                case '}':
                    return 3;
                case '>':
                    return 4;
            }

            throw new Exception("Returning char that isn't in list: " + incompleteChar.ToString());
        }

        private static Dictionary<char,char> GetMatchingPairs()
        {
            var matchingPairs = new Dictionary<char, char>();
            matchingPairs.Add('{', '}');
            matchingPairs.Add('[', ']');
            matchingPairs.Add('(', ')');
            matchingPairs.Add('<', '>');

            return matchingPairs;
        }

        #endregion

        private static void RunDay11()
        {
            Console.WriteLine("Part 1 or Part 2?");

            bool part1 = true;

            switch (Console.ReadLine())
            {
                case "2":
                    part1 = false;
                    break;
            }

            var lines = ReadFileLineByLine(_folder + "Adv11.txt");

            List<Octopus> octopi = new List<Octopus>();

            for (int y = 0; y < lines.Count(); y++)
                for (int x = 0; x < lines[y].Length; x++)                
                    octopi.Add(new Octopus { X = x, Y = y, EnergyLevel = int.Parse(lines[y][x].ToString()), HasFlashed = false });                

            var output = "";

            if (part1)
                output = "Flashes: " + GetFlashCount(octopi, 100);            
            else            
                output += "\nSynch Step: " + GetSynchStep(octopi);
            
            
            Console.Write(output);
        }

        private static int GetFlashCount(List<Octopus> octopi, int numberOfSteps)
        {
            int flashCount = 0;

            for (int i = 0; i < numberOfSteps; i++)
            {
                int newFlashes;
                octopi = CommitStep(octopi, out newFlashes);
                flashCount += newFlashes;
            }

            return flashCount;
        }

        private static int GetSynchStep(List<Octopus> octopi)
        {
            int iteration = 0;

            bool synchFound = false;

            while (!synchFound)
            {
                iteration++;

                int newFlashes;
                octopi = CommitStep(octopi, out newFlashes);

                if (newFlashes == octopi.Count)
                    return iteration;
            }

            return iteration;
        }

        private static List<Octopus> CommitStep(List<Octopus> octopi, out int flashes)
        {
            List<Octopus> remaining = new List<Octopus>(octopi);

            var maxX = octopi.OrderByDescending(o => o.X).First().X;
            var maxY = octopi.OrderByDescending(o => o.Y).First().Y;

            while (remaining.Count > 0)
            {
                var flashers = new List<Octopus>();

                var masterResult = octopi.First(o => o.X == remaining[0].X && o.Y == remaining[0].Y);
                masterResult.EnergyLevel++;

                if (masterResult.EnergyLevel > 9 && !masterResult.HasFlashed)
                {
                    flashers.Add(masterResult);
                    masterResult.HasFlashed = true;
                }

                remaining.Remove(masterResult);

                for (int i = 0; i < flashers.Count(); i++)                
                    remaining.AddRange(GetAdjacentOctopi(octopi, flashers[i].X, flashers[i].Y, maxX, maxY));                
            }

            flashes = 0;

            for (int i = 0; i < octopi.Count(); i++)
            {
                if (octopi[i].HasFlashed)
                {
                    octopi[i].EnergyLevel = 0;
                    octopi[i].HasFlashed = false;
                    flashes++;
                }                    
            }

            return octopi;
        }  
        
        private static List<Octopus> GetAdjacentOctopi(List<Octopus> grid, int curX, int curY, int maxX, int maxY)
        {
            var startX = curX == 0 ? 0 : curX - 1;
            var endX = curX == maxX ? maxX : curX + 1;

            var startY = curY == 0 ? 0 : curY - 1;
            var endY = curY == maxY ? maxY : curY + 1;

            var results = new List<Octopus>();

            for (int y = startY; y <= endY; y++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    if (x == curX && y == curY)
                        continue;

                    results.Add(grid.First(r => r.X == x && r.Y == y));
                }
            }

            return results;
        }
    }

    public class VentLine
    {
        public int StartX;
        public int StartY;
        public int FinishX;
        public int FinishY;
    }

    public class CoordinateCheck
    {
        public int X;
        public int Y;
        public int Count;
    }

    public class BingoCard
    {
        public List<BingoNumber> Numbers;
    }

    public class BingoNumber
    {
        public int PositionX;
        public int PositionY;
        public int Value;
    }

    public class WireNumber
    {
        public int? DisplayNumber;
        public char[] SignalWires;
    }

    public class Octopus
    {
        public int X;
        public int Y;
        public int EnergyLevel;
        public bool HasFlashed;
    }
}
