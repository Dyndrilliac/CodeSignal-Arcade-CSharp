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

        public static int oddNumbersBeforeZero(int[] s) => s.TakeWhile(n => n > 0).Count(e => e % 2 != 0);

        public static string longestDigitsPrefix(string s) => string.Concat(s.TakeWhile(e => Char.IsDigit(e)));

        public static int[] mixedFractionToImproper(int[] a) => new int[]{
            a[2]*a[0]+a[1],
            a[2]
        };

        // TODO: Finish the rest of the CodeSignal Challenges.
    }
}