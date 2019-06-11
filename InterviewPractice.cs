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

            private static int firstDuplicateInSequence(char[] a) {
                var s = new HashSet<char>();

                foreach (char c in a)
                    if (c == '.' || !char.IsDigit(c)) continue;
                    else if (s.Contains(c)) return (int)c;
                    else s.Add(c);

                return -1;
            }

            private static int duplicateInSubGrid(char[][] a, int row, int col) {
                var s = new List<char>();

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        s.Add(a[row + i][col + j]);

                return firstDuplicateInSequence(s.ToArray());
            }

            private static int firstDuplicateInSubGrid(char[][] a) {
                for (int i = 0; i < 9; i += 3)
                    for (int j = 0; j < 9; j += 3) {
                        int result = duplicateInSubGrid(a, i, j);
                        if (result != -1) return result;
                    }

                return -1;
            }

            public static bool sudoku2(char[][] grid) {
                for (int x = 0; x < 9; x++)
                    if (firstDuplicateInSequence(grid[x]) != -1 || firstDuplicateInSequence(grid.Select(r => r[x]).ToArray()) != -1) return false;
                    else if (firstDuplicateInSubGrid(grid) != -1) return false;

                return true;
            }

            private static double decryptString(string s, Dictionary<char,char> mapping) {
                var buffer = new StringBuilder();

                foreach (char c in s) buffer.Append(mapping[c]);

                if (buffer.Length > 1 && buffer.ToString().StartsWith("0")) return -1;
                else return Convert.ToDouble(buffer.ToString());
            }

            public static bool isCryptSolution(string[] crypt, char[][] solution) {
                var mapping = solution.ToDictionary(x => x[0], x => x[1]);
                double[] decrypt = new double[3]{decryptString(crypt[0], mapping), decryptString(crypt[1], mapping), decryptString(crypt[2], mapping)};

                return ( decrypt[0] + decrypt[1] == decrypt[2] );
            }
        }

        public static class LinkedLists
        {
            public class ListNode<T> {
                public T value { get; set; }
                public ListNode<T> next { get; set; }
            }

            public static ListNode<int> removeKFromList(ListNode<int> l, int k) {
                while (l != null && l.@value == k) l = l.next;

                if (l != null) {
                    var current = l;

                    while (current.next != null) {
                        if (current.next.@value == k) current.next = current.next.next;
                        else current = current.next;
                    }
                }

                return l;
            }

            // TODO: Finish the rest of the CodeSignal Interview Practice LinkedList tasks.
        }

        // TODO: Finish the rest of the CodeSignal Interview Practice tasks.
    }
}