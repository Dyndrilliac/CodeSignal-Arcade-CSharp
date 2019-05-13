using System;

namespace CodeSignal_Arcade_CSharp
{
    class Program
    {
        private const string WORLD_EXIST_ERROR = "World does not exist.";

        private const string WORLD_IMPLEMENT_ERROR = "World not yet implemented.";
        
        public class PickWorldException : Exception {
            public PickWorldException(string message) : base(message) {}
        }

        public static void Main(string[] args)
        {
            uint choice = 0;

            do {
                try {
                    Console.Clear();
                    Console.WriteLine("Hello, CodeSignal Arcade!");
                    choice = pickWorld();

                    switch (choice) {
                        case 0:
                            break;
                        case 1:
                            try {
                                choice = pickProblem();
                                IntroTests.runTests(choice);
                            } catch (Exception e) {
                                Console.WriteLine("\n" + e.Message);
                            }
                            break;
                        case 2:
                            throw new PickWorldException(WORLD_IMPLEMENT_ERROR);
                        case 3:
                            throw new PickWorldException(WORLD_IMPLEMENT_ERROR);
                        case 4:
                            throw new PickWorldException(WORLD_IMPLEMENT_ERROR);
                        case 5:
                            throw new PickWorldException(WORLD_IMPLEMENT_ERROR);
                        default:
                            throw new PickWorldException(WORLD_EXIST_ERROR);
                    }
                } catch (Exception e) {
                    Console.WriteLine("\n" + e.Message);
                } finally {
                    Console.WriteLine("\nPress any key to continue from the main menu.");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (choice > 0);
        }

        private static uint pickWorld() {
            Console.WriteLine("\nPick a world:\n");
            Console.WriteLine("\t[{0}]\t{1}", 1, "Intro");
            Console.WriteLine("\t[{0}]\t{1}", 2, "Databases");
            Console.WriteLine("\t[{0}]\t{1}", 3, "The Core");
            Console.WriteLine("\t[{0}]\t{1}", 4, "Python");
            Console.WriteLine("\t[{0}]\t{1}", 5, "Graphs");
            Console.Write("\nSelection (0 to exit):\t");
            return UInt32.Parse(Console.ReadLine());
        }

        private static uint pickProblem() {
            Console.WriteLine("\nPick a problem:\n");
            Console.WriteLine("\t[{0}]\t{1}", 1, "add");
            Console.WriteLine("\t[{0}]\t{1}", 2, "centuryFromYear");
            Console.WriteLine("\t[{0}]\t{1}", 3, "checkPalindrome");
            Console.WriteLine("\t[{0}]\t{1}", 4, "adjacentElementsProduct");
            Console.WriteLine("\t[{0}]\t{1}", 5, "shapeArea");
            Console.WriteLine("\t[{0}]\t{1}", 6, "makeArrayConsecutive2");
            Console.WriteLine("\t[{0}]\t{1}", 7, "almostIncreasingSequence");
            Console.WriteLine("\t[{0}]\t{1}", 8, "matrixElementsSum");
            Console.WriteLine("\t[{0}]\t{1}", 9, "allLongestStrings");
            Console.WriteLine("\t[{0}]\t{1}", 10, "commonCharacterCount");
            Console.WriteLine("\t[{0}]\t{1}", 11, "isLucky");
            Console.WriteLine("\t[{0}]\t{1}", 12, "sortByHeight");
            Console.WriteLine("\t[{0}]\t{1}", 13, "reverseInParentheses");
            Console.WriteLine("\t[{0}]\t{1}", 14, "alternatingSums");
            Console.WriteLine("\t[{0}]\t{1}", 15, "addBorder");
            Console.WriteLine("\t[{0}]\t{1}", 16, "areSimilar");
            Console.WriteLine("\t[{0}]\t{1}", 17, "arrayChange");
            Console.Write("\nSelection (0 to exit):\t");
            return UInt32.Parse(Console.ReadLine());
        }
    }
}