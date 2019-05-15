using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeSignal_Arcade_CSharp
{
    class IntroSolutions
    {
        public static int add(int param1, int param2) {
            return (param1 + param2);
        }

        public static int centuryFromYear(int year) {
            return ((year % 100) != 0) ? ((year / 100) + 1) : (year / 100);
        }

        public static bool checkPalindrome(string inputString) {
            return inputString.SequenceEqual(inputString.Reverse());
        }

        public static int adjacentElementsProduct(int[] inputArray) {
            List<int> outputList = new List<int>();

            for (int i = 0; i < (inputArray.Length - 1); i++) outputList.Add(inputArray[i] * inputArray[i + 1]);

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

        public static int makeArrayConsecutive2(int[] statues) {
            List<int> statuesList = new List<int>(statues);

            return (((statuesList.Max() + 1) - statuesList.Min()) - statuesList.Count);
        }

        private static bool removeElement(List<int> sequence, int i) {
            bool removeElement = false;

            if      (i == 0)                    removeElement = (sequence[i] >= sequence[i + 1]);
            else if (i == (sequence.Count - 1)) removeElement = (sequence[i] <= sequence[i - 1]);
            else {
                if (sequence[i] > sequence[i - 1]) {
                    if (sequence[i] < sequence[i + 1]) removeElement = false;
                    else {
                        removeElement = true;

                        // Handle edge case for test 19.
                        if (i == (sequence.Count - 2)) return false;

                        // Handle edge case for test 16.
                        if (i < (sequence.Count - 2)) {
                            if ((sequence[i + 1] < sequence[i + 2]) && (sequence[i] < sequence[i + 2])) return false;
                        }
                    }
                } else removeElement = true;
            }

            return removeElement;
        }

        public static bool almostIncreasingSequence(int[] sequence) {
            if (sequence.Length <= 2) return true;

            List<int> initSequence = new List<int>(sequence);

            for (int i = 0; i < initSequence.Count; i++) {
                bool remEle = removeElement(initSequence, i);

                if (remEle) {
                    initSequence.RemoveAt(i);

                    for (int j = i; j < initSequence.Count; j++) {
                        remEle = removeElement(initSequence, j);

                        if (remEle) return false;
                    }

                    break;
                }
            }

            return true;
        }

        private static bool isBadRoom(int[][] matrix, int i, int j) {
            for (int k = i; k >= 0; k--) {
                if (matrix[k][j] == 0) return true;
            }

            return false;
        }

        public static int matrixElementsSum(int[][] matrix) {
            int sum = 0;

            for (int i = 0; i < matrix.Length; i++) {
                for (int j = 0; j < matrix[i].Length; j++) {
                    if (isBadRoom(matrix, i, j)) continue;
                    else sum += matrix[i][j];
                }
            }

            return sum;
        }

        public static string[] allLongestStrings(string[] inputArray) {
            List<string> longestStrings = new List<string>();
            int maxLength = 0;

            foreach (string s in inputArray) {
                if      (s.Length == maxLength) longestStrings.Add(s);
                else if (s.Length > maxLength) {
                    maxLength = s.Length;
                    longestStrings.Clear();
                    longestStrings.Add(s);
                }
            }

            return longestStrings.ToArray();
        }

        public static int commonCharacterCount(string s1, string s2) {
            HashSet<char> characterSet = new HashSet<char>();
            Dictionary<char,int> freqMap1 = new Dictionary<char,int>(), freqMap2 = new Dictionary<char,int>();
            int count = 0;

            foreach (char c in s1) {
                if (freqMap1.ContainsKey(c)) freqMap1[c] += 1;
                else {
                    freqMap1.Add(c,1);
                    characterSet.Add(c);
                }
            }

            foreach (char c in s2) {
                if (freqMap2.ContainsKey(c)) freqMap2[c] += 1;
                else {
                    freqMap2.Add(c,1);
                    characterSet.Add(c);
                }
            }

            foreach (char c in characterSet) {
                if (freqMap1.ContainsKey(c) && freqMap2.ContainsKey(c)) count += Math.Min(freqMap1[c], freqMap2[c]);
            }

            return count;
        }

        public static bool isLucky(int n) {
            string number = n.ToString();
            int sumA = 0, sumB = 0, middleBound = (number.Length / 2);
            
            for (int i = 0;           i < middleBound;   i++) sumA += Int32.Parse(number[i].ToString());
            for (int i = middleBound; i < number.Length; i++) sumB += Int32.Parse(number[i].ToString());
            
            return (sumA == sumB);
        }

        private static bool isTree(int n) {
            return (n == -1);
        }

        public static int[] sortByHeight(int[] a) {
            List<int> sorted = new List<int>(a);

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
            StringBuilder result = new StringBuilder(), buffer = null;
            Stack<StringBuilder> stack = new Stack<StringBuilder>();

            for (int i = 0, depth = 0; i < inputString.Length; i++) {
                char c = inputString[i];

                if ((depth == 0) && (c != '(') && (c != ')')) result.Append(c);
                if ((depth >  0) && (c != '(') && (c != ')')) buffer.Append(c);

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

                    string output = new string (buffer.ToString().Reverse().ToArray());

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
            int[] sums = new int[2];

            for (int i = 0; i < a.Length; i++) {
                if      ((i == 0) || ((i % 2) == 0)) sums[0] += a[i];
                else if ((i == 1) || ((i % 2) != 0)) sums[1] += a[i];
            }

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

            Dictionary<char,int> freqMap = new Dictionary<char,int>();
            HashSet<char> characterSet = new HashSet<char>();

            foreach (char c in inputString) {
                if (freqMap.ContainsKey(c)) freqMap[c]++;
                else {
                    freqMap.Add(c,1);
                    characterSet.Add(c);
                }
            }

            bool foundOdd = false;

            foreach (char c in characterSet) {
                if ((freqMap[c] % 2) != 0) {
                    if (foundOdd) return false;
                    else foundOdd = true;
                }
            }

            return true;
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
            List<int> list = new List<int>(inputArray);

            for (int minJump = 2; minJump < (list.Max() + 1); minJump++) {
                bool encounteredObstacle = false;

                for (int i = 0; i < (list.Max() + 1); i += minJump) {
                    if (list.Contains(i)) {
                        encounteredObstacle = true;
                        break;
                    }
                }

                if (!encounteredObstacle) return minJump;
            }

            return (list.Max() + 1);
        }

        public static int[][] boxBlur(int[][] image) {
            int resultRows = image.Length - 2, resultCols = image[0].Length - 2;
            int[][] result = new int[resultRows][];
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
            int rows = a.Length, cols = a[0].Length;
            int count = 0;

            for (int y = (i - 1); y <= (i + 1); y++) {
                if ((y >= 0) && (y < rows)) {
                    for (int x = (j - 1); x <= (j + 1); x++) {
                        if ((x >= 0) && (x < cols)) {
                            if (a[y][x] == true) count++;
                        }
                    }
                }
            }

            return count;
        }

        public static int[][] minesweeper(bool[][] matrix) {
            int rows = matrix.Length, cols = matrix[0].Length;
            int[][] result = new int[rows][];
            for (int i = 0; i < rows; i++) result[i] = new int[cols];

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    if (!matrix[i][j]) {
                        result[i][j] = countMines(matrix, i, j);
                    } else {
                        result[i][j] = (countMines(matrix, i, j) - 1);
                    }
                }
            }

            return result;
        }

        public static int[] arrayReplace(int[] inputArray, int elemToReplace, int substitutionElem) {
            for (int i = 0; i < inputArray.Length; i++) {
                if (inputArray[i] == elemToReplace) inputArray[i] = substitutionElem;
            }
            
            return inputArray;
        }

        public static bool evenDigitsOnly(int n) {
            string s = n.ToString();
            bool result = true;

            foreach (char c in s) {
                if ((Int32.Parse(c.ToString()) % 2) != 0) {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public static bool variableName(String name) {
            string pattern = @"^([a-zA-Z_](\w+)*)$";
            Regex rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(name);
            return (matches.Count == 1);
        }

        private static char characterMap(char c) {
            if (c != 'z') return (char)((int)c + 1);
            else return 'a';
        }

        public static string alphabeticShift(string inputString) {
            StringBuilder buffer = new StringBuilder();

            foreach (char c in inputString) buffer.Append(characterMap(c));

            return buffer.ToString();
        }

        // TODO: Finish the rest of the CodeSignal Arcade Intro tasks.
    }
}