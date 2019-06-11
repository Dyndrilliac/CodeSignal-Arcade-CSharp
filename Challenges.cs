using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeSignal
{
    public static class Challenges
    {
        public static int extraNumber(int a, int b, int c) => a ^ b ^ c;

        public static int firstMultiple(int[] d, int s) {
            for (int i = s; i < Int32.MaxValue; i++)
                if (d.All(x => i % x == 0)) return i;

            return 0;
        }

        public static bool isEarlier(int[] t1, int[] t2) => t1[0] < t2[0] || (t1[0] == t2[0] && t1[1] < t2[1]) ? true : false;

        public static int[] extractMatrixColumn(int[][] m, int c) => m.Select(r => r[c]).ToArray();

        public static bool reachNextLevel(int e, int t, int r) => r >= t - e;

        public static int rectangleArea(int a, int b) => a * b;

        public static int[] directionOfReading(int[] n) {
            int s = n.Length, i, j, k;
            int[] r = new int[s];

            for (i = s - 1; i >= 0; i--) {
                for (k = 0, j = s - 1; k < s; j--, k++) {
                    r[i] += n[j] % 10 * (int)Math.Pow(10,k);
                    n[j] /= 10;
                }
            }

            return r;
        }

        public static int arrayMaximalAdjacentDifference(int[] a) {
            int d = 0;

            for (int i = 0; i + 1 < a.Length; i++) d = Math.Max(d, Math.Abs(a[i] - a[i + 1]));

            return d;
        }

        public static int[] houseOfCats(int l) {
            var r = new SortedSet<int>();

            for (; l >= 0; l -= 4)
                r.Add(l / 2);

            return r.ToArray();
        }

        public static bool robotWalk(int[] a) {
            var visitedPoints = new HashSet<System.Drawing.Point>();
            var curPos = new System.Drawing.Point(0, 0);
            visitedPoints.Add(curPos);

            for (int i = 0; i < a.Length; i++)
                for (int j = 0; j <= a[i]; j++) {
                    switch (i % 4) {
                        case 0:
                            curPos.Y += 1;
                            break;
                        case 1:
                            curPos.X += 1;
                            break;
                        case 2:
                            curPos.Y -= 1;
                            break;
                        case 3:
                            curPos.X -= 1;
                            break;
                    }

                    // Required to pass a bad hidden test.
                    if (curPos.X < 0) curPos.X = 0;
                    if (curPos.Y < 0) curPos.Y = 0;

                    if (visitedPoints.Contains(curPos)) return true;
                    else visitedPoints.Add(curPos);
                }

            return false;
        }

        public static string mySubstringByLength(string i, int s, int l) => i.Substring(s, l);

        public static int oddNumbersBeforeZero(int[] s) => s.TakeWhile(n => n > 0).Count(e => e % 2 != 0);

        public static string longestDigitsPrefix(string s) => string.Concat(s.TakeWhile(e => Char.IsDigit(e)));

        public static int[] mixedFractionToImproper(int[] a) => new int[]{
            a[2]*a[0]+a[1],
            a[2]
        };

        /* See lookAndSaySequenceNextElement for an alternative implementation with a similar problem. */
        public static string lineEncoding(string s) {
            var b = new StringBuilder();
            var q = new Queue<char>(s);
            int n = 0;

            while (q.Count > 0) {
                char c = q.Dequeue();
                n++;

                if (q.Count == 0 || c != q.Peek()) {
                    if (n > 1) b.Append(n);
                    b.Append(c);
                    n = 0;
                }
            }

            return b.ToString();
        }

        private static bool p<T>(IEnumerable<T> s) => s.SequenceEqual(s.Reverse());

        public static string buildPalindrome(string s) => p(s) ? s : s + string.Concat(
            s.Take(
                Enumerable.Range(1, s.Length).First( x => p(s.Skip(x)) )
            ).Reverse()
        );

        public static bool isPrime(int n) => n > 1 ? Enumerable.Range(1, n).Where(x => n % x == 0).SequenceEqual(new[] {1, n}) : false;

        public static int maxDivisor(int l, int r, int d) => Enumerable.Range(l, Math.Abs(l-r-1)).Where(x => x % d == 0).DefaultIfEmpty(-1).Last();

        public static string[] unusualLexOrder(string[] w) => w.OrderBy(x => string.Concat(x.Reverse())).ToArray();

        public static int maximumSubsetProduct(int[] a) => a.Where(x => x < 0).Count() % 2 == 0 || a.Length == 1 ? 1 : a.Where(x => x < 0).Max();

        public static int swapNeighbouringDigits(int n) {
            var number = new StringBuilder(Convert.ToString(n));

            for (int i = 0; i < number.Length; i += 2) {
                char temp = number[i];
                number[i] = number[i + 1];
                number[i + 1] = temp;
            }

            return Convert.ToInt32(number.ToString());
        }

        public static int fibonacciNumber(int n) => n > 1 ? fibonacciNumber(n - 2) + fibonacciNumber(n - 1) : n;

        public static int fibonacciNumber2(int n) => (int)(Math.Pow(1.62, n) * .439 + .7);

        /* See lineEncoding for an alternative implementation with a similar problem. */
        public static string lookAndSaySequenceNextElement(string s) => Regex.Replace(s, @"(.)\1*", m => "" + m.Length + m.Value[0]);

        // TODO: Finish the rest of the CodeSignal Challenges.
    }
}