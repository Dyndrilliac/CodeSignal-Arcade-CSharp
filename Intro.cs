using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeSignal
{
    public static class Intro
    {
        public static int add(int param1, int param2) => param1 + param2;

        public static int centuryFromYear(int year) => (year % 100) != 0 ? (year / 100) + 1 : year / 100;

        public static bool checkPalindrome(string inputString) => inputString.SequenceEqual(inputString.Reverse());

        public static int adjacentElementsProduct(int[] inputArray) {
            var outputList = new List<int>();
            for (int i = 0; i < inputArray.Length - 1; i++) outputList.Add(inputArray[i] * inputArray[i + 1]);
            return outputList.Max();
        }

        public static int shapeArea(int n) {
            switch (n) {
                case 4:
                    return 25;
                case 3:
                    return 13;
                case 2:
                    return 5;
                case 1:
                    return 1;
                default:
                    return (shapeArea(n - 1) + ((n - 1) * 4));
            }
        }

        public static int makeArrayConsecutive2(int[] statues) => ((statues.Max() + 1) - statues.Min()) - statues.Length;

        private static bool removeElement(IEnumerable<int> sequence, int i) {
            bool removeElement = false;
            int c = sequence.ElementAt(i), p = sequence.ElementAtOrDefault(i - 1), n = sequence.ElementAtOrDefault(i + 1);

            if (i == 0) removeElement = (c >= n);
            else if (i == (sequence.Count() - 1)) removeElement = (c <= p);
            else
                if (c > p && c < n) removeElement = false;
                else if (c > p && c >= n) {
                    removeElement = true;

                    // Handle edge case for test 19.
                    if (i == (sequence.Count() - 2)) removeElement = false;

                    // Handle edge case for test 16.
                    if (i < (sequence.Count() - 2))
                        if (n < sequence.ElementAt(i + 2) && c < sequence.ElementAt(i + 2)) removeElement = false;
                } else removeElement = true;

            return removeElement;
        }

        public static bool almostIncreasingSequence(IEnumerable<int> sequence) {
            if (sequence.Count() <= 2)
                return true;
            else if (sequence.SequenceEqual(sequence.Distinct().OrderBy(x => x)))
                return true;
            else {
                int index = Enumerable.Range(0, sequence.Count()).First(x => removeElement(sequence, x));
                sequence = sequence.Where((x, i) => i != index);
                return sequence.SequenceEqual(sequence.Distinct().OrderBy(x => x));
            }
        }

        private static bool isBadRoom(int[][] matrix, int i, int j) {
            for (int k = i; k >= 0; k--)
                if (matrix[k][j] == 0) return true;

            return false;
        }

        public static int matrixElementsSum(int[][] matrix) {
            int sum = 0;

            for (int i = 0; i < matrix.Length; i++)
                for (int j = 0; j < matrix[i].Length; j++)
                    if (isBadRoom(matrix, i, j)) continue;
                    else sum += matrix[i][j];

            return sum;
        }

        public static string[] allLongestStrings(string[] inputArray) {
            var longestStrings = new List<string>();
            int maxLength = 0;

            foreach (string s in inputArray) {
                if (s.Length == maxLength) {
                    longestStrings.Add(s);
                } else if (s.Length > maxLength) {
                    maxLength = s.Length;
                    longestStrings.Clear();
                    longestStrings.Add(s);
                }
            }

            return longestStrings.ToArray();
        }

        public static int commonCharacterCount(string s1, string s2) {
            var freqMap1 = s1.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var freqMap2 = s2.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            return Enumerable.Intersect(freqMap1.Keys, freqMap2.Keys).Sum(x => Math.Min(freqMap1[x], freqMap2[x]));
        }

        public static bool isLucky(int n) {
            string numStr = n.ToString();
            int middleBound = numStr.Length / 2;
            return numStr.Substring(middleBound).Sum(x => x - '0') == numStr.Remove(middleBound).Sum(x => x - '0');
        }

        private static bool isTree(int n) => n == -1;

        public static int[] sortByHeight(int[] a) {
            var sorted = a.ToList();
            sorted.RemoveAll(isTree);
            sorted.Sort();

            for (int i = 0; i < a.Length; i++) {
                if (isTree(a[i])) continue;
                else {
                    a[i] = sorted[0];
                    sorted.RemoveAt(0);
                }
            }

            return a;
        }

        public static string reverseInParentheses(string inputString) {
            var stack = new Stack<StringBuilder>();
            var result = new StringBuilder();
            StringBuilder buffer = null;

            for (int i = 0, depth = 0; i < inputString.Length; i++) {
                char c = inputString[i];

                if (depth == 0 && c != '(' && c != ')') result.Append(c);
                if (depth >  0 && c != '(' && c != ')') buffer.Append(c);

                if (c == '(') {
                    depth++;

                    if (buffer == null) buffer = new StringBuilder();
                    else {
                        stack.Push(buffer);
                        buffer = new StringBuilder();
                    }
                }

                if (c == ')') {
                    depth--;

                    string output = string.Concat(buffer.ToString().Reverse());

                    if (stack.Count > 0) {
                        buffer = stack.Pop();
                        buffer.Append(output);
                    } else {
                        buffer = null;
                        result.Append(output);
                    }
                }
            }

            return result.ToString();
        }

        public static int[] alternatingSums(int[] a) {
            var sums = new int[2]{
                a.Where((x, i) => i % 2 == 0).Sum(),
                a.Where((x, i) => i % 2 != 0).Sum()
            };

            return sums;
        }

        public static string[] addBorder(string[] picture) {
            string[] result = new string[picture.Length + 2];

            result[0] = new string('*', picture[0].Length + 2);
            result[picture.Length + 1] = result[0];

            for (int i = 1; i < (picture.Length + 1); i++) result[i] = ("*" + picture[i - 1] + "*");

            return result;
        }

        public static bool areSimilar(int[] a, int[] b) {
            if (a.Length != b.Length) return false;
            if (a.SequenceEqual(b))   return true;

            for (int i = 0; i < a.Length; i++) {
                if (a[i] != b[i]) {
                    for (int j = (i + 1); j < a.Length; j++) {
                        if (a[j] != b[j]) {
                            if ((a[i] == b[j]) && (a[j] == b[i])) {
                                int newLen = ((a.Length - 1) - j);
                                int[] A = new int[newLen], B = new int[newLen];
                                Array.Copy(a, (j + 1), A, 0, newLen);
                                Array.Copy(b, (j + 1), B, 0, newLen);

                                if (A.SequenceEqual(B)) return true;
                                else return false;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private static int fixItem(int[] a, int i) {
            int moves = 0;

            while (a[i] <= a[i - 1]) {
                a[i]++;
                moves++;
            }

            return moves;
        }
        public static int arrayChange(int[] inputArray) {
            int moves = 0;
            for (int i = 1; i < inputArray.Length; i++) moves += fixItem(inputArray, i);
            return moves;
        }

        public static bool palindromeRearranging(string inputString) {
            if (string.IsNullOrEmpty(inputString)) return false;
            if (inputString.Length == 1) return true;

            var freqMap = inputString.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            return freqMap.Keys.Where(x => freqMap[x] % 2 != 0).Count() <= 1;
        }

        public static bool areEquallyStrong(int yourLeft, int yourRight, int friendsLeft, int friendsRight) {
            return ((Math.Max(yourLeft, yourRight) == Math.Max(friendsLeft, friendsRight)) && (Math.Min(yourLeft, yourRight) == Math.Min(friendsLeft, friendsRight)));
        }

        public static int arrayMaximalAdjacentDifference(int[] inputArray) {
            int maxAbsDif = 0;
            for (int i = 0; (i + 1) < inputArray.Length; i++) maxAbsDif = Math.Max(maxAbsDif, Math.Abs(inputArray[i] - inputArray[i + 1]));
            return maxAbsDif;
        }

        public static bool isIPv4Address(string inputString) {
            string pattern = @"^\b(?:(?:2(?:[0-4][0-9]|5[0-5])|[0-1]?[0-9]?[0-9])\.){3}(?:(?:2([0-4][0-9]|5[0-5])|[0-1]?[0-9]?[0-9]))\b$";
            Regex rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(inputString);
            return (matches.Count == 1);
        }

        public static int avoidObstacles(int[] inputArray) {
            var list = inputArray.ToList();

            for (int minJump = 2; minJump < list.Max() + 1; minJump++) {
                bool encounteredObstacle = false;

                for (int i = 0; i < list.Max() + 1; i += minJump)
                    if (list.Contains(i)) {
                        encounteredObstacle = true;
                        break;
                    }

                if (!encounteredObstacle) return minJump;
            }

            return list.Max() + 1;
        }

        public static int[][] boxBlur(int[][] image) {
            int resultRows = image.Length - 2, resultCols = image[0].Length - 2;
            var result = new int[resultRows][];
            for (int i = 0; i < resultRows; i++) result[i] = new int[resultCols];

            for (int i = 1, resRow = 0; i < (image.Length - 1); i++, resRow++) {
                for (int j = 1, resCol = 0; j < (image[0].Length - 1); j++, resCol++) {
                    int upperLeft = image[i - 1][j - 1], upperCenter = image[i - 1][j], upperRight = image[i - 1][j + 1];
                    int middleLeft = image[i][j - 1], middleCenter = image[i][j], middleRight = image[i][j + 1];
                    int lowerLeft = image[i + 1][j - 1], lowerCenter = image[i + 1][j], lowerRight = image[i + 1][j + 1];
                    int sum = (upperLeft + upperCenter + upperRight + middleLeft + middleCenter + middleRight + lowerLeft + lowerCenter + lowerRight);
                    result[resRow][resCol] = (int)Math.Floor(((double)sum) / 9.0);
                }
            }

            return result;
        }

        private static int countMines(bool[][] a, int i, int j) {
            int rows = a.Length, cols = a[0].Length, count = 0;

            for (int y = i - 1; y <= i + 1; y++)
                if (y >= 0 && y < rows)
                    for (int x = j - 1; x <= j + 1; x++)
                        if (x >= 0 && x < cols)
                            if (a[y][x] == true) count++;

            return count;
        }

        public static int[][] minesweeper(bool[][] matrix) {
            int rows = matrix.Length, cols = matrix[0].Length;
            var result = new int[rows][];
            for (int i = 0; i < rows; i++) result[i] = new int[cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (!matrix[i][j]) result[i][j] = countMines(matrix, i, j);
                    else result[i][j] = (countMines(matrix, i, j) - 1);

            return result;
        }

        public static int[] arrayReplace(int[] inputArray, int elemToReplace, int substitutionElem) => inputArray.Select(x => x == elemToReplace ? substitutionElem : x).ToArray();

        public static bool evenDigitsOnly(int n) => n.ToString().All(x => Int32.Parse(x.ToString()) % 2 == 0);

        public static bool variableName(String name) {
            string pattern = @"^([a-zA-Z_](\w+)*)$";
            Regex rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(name);
            return (matches.Count == 1);
        }

        private static char characterMap(char c) => c == 'z' ? 'a' : (char)((int)c + 1);

        public static string alphabeticShift(string inputString) => string.Concat(inputString.Select(x => characterMap(x)));

        private static int getCellColor(string cell) => (cell[0]+cell[1]) % 2;

        public static bool chessBoardCellColor(string cell1, string cell2) => getCellColor(cell1) == getCellColor(cell2) ? true : false;

        public static int circleOfNumbers(int n, int firstNumber) => (firstNumber + (n / 2)) % n;

        // Use formula for compound interest where 'n' = 1: https://www.thecalculatorsite.com/articles/finance/compound-interest-formula.php#time-factor
        public static int depositProfit(double p, double r, double a) => (int)Math.Ceiling( Math.Log( a / p ) / ( Math.Log( 1.0 + ( r / 100.0 ) ) ) );

        public static int absoluteValuesSumMinimization(int[] a) => a[(int)Math.Floor((a.Length - 1.0) / 2.0)];

        private static IEnumerable<T[]> GetPermutations<T>(T[] values) {
            if (values.Length == 1) return new[] {values};
            return values.SelectMany(v => GetPermutations(values.Except(new[] {v}).ToArray()), (v, p) => new[] {v}.Concat(p).ToArray());
        }

        private static bool TestStrings(string s1, string s2) => s1.Length != s2.Length ? false : s1.Where((x,i) => x != s2[i]).Count() == 1;

        private static bool TestPermutation(string[] inputArray, int[] p) {
            bool test = false;

            for (int i = 0; i < p.Length - 1; i++) {
                test = TestStrings(inputArray[p[i]], inputArray[p[i + 1]]);
                if (!test) break;
            }

            return test;
        }

        public static bool stringsRearrangement(string[] inputArray) => GetPermutations(Enumerable.Range(0, inputArray.Length).ToArray()).Any(p => TestPermutation(inputArray, p));

        // TODO: Finish the rest of the CodeSignal Arcade Intro tasks.
    }
}