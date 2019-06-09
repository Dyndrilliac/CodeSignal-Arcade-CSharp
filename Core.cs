using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeSignal
{
    public static class Core
    {
        public static int addTwoDigits(int n) => (n / 10) + (n % 10);

        public static int largestNumber(int n) => (int)(Math.Pow(10.0, n) - 1.0);

        public static int candies(int n, int m) => (m / n) * n;

        public static int seatsInTheater(int nCols, int nRows, int col, int row) => (nCols - (col - 1)) * (nRows - row);

        public static int maxMultiple(int divisor, int bound) => bound - (bound % divisor);

        public static int circleOfNumbers(int n, int firstNumber) => (firstNumber + (n / 2)) % n;

        public static int lateRide(int n) => ((n / 60) / 10) + ((n / 60) % 10) + ((n % 60) / 10) + ((n % 60) % 10);

        public static int phoneCall(int min1, int min2_10, int min11, int s) {
            int rem = s - min1, minutes = 0;

            if (rem >= 0) {
                minutes++;

                if ((min2_10 * 9) <= rem) {
                    minutes += 9;
                    rem -= (min2_10 * 9);

                    if (rem > 0) minutes += (rem / min11);
                } else minutes += (rem / min2_10);
            }

            return minutes;
        }

        public static bool reachNextLevel(int experience, int threshold, int reward) => reward >= threshold - experience;

        public static int knapsackLight(int value1, int weight1, int value2, int weight2, int maxW) {
            if (maxW >= weight1 + weight2) return value1 + value2;
            else if (value1 > value2) {
                if (maxW >= weight1) return value1;
                else if (maxW >= weight2) return value2;
                else return 0;
            } else {
                if (maxW >= weight2) return value2;
                else if (maxW >= weight1) return value1;
                else return 0;
            }
        }

        public static int extraNumber(int a, int b, int c) => a ^ b ^ c;

        private static bool isOdd(int n) => n % 2 != 0;

        private static bool isEven(int n) => n % 2 == 0;

        public static bool isInfiniteProcess(int a, int b) => a > b || (isOdd(a) && isEven(b)) || (isEven(a) && isOdd(b));

        public static bool arithmeticExpression(int a, int b, int c) {
            var tests = new bool[4] { (a + b == c), (a - b == c), (a * b == c), (b * c == a) };

            return tests.Any(x => x == true);
        }

        public static bool tennisSet(int score1, int score2) {
            int max = Math.Max(score1, score2), min = Math.Min(score1, score2);

            if (max == 6 && min < 5) return true;
            if (max == 7 && min < 7 && min >= 5) return true;

            return false;
        }

        public static bool willYou(bool young, bool beautiful, bool loved) => ((young && beautiful && !loved) || (loved && (!young || !beautiful))) ? true : false;

        public static int[] metroCard(int lastNumberOfDays) => Enumerable.Range(1, 12).Where(x => DateTime.DaysInMonth(1998, x) == lastNumberOfDays)
                                                                .Select(x => DateTime.DaysInMonth(1998, x < 12 ? x + 1 : x)).Distinct().ToArray();

        public static int killKthBit(int n, int k) => n & ~(1 << (k - 1));

        public static int arrayPacking(int[] a) {
            var bytes = a.Select(x => Convert.ToByte(x));
            switch (bytes.Count()) {
                case 2: return BitConverter.ToInt16(bytes.ToArray(), 0);
                case 3: return BitConverter.ToInt32(bytes.Concat(new byte[1]{0}).ToArray(), 0);
                case 4: return BitConverter.ToInt32(bytes.ToArray(), 0);
                default: return bytes.ElementAtOrDefault(0);
            }
        }

        private static int numberOfSetBits(int i) {
            i = i - ((i >> 1) & 0x55555555);
            i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
            return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
        }

        public static int rangeBitCount(int a, int b) => Enumerable.Range(a, Math.Abs(a-b-1)).Sum(x => numberOfSetBits(x));

        public static int mirrorBits(int a) => Convert.ToInt32(string.Concat(Convert.ToString(a, 2).Reverse()), 2);

        public static int secondRightmostZeroBit(int n) => ~(n | n + 1) & ((n | n + 1) + 1);

        // TODO: Finish the rest of the CodeSignal Arcade Core tasks.
    }
}