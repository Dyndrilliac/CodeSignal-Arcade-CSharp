using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeSignal
{
    public static class Company
    {
        public static class SpaceX
        {
            public static bool launchSequenceChecker(string[] systemNames, int[] stepNumbers) {
                var systems = new Dictionary<string,List<int>>();

                // Build a Dictionary such that each distinct system String has its own separate List of distinct step number Integers.
                for (int i = 0; i < systemNames.Length; i++) {
                    if (systems.ContainsKey(systemNames[i])) {
                        if (systems[systemNames[i]].Contains(stepNumbers[i])) return false;
                        else systems[systemNames[i]].Add(stepNumbers[i]);
                    } else {
                        systems.Add(systemNames[i], new List<int>());
                        systems[systemNames[i]].Add(stepNumbers[i]);
                    }
                }

                // Check that each system's List in the Dictionary is in ascending order.
                return systems.Keys.All(s => systems[s].SequenceEqual(systems[s].OrderBy(x => x)));
            }

            public static string packetDescrambler(int[] seq, char[] fragmentData, int n) {
                var data = new SortedDictionary<int,List<char>>();

                for (int i = 0; i < seq.Length; i++) {
                    if (data.ContainsKey(seq[i])) data[seq[i]].Add(fragmentData[i]);
                    else {
                        data.Add(seq[i], new List<char>());
                        data[seq[i]].Add(fragmentData[i]);
                    }
                }

                int max = data.Keys.Max(), groups = max + 1;

                if (data.Keys.SequenceEqual(Enumerable.Range(0, groups))) {
                    var buffer = new StringBuilder();

                    foreach (int i in data.Keys) {
                        var freqMap = data[i].GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

                        foreach (char c in freqMap.Keys) {
                            if (((double)freqMap[c] / (double)n) > (double)0.500000) {
                                if (c == '#' && i != max) return "";
                                else buffer.Append(c);
                            }
                        }
                    }

                    if (buffer.Length == groups) return buffer.ToString();
                }

                return "";
            }

            // TODO: Finish the rest of the CodeSignal SpaceX Company Challenges.
        }

        // TODO: Finish the rest of the CodeSignal Company Challenges.
    }
}