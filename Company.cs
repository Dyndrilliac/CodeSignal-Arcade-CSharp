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

            // TODO: Finish the rest of the CodeSignal SpaceX Company Challenges.
        }

        // TODO: Finish the rest of the CodeSignal Company Challenges.
    }
}