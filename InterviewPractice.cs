using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeSignal
{
    public static class InterviewPractice
    {
        public static class Arrays
        {
            public static int firstDuplicate(int[] a) {
                var s = new HashSet<int>();

                foreach (int i in a)
                    if (s.Contains(i)) return i;
                    else s.Add(i);

                return -1;
            }

            public static char firstNotRepeatingCharacter(string s) {
                if (s.Length == 1) return s[0];

                bool[] alphabetSeen = new bool[26];

                for (int i = 0; i < s.Length - 1; i++)
                    if (!alphabetSeen[s[i] - 'a'])
                        if (!s.Substring(i + 1).Contains(s[i])) return s[i];
                        else alphabetSeen[s[i] - 'a'] = true;

                return '_';
            }

            public static int[][] rotateImageByNew(int[][] a) {
                int n = a.Length;
                int[][] b = new int[n][];

                for (int k = 0; k < n; k++) {
                    b[k] = new int[n];
                    // The kth row of b is equal to the the kth column of a in reverse order.
                    for (int i = 0, j = n - 1; i < n; i++, j--) {
                        b[k][i] = a[j][k];
                    }
                }

                return b;
            }

            private static void swap(int[][] a, int g, int h, int i, int j) {
                int temp = a[g][h];
                a[g][h] = a[i][j];
                a[i][j] = temp;
            }

            public static int[][] rotateImageInPlace(int[][] a) {
                if (a.Length > 1) {
                    int topRow = 0, bottomRow = a.Length - 1, leftCol = topRow, rightCol = bottomRow;

                    while (topRow < bottomRow && leftCol < rightCol) {
                        swap(a, topRow, leftCol, topRow, rightCol);
                        swap(a, topRow, leftCol, bottomRow, leftCol);
                        swap(a, bottomRow, leftCol, bottomRow, rightCol);

                        for (int i = topRow + 1, j = bottomRow - 1; i < bottomRow && j > topRow; i++, j--) {
                            swap(a, topRow, i, i, rightCol);
                            swap(a, topRow, i, bottomRow, j);
                            swap(a, topRow, i, j, leftCol);
                        }

                        topRow++; leftCol++;
                        bottomRow--; rightCol--;
                    }
                }

                return a;
            }

            // TODO: Finish the rest of the CodeSignal Interview Practice Array tasks.
        }

        // TODO: Finish the rest of the CodeSignal Interview Practice tasks.
    }
}