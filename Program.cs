using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    class Program
    {
        private static string _folder;
        private static List<Stack<Node>> _possiblePaths;
        private static Dictionary<CaveVertex, CaveVertex> outerParents;

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
                case "12":
                    RunDay12();
                    break;
                case "13":
                    RunDay13();
                    break;
                case "14":
                    RunDay14();
                    break;
                case "15":
                    RunDay15();
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

        

        private static void RunDay12()
        {
            Console.WriteLine("Part 1 or Part 2?");

            bool part1 = true;

            switch (Console.ReadLine())
            {
                case "2":
                    part1 = false;
                    break;
            }

            var lines = ReadFileLineByLine(_folder + "Adv12.txt");

            var nodes = GetCaveNodes(lines);

            var routes = GetCaveRoutes(nodes, part1);

            Console.Write("Path Count = " + routes.Count());
        }
     
        private static Dictionary<Node, List<Node>> GetCaveNodes(List<string> lines)
        {
            var nodes = new Dictionary<Node, List<Node>>();

            //First generate all cave nodes
            for (int i = 0; i < lines.Count(); i++)
            {
                var entry = lines[i].Split('-');
                
                for (int x = 0; x < entry.Length; x++)
                {
                    if (!nodes.Keys.Any(n => n.Identifier == entry[x]))
                    {
                        var node = new Node
                        {
                            Identifier = entry[x]
                        };

                        node.Type = CaveType.Small;

                        if (entry[x] == "start")
                            node.Type = CaveType.Start;
                        else if (entry[x] == "end")
                            node.Type = CaveType.End;
                        else if (Char.IsUpper(entry[x][0]))
                            node.Type = CaveType.Big;

                        nodes.Add(node, new List<Node>());
                    }
                }                
            }

            for (int i = 0; i < lines.Count(); i++)
            {
                var entry = lines[i].Split('-');

                var entry1 = nodes.First(n => n.Key.Identifier == entry[0]);
                var entry2 = nodes.First(n => n.Key.Identifier == entry[1]);

                if (!entry1.Value.Exists(n => n == entry2.Key))
                    entry1.Value.Add(entry2.Key);

                if (!entry2.Value.Exists(n => n == entry1.Key))
                    entry2.Value.Add(entry1.Key);
            }

            return nodes;
        }

        private static List<Stack<Node>> GetCaveRoutes(Dictionary<Node, List<Node>> allNodes, bool part1)
        {
            List<Stack<Node>> allRoutes = new List<Stack<Node>>();

            var possiblePaths = new List<Stack<Node>>();
            var curStack = new Stack<Node>();

            curStack.Push(allNodes.First(n => n.Key.Type == CaveType.Start).Key);

            if (part1)
                GetAllPaths(allNodes, curStack, allNodes.First(n => n.Key.Type == CaveType.End).Key, possiblePaths);
            else
                GetAllPathsPart2(allNodes, curStack, allNodes.First(n => n.Key.Type == CaveType.End).Key, possiblePaths);

            return possiblePaths;
        }

        private static void GetAllPaths(Dictionary<Node, List<Node>> allNodes, Stack<Node> visited, Node endNode, List<Stack<Node>> possiblePaths)
        {
            var nodes = allNodes.First(k => k.Key == visited.Peek()).Value;

            for (int i = 0; i < nodes.Count(); i++)
            {
                if ((visited.Contains(nodes[i]) && nodes[i].Type == CaveType.Small) || nodes[i].Type == CaveType.Start)
                    continue;

                if (nodes[i] == endNode)
                {
                    visited.Push(nodes[i]);
                    possiblePaths.Add(visited);
                    PrintPath(visited);
                    visited.Pop();
                    break;
                }
            }

            for (int i = 0; i < nodes.Count(); i++)
            {
                if ((visited.Contains(nodes[i]) && nodes[i].Type == CaveType.Small) || nodes[i].Type == CaveType.Start || nodes[i] == endNode)
                    continue;

                visited.Push(nodes[i]);
                GetAllPaths(allNodes, visited, endNode, possiblePaths);
                visited.Pop();
            }
        }

        private static void GetAllPathsPart2(Dictionary<Node, List<Node>> allNodes, Stack<Node> visited, Node endNode, List<Stack<Node>> possiblePaths)
        {
            var nodes = allNodes.First(k => k.Key == visited.Peek()).Value;

            for (int i = 0; i < nodes.Count(); i++)
            {
                var nodeMatch = visited.Where(n => n == nodes[i]);
                var nodeMatching = visited.GroupBy(x => x).Where(n => n.Key.Type == CaveType.Small && n.Count() > 1).Select(y => y.Key).ToList();
                if (nodes[i].Type == CaveType.Small && nodeMatch.Count() == 2)
                    continue;
                else if (nodes[i].Type == CaveType.Small && nodeMatch.Count() == 1 && nodeMatching.Count > 0)
                    continue;

                if (nodes[i].Type == CaveType.Start)
                    continue;

                if (nodes[i] == endNode)
                {
                    visited.Push(nodes[i]);
                    possiblePaths.Add(visited);
                    PrintPath(visited);
                    visited.Pop();
                    break;
                }
            }

            for (int i = 0; i < nodes.Count(); i++)
            {
                var nodeMatch = visited.Where(n => n == nodes[i]);
                var nodeMatching = visited.GroupBy(x => x).Where(n => n.Key.Type == CaveType.Small && n.Count() > 1).Select(y => y.Key).ToList();
                if (nodes[i].Type == CaveType.Small && nodeMatch.Count() == 2)
                    continue;
                else if (nodes[i].Type == CaveType.Small && nodeMatch.Count() == 1 && nodeMatching.Count > 0)
                    continue;

                if (nodes[i].Type == CaveType.Start || nodes[i].Type == CaveType.End)
                    continue;

                visited.Push(nodes[i]);
                GetAllPathsPart2(allNodes, visited, endNode, possiblePaths);
                visited.Pop();
            }
        }

        private static void PrintPath(Stack<Node> visited){
            var output = "\n";

            for (int i = visited.Count() - 1; i > -1; i--)
                output += visited.ElementAt(i).Identifier + " -> ";

            Console.Write(output);
        }        

        private static void RunDay13()
        {
            Console.WriteLine("Part 1 or Part 2?");

            bool part1 = true;

            switch (Console.ReadLine())
            {
                case "2":
                    part1 = false;
                    break;
            }

            var lines = ReadFileLineByLine(_folder + "Adv13.txt");

            var foldInstructions = new List<string>();
            var locations = new List<Tuple<int, int>>();

            bool secondPart = false;

            for (int i = 0; i < lines.Count(); i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    secondPart = true;
                    continue;
                }                    

                if (secondPart)
                    foldInstructions.Add(lines[i]);
                else
                    locations.Add(new Tuple<int, int>(int.Parse(lines[i].Split(',')[0]), int.Parse(lines[i].Split(',')[1])));
            }

            int instructionCount = 1;

            if (!part1)
                instructionCount = foldInstructions.Count();

            for(int i = 0; i < instructionCount; i++)            
                locations = MakeFold(locations, foldInstructions[i]);            

            if (part1)
                Console.Write("Dots remaining: " + locations.Count());
            else            
                PrintGrid(locations);                            
        }

        private static List<Tuple<int, int>> MakeFold(List<Tuple<int,int>> locations, string instruction)
        {
            bool xFold = instruction.Contains('x');
            var val = int.Parse(Regex.Split(instruction, @"\D+")[1]);
            var newLocations = locations;

            if (xFold)
                locations.RemoveAll(l => l.Item1 == val);
            else
                locations.RemoveAll(l => l.Item2 == val);

            var movingCandidates = new List<Tuple<int, int>>();

            if (xFold)
                movingCandidates.AddRange(locations.Where(l => l.Item1 > val));
            else
                movingCandidates.AddRange(locations.Where(l => l.Item2 > val));

            for(int i = 0; i < movingCandidates.Count(); i++)
            {                
                var x = xFold ? 0 : movingCandidates[i].Item1;
                var y = xFold ? movingCandidates[i].Item2 : 0;

                if (xFold)                
                    x = val - (movingCandidates[i].Item1 - val);                
                else                
                    y = val - (movingCandidates[i].Item2 - val);

                locations.Remove(movingCandidates[i]);
                locations.Add(new Tuple<int, int>(x, y));
            }

            return locations.Distinct().ToList();
        }

        private static void PrintGrid(List<Tuple<int,int>> grid)
        {
            var maxX = grid.OrderByDescending(i => i.Item1).First().Item1;
            var maxY = grid.OrderByDescending(i => i.Item2).First().Item2;

            var output = "";

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                    output += grid.Any(i => i.Item1 == x && i.Item2 == y) ? "#" : ".";
                output += "\n";
            }                                  

            Console.Write(output);
        }
        
        private static void RunDay14()
        {
            Console.WriteLine("Part 1 or Part 2?");

            bool part1 = true;

            switch (Console.ReadLine())
            {
                case "2":
                    part1 = false;
                    break;
            }

            var lines = ReadFileLineByLine(_folder + "Adv14.txt");

            Dictionary<string, string> pairInsertions = new Dictionary<string, string>();

            for (int i = 2; i < lines.Count(); i++)
            {
                var sections = lines[i].Split(" -> ");
                pairInsertions.Add(sections[0], sections[0][0].ToString() + sections[1] + sections[0][1].ToString());
            }

            int stepCount = 10;

            if (!part1)
                stepCount = 40;

            Dictionary<string, long> pairCount = GetInitialPairCount(pairInsertions, lines[0]);

            for (int i = 0; i < stepCount; i++)
                pairCount = RunPolymerStep(pairCount, pairInsertions);

            var charCount = GetCharCount(pairCount, lines[0][lines[0].Length - 1]);

            foreach (var kvp in charCount)            
                Console.Write(kvp.Key + ": " + kvp.Value + "\n");            

            Console.Write("Answer: " + (charCount[charCount.Keys.Last()] - charCount[charCount.Keys.First()]).ToString());
        }

        private static Dictionary<string, long> GetInitialPairCount(Dictionary<string, string> list, string line)
        {
            var initialPairs = new Dictionary<string, long>();

            foreach (var kvp in list)
                initialPairs.Add(kvp.Key, 0);

            for (int i = 0; i < line.Length - 1; i++)
                initialPairs[line[i].ToString() + line[i + 1].ToString()]++;            

            return initialPairs;
        }

        private static Dictionary<string, long> RunPolymerStep(Dictionary<string, long> pairCount, Dictionary<string,string> pairInsertions)
        {
            var origCount = new Dictionary<string,long>(pairCount);

            foreach(var kvp in origCount)
            {
                if (kvp.Value == 0)
                    continue;

                var newPair1 = pairInsertions[kvp.Key].Substring(0,2);
                var newPair2 = pairInsertions[kvp.Key].Substring(1,2);

                pairCount[newPair1] += kvp.Value;
                pairCount[newPair2] += kvp.Value;
                pairCount[kvp.Key] -= kvp.Value;
            }


            return pairCount;
        }

        private static Dictionary<char, long> GetCharCount(Dictionary<string,long> list, char lastChar)
        {
            var charCount = new Dictionary<char, long>();

            foreach (var kvp in list)            
                if (charCount.ContainsKey(kvp.Key[0]))
                    charCount[kvp.Key[0]] += kvp.Value;
                else
                    charCount.Add(kvp.Key[0], kvp.Value);

            charCount[lastChar]++;

            return charCount.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        #endregion

        private static void RunDay15()
        {
            Console.WriteLine("Part 1 or Part 2?");

            bool part1 = true;

            switch (Console.ReadLine())
            {
                case "2":
                    part1 = false;
                    break;
            }

            var lines = ReadFileLineByLine(_folder + "Adv15.txt");
            
            int repeats = part1 ? 1 : 5;
            var maxX = lines[0].Length * repeats - 1;
            var maxY = lines.Count() * repeats - 1;

            var graph = GetCaveMap(lines, repeats);
            var startNode = graph.AdjList.First(n => n.Key.X == 0 && n.Key.Y == 0);
            var endNode = graph.AdjList.First(n => n.Key.X == maxX && n.Key.Y == maxY);

            PrintShortestPath(graph, startNode.Key, endNode.Key);       
        }

        private static CaveGraph GetCaveMap(List<string> lines, int repeat)
        {
            var graph = new CaveGraph();

            var vertex = new List<CaveVertex>();

            int counter = 0;

            var maxY = lines.Count;
            var maxX = lines[0].Count();                          

            for (int yMaster = 0; yMaster < repeat; yMaster++)
            {
                for (int xMaster = 0; xMaster < repeat; xMaster++)
                {
                    for (int y = 0; y < lines.Count(); y++)
                    {
                        for (int x = 0; x < lines[y].Length; x++)
                        {
                            var weight = int.Parse(lines[y][x].ToString()) + yMaster + xMaster;

                            while (weight > 9)
                                weight = weight - 9;

                            vertex.Add(new CaveVertex(counter, x + (maxX * xMaster), y + (maxY * yMaster), weight));
                            counter++;
                        }
                    }
                }
            }

            if (repeat > 1)
            {
                maxX = maxX * repeat - 1;
                maxY = maxY * repeat - 1;
            }

            foreach (var vert in vertex)
            {
                var startX = vert.X > 0 ? vert.X - 1 : 0;
                var endX = vert.X < maxX ? vert.X + 1 : maxX;
                var startY = vert.Y > 0 ? vert.Y - 1 : 0;
                var endY = vert.Y < maxY ? vert.Y + 1 : maxY;

                for (int y = startY; y <= endY; y++)
                {
                    for (int x = startX; x <= endX; x++)
                    {
                        if (x == vert.X && y == vert.Y)
                            continue;

                        if (x != vert.X && y != vert.Y)
                            continue;

                        var match = vertex.First(v => v.X == x && v.Y == y);
                        graph.AddEdgeDirected(vert, match, match.Weight + vert.Weight);
                    }
                }                
            }

            return graph;
        }

        private static Dictionary<CaveVertex, int> DijkstraGraph(CaveGraph graph, CaveVertex source)
        {
            var map = new Dictionary<CaveVertex, CaveNode>();
            // set list capacity to number of vertices to keep Add() at O(1)
            // For details see:
            // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.add?view=netframework-4.8#remarks
            var pq = new List<CaveNode>(graph.AdjList.Keys.Count);
            var weights = new Dictionary<CaveVertex, int>();
            var parents = new Dictionary<CaveVertex, CaveVertex>();
            var dq = new HashSet<CaveVertex>();

            foreach (var v in graph.AdjList.Keys)
            {
                var node = new CaveNode(v, 0);
                if (v.Key == source.Key)
                {
                    map.Add(v, node);
                    // O(1) unless capacity is exceeded
                    pq.Add(node);
                }
                else
                {
                    node.Priority = int.MaxValue;
                    map.Add(v, node);
                    // O(1) unless capacity is exceeded
                    pq.Add(node);
                }
            }

            weights.Add(source, 0);
            parents.Add(source, null);

            while (pq.Count > 0)
            {
                // sort by priority. O(n log n)
                pq.Sort((a, b) => a.Priority.CompareTo(b.Priority));

                var temp = pq[0];
                // O(n)
                pq.RemoveAt(0);
                var current = temp.V;
                var weight = temp.Priority;

                dq.Add(current);

                // update shortest dist of current vertex from source
                if (!weights.TryAdd(current, weight))
                {
                    weights[current] = weight;
                }

                foreach (var adjEdge in graph.AdjList[current])
                {
                    var adj = adjEdge.Source.Key == current.Key
                        ? adjEdge.Destination
                        : adjEdge.Source;

                    // skip already dequeued vertices
                    if (dq.Contains(adj))
                    {
                        continue;
                    }

                    int calcWeight = weights[current] + adjEdge.PathWeight;
                    var adjNode = map[adj];
                    int adjWeight = adjNode.Priority;

                    // is tense?
                    if (calcWeight < adjWeight)
                    {
                        // relax
                        map[adj].Priority = calcWeight;
                        // potentially O(n)
                        pq.Find(n => n == adjNode).Priority = calcWeight;

                        if (!parents.TryAdd(adj, current))
                        {
                            parents[adj] = current;
                        }
                    }
                }

            }

            // only here for PrintShortestPaths() & PrintShortestPath() - not recommended
            outerParents = parents;

            return weights;
        }

        public static void PrintShortestPath(CaveGraph graph, CaveVertex source, CaveVertex destination)
        {
            var path = DijkstraGraph(graph, source);

            bool end = false;

            var vertex = destination;
            var parents = outerParents;

            int weightCounter = 0;
            int lastEntry = 0;

            while (!end)
            {
                if (vertex == null || !parents.ContainsKey(vertex))               
                    break;                

                Console.WriteLine($" {vertex.X + "," + vertex.Y} Weight: {vertex.Weight} ({path[vertex]})");

                weightCounter += vertex.Weight;
                lastEntry = vertex.Weight;
                vertex = parents[vertex];
            }

            weightCounter -= lastEntry;

            Console.WriteLine("Weight: " + weightCounter);
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

    public class Node
    {
        public string Identifier;
        //public List<Node> ConnectingNodes;
        public CaveType Type;        
    }

    public enum CaveType
    {
        Big,
        Small,
        Start,
        End
    }

    public class CaveNode
    {
        public CaveNode(CaveVertex v, int priority)
        {
            V = v;
            Priority = priority;
        }

        public CaveVertex V;
        public int Priority;

        public int X;
        public int Y;
        public int Weight;
    }

    public class CaveVertex
    {
        public CaveVertex(int key, int x, int y, int weight)
        {
            Key = key;
            X = x;
            Y = y;
            Weight = weight;
        }
        public int Key;

        public int X;
        public int Y;
        public int Weight;
    }

    public class CaveEdge
    {
        public CaveEdge(CaveVertex source, CaveVertex destination, int weight)
        {
            Source = source;
            Destination = destination;
            PathWeight = weight;
        }

        public int PathWeight;
        public CaveVertex Source;
        public CaveVertex Destination;
    }

    public class CaveGraph
    {
        private Dictionary<CaveVertex, List<CaveEdge>> adjList;

        public CaveGraph()
        {
            adjList = new Dictionary<CaveVertex, List<CaveEdge>>();
        }

        public Dictionary<CaveVertex, List<CaveEdge>> AdjList
        {
            get
            {
                return adjList;
            }
        }

        public void AddEdgeDirected(CaveVertex source, CaveVertex destination, int weight)
        {
            if (adjList.ContainsKey(source))
            {
                adjList[source].Add(new CaveEdge(source, destination, weight));
            }
            else
            {
                adjList.Add(source, new List<CaveEdge> { new CaveEdge(source, destination, weight) });
            }

            if (!adjList.ContainsKey(destination))
            {
                adjList.Add(destination, new List<CaveEdge>());
            }
        }

        
    }
}
